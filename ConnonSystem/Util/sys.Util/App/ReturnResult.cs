using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Util
{
    public class ReturnResult
    {
        public ReturnResult()
        {
            Code = 0;
            Msg = "";
            Obj = "";
        }

        public ReturnResult(int code,string msg = "",object obj = null)
        {
            Code = code;
            Msg = msg;
            Obj = obj??"";
        }

        public int Code { get; set; }

        public string Msg { get; set; }

        public object Obj { get; set; }
    }

    public class ReturnResult<T>
    {
        public ReturnResult()
        {
            Code = 0;
            Msg = "";
            Obj = default(T);
        }

        public ReturnResult(int code, string msg = "", T obj = default(T))
        {
            Code = code;
            Msg = msg;
            Obj = obj;
        }

        public int Code { get; set; }

        public string Msg { get; set; }

        public T Obj { get; set; }
    }
}
