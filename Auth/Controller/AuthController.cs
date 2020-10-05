using System.Collections.Generic;
using BackendWebService.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendWebService.Auth.Controller {

    [Produces ("application/json")]
    [EnableCors ("AllowSpecificOrigins")]
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        [HttpPost]
        [Route ("signin")]
        public SignInRespond SingIn ([FromBody] SignInRequest request) {
            SignInRespond respond = new SignInRespond ();
            respond.Email = request.Email;
            respond.Token = "123456789987456321";
            return respond;
        }

        [HttpPost]
        [Route ("signup")]
        public void SignUp () {

        }

        [HttpPost]
        [Route ("signout")]
        public void SignOut () {

        }
    }
}