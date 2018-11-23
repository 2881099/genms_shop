using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using es.BLL;
using es.Model;

namespace es.Module.Test.Controllers {
	[Route("[controller]")]
	public class TestController : BaseController {
		public TestController(ILogger<TestController> logger) : base(logger) { }

		[HttpGet]
		public APIReturn List([FromServices]IConfiguration cfg) {
			return APIReturn.成功;
		}
	}
}
