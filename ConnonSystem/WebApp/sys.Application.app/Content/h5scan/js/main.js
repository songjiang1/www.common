/**
 * Created by huangyuheng on 2018/4/9.
 */

var Quagga = window.Quagga;
var App = {
    _lastResult: null,
    init: function() {
        this.attachListeners();
    },
    activateScanner: function() {
        var scanner = this.configureScanner('.overlay__content'),
            onDetected = function (result) {
                this.addToResults(result);
            }.bind(this),
            stop = function() {
                scanner.stop();  // should also clear all event-listeners?
                scanner.removeEventListener('detected', onDetected);
                this.hideOverlay();
                this.attachListeners();
            }.bind(this);

        this.showOverlay(stop);
        console.log("activateScanner");
        scanner.addEventListener('detected', onDetected).start();
    },
    addToResults: function(result) {

        if (this._lastResult === result.codeResult.code) {
            return;
        }
        this._lastResult = result.codeResult.code;
        var resultSets = document.querySelectorAll('ul.results');

        Array.prototype.slice.call(resultSets).forEach(function(resultSet) {
            var li = document.createElement('li'),
                format = document.createElement('span'),
                code = document.createElement('span');

            console.log(result);
            li.className = "result";
            format.className = "format";
            code.className = "code";

            li.appendChild(format);
            li.appendChild(code);

            format.appendChild(document.createTextNode(result.codeResult.format));
            code.appendChild(document.createTextNode(result.codeResult.code));

            resultSet.insertBefore(li, resultSet.firstChild);
        });
    },
    attachListeners: function() {
        var button = document.querySelector('button.scan'),
            self = this;

        button.addEventListener("click", function clickListener (e) {
            e.preventDefault();
            button.removeEventListener("click", clickListener);
            self.activateScanner();
            self.qrcode();
        });
    },
    showOverlay: function(cancelCb) {
        document.querySelector('.container .controls')
            .classList.add('hide');
        document.querySelector('.overlay--inline')
            .classList.add('show');
        var closeButton = document.querySelector('.overlay__close');
        closeButton.addEventListener('click', function closeHandler() {
            closeButton.removeEventListener("click", closeHandler);
            cancelCb();
        });
    },
    hideOverlay: function() {
        document.querySelector('.container .controls')
            .classList.remove('hide');
        document.querySelector('.overlay--inline')
            .classList.remove('show');
    },
    querySelectedReaders: function(index,n) {
        // EAN-13:ean_reader
        // EAN-8:ean_8_reader
        // UPC-E:upc_e_reader
        // Code 39:code_39_reader
        // Codabar:codabar_reader
        // Code 128:code_128_reader
        // Interleaved 2 of 5:i2of5_reader
        // 编码配置reader
        var arr=["ean_reader","code_39_reader","code_128_reader"]
        return arr;

    },
    configureScanner: function(selector) {
        var scanner = Quagga
            .decoder({readers: this.querySelectedReaders()})
            .locator({patchSize: 'medium'})
            .fromSource({
                target: selector,
                constraints: {
                    width: 600,
                    height: 600,
                    facingMode: "environment"
                }
            });
        return scanner;
    },
    qrcode:function(){
        var gCtx = null;
        var gCanvas = null;
        var c=0;
        var stype=0;
        var gUM=false;
        var webkit=false;
        var moz=false;
        var v=null;

        function dragenter(e) {
            e.stopPropagation();
            e.preventDefault();
        }

        function dragover(e) {
            e.stopPropagation();
            e.preventDefault();
        }
        function drop(e) {
            e.stopPropagation();
            e.preventDefault();

            var dt = e.dataTransfer;
            var files = dt.files;
            if(files.length>0)
            {
                handleFiles(files);
            }
            else
            if(dt.getData('URL'))
            {
                qrcode.decode(dt.getData('URL'));
            }
        }

        function handleFiles(f)
        {
            var o=[];

            for(var i =0;i<f.length;i++)
            {
                var reader = new FileReader();
                reader.onload = (function(theFile) {
                    return function(e) {
                        gCtx.clearRect(0, 0, gCanvas.width, gCanvas.height);

                        qrcode.decode(e.target.result);
                    };
                })(f[i]);
                reader.readAsDataURL(f[i]);//readAsDataURL方法会使用base-64进行编码
            }
        }
        //渲染画布
        function initCanvas(w,h)
        {
            gCanvas = document.getElementById("qr-canvas");
            gCanvas.style.width = w + "px";
            gCanvas.style.height = h + "px";
            gCanvas.width = w;
            gCanvas.height = h;
            gCtx = gCanvas.getContext("2d");
            gCtx.clearRect(0, 0, w, h);
        }


        function captureToCanvas() {
            if(stype!=1)
                return;
            if(gUM)
            {
                try{
                    gCtx.drawImage(v,0,0);
                    try{
                        qrcode.decode();//解码图像
                    }
                    catch(e){
                        console.log(e);
                        setTimeout(captureToCanvas, 500);
                    };
                }
                catch(e){
                    console.log(e);
                    setTimeout(captureToCanvas, 500);
                };
            }
        }

        function htmlEntities(str) {
            return String(str).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
        }

        function read(a)
        {
            var result=a;
            var resultSets = document.querySelectorAll('ul.results');
            Array.prototype.slice.call(resultSets).forEach(function(resultSet) {
                var li = document.createElement('li'),
                    format = document.createElement('span'),
                    code = document.createElement('span');
                li.className = "result";
                format.className = "format";
                code.className = "code";

                li.appendChild(format);
                li.appendChild(code);

                format.appendChild(document.createTextNode('QR code'));
                code.appendChild(document.createTextNode(result));

                resultSet.insertBefore(li, resultSet.firstChild);
            });
        }

        function isCanvasSupported(){
            var elem = document.createElement('canvas');
            return !!(elem.getContext && elem.getContext('2d'));
        }
        function success(stream)
        {
            v.srcObject = stream;
            v.play();
            gUM=true;
            setTimeout(captureToCanvas, 500);
        }

        function error(error)
        {
            gUM=false;
            return;
        }

        function load()
        {
            if(isCanvasSupported() && window.File && window.FileReader)
            {
                initCanvas(800, 600);
                qrcode.callback = read;//将qrcode.callback设置为函数，其中数据将获得解码信息
                setwebcam();
            }
            else
            {

            }
        }
        //调用媒体对象
        function setwebcam()
        {

            var options = true;
            if(navigator.mediaDevices && navigator.mediaDevices.enumerateDevices)
            {
                try{
                    navigator.mediaDevices.enumerateDevices()
                        .then(function(devices) {
                            devices.forEach(function(device) {
                                if (device.kind === 'videoinput') {
                                    if(device.label.toLowerCase().search("back") >-1)
                                        options={'deviceId': {'exact':device.deviceId}, 'facingMode':'environment'} ;
                                }
                                console.log(device.kind + ": " + device.label +" id = " + device.deviceId);
                            });
                            setwebcam2(options);
                        });
                }
                catch(e)
                {
                    console.log(e);
                }
            }
            else{
                console.log("no navigator.mediaDevices.enumerateDevices" );
                setwebcam2(options);
            }

        }

        function setwebcam2(options)
        {
            console.log(options);
            if(stype==1)
            {
                setTimeout(captureToCanvas, 500);
                return;
            }
            var n=navigator;
            v=document.getElementById("v");

            if(n.mediaDevices.getUserMedia)
            {
                n.mediaDevices.getUserMedia({video: options, audio: false}).
                then(function(stream){
                    success(stream);
                }).catch(function(error){
                    error(error)
                });
            }
            else
            if(n.getUserMedia)
            {
                webkit=true;
                n.getUserMedia({video: options, audio: false}, success, error);
            }
            else
            if(n.webkitGetUserMedia)
            {
                webkit=true;
                n.webkitGetUserMedia({video:options, audio: false}, success, error);
            }

            stype=1;
            setTimeout(captureToCanvas, 500);
        }

        load();
    },
};
App.init();
