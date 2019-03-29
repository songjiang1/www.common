using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Threading.Tasks; 
using Microsoft.AspNet.Identity; 
using Microsoft.AspNet.Identity.EntityFramework;
using sys.Application.Api;

namespace sys.Application.Api
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx; 
        private UserManager<IdentityUser> _userManager; 
        public AuthRepository() 
        { 
            _ctx = new AuthContext(); 
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx)); 
        }

        public async Task<IdentityResult> Register(UserModel model)
        {
            IdentityUser user = new IdentityUser() 
            { 
                UserName = model.UserName 
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password); 
            return result; 
        }

        public async Task<IdentityUser> FindUser(UserModel model) 
        { 
            IdentityUser user = await _userManager.FindAsync(model.UserName, model.Password); 
            return user; 
        }

        public async Task<IdentityUser> FindUserByName(string username) 
        { 
            IdentityUser user = await _userManager.FindByNameAsync(username); 
            return user; 
        }

        public void Dispose() 
        { 
            _ctx.Dispose(); 
            _userManager.Dispose(); 
        }
    }
}