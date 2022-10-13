using PayrollPartnerAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollPartnerAPI.DataAccessLayer
{
    public interface IAuthDL
    {
        public Task<Response> SignUp(LoginModel model);

        public Task<Response> Login(LoginModel model);
    }
}
