using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;  
using Microsoft.AspNet.Identity; 
using sys.Application.Api;
namespace sys.Application.Api.Controllers
{
    public class AccountController : ApiController
    {
        private AuthRepository _authRepo;

        public AccountController() 
        { 
            _authRepo = new AuthRepository(); 
        } 
        [AllowAnonymous] 
        [Route("Register")] 
        public async Task<IHttpActionResult> Register(UserModel model) 
        { 
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            } 
            IdentityResult result = await _authRepo.Register(model); 
            IHttpActionResult errorResult = GetError(result); 
            if (errorResult != null) 
            { 
                return errorResult; 
            } 
            return Ok(); 
        }

         
        private IHttpActionResult GetError(IdentityResult result) 
        { 
            if (result == null) 
            { 
                return InternalServerError(); 
            } 
            if (!result.Succeeded) 
            { 
                foreach (string err in result.Errors) 
                { 
                    ModelState.AddModelError("", err); 
                } 
                if (ModelState.IsValid) 
                { 
                    return BadRequest(); 
                } 
                return BadRequest(ModelState); 
            } 
            return null; 
        }
    }
}
