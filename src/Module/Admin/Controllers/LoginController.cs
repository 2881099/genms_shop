using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using es.BLL;
using es.Model;

namespace es.Module.Admin.Controllers {

	[Route("")]
	public class HomeController {
		[HttpGet]
		public RedirectResult Index() {
			return new RedirectResult("/module/Admin");
		}
	}

	[Route("[controller]")]
	[Obsolete]
	public class LoginController : BaseController {

		public LoginController(ILogger<LoginController> logger) : base(logger) { }

		[HttpGet, 匿名访问]
		public ViewResult Index() {
			return View();
		}
		[HttpPost, 匿名访问]
		public APIReturn Post(LoginModel data) {
			HttpContext.Session.SetString("login.username", data.Username);
			return APIReturn.成功;
		}

		public class LoginModel {
			[FromForm, Required(ErrorMessage = "请输入登陆名")]
			public string Username { get; set; }

			[FromForm, Required(ErrorMessage = "请输入密码")]
			public string Password { get; set; }
		}
	}
}
