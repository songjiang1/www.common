var Together = {};
Together.Detail = function() {
	return {
		init: function() {
			//show_project_tab(), send_get_donate_statistics_ajax(), send_get_project_data_ajax()
		}
	}
}(), Together.Form = function() {
	return {
		init: function() {
			//CfBase.init(), $("#checkbox_protocol").on("change", function() {
			//	this.checked ? $(".protocol_content").hide("fast") : $(".protocol_content").show("fast")
			//})
		}
	}
}(), Together.Index = function() {
	return {
		init: function() {
			var t = (new Swiper(".swiper-container", {
				pagination: ".swiper-pagination",
				autoplay: 3e3
            }),
               $("#team_id").val() 
                ),
				e = $("#subdomain").val(),
				o = "ajax_get_project_list?team_id=" + t + "&subdomain=" + e + "&selector=all_donate";
			send_get_list_ajax(o)
		}
	}
}(), Together.CrowdUsers = function() {
	return {
		init: function(t) {
			//var e = $("#project_id").val().trim();
			//if ("together" == t) var o = "ajax_get_crowd_users_list?project_id=" + e + "&selector=donators";
			//else var o = "ajax_get_donators_list?project_id=" + e + "&selector=donators";
			//send_get_list_ajax(o)
		}
	}
}(), Together.Donators = function() {
	return {
		init: function() {
			//var t = $("#user_project_id").val().trim(),
			//	e = "ajax_get_donators_list?user_project_id=" + t + "&selector=donators";
			//send_get_list_ajax(e)
		}
	}
}();