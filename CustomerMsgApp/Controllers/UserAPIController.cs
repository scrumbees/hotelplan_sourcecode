using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerMsgApp.Service;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Runtime.CompilerServices;
using CustomerMsgApp.Model;
using System.Threading.Tasks;

namespace CustomerMsgApp.Controllers
{
    public class UserAPIController : ApiController
    {
        public UserService _UserService;

        public UserAPIController()
        {
            _UserService = new UserService();
        }

        #region Method
        // GET api/userapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userapi/5
        public string Get(int? id)
        {
            return "value";
        }

        // POST api/userapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/userapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        #endregion

        [HttpPost]
        public int AddNewUser(User User)
        {
            try
            {
                int loginModel = _UserService.AddNewUser(User);
                if (loginModel != 0)
                {
                    return loginModel;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public bool AddUserRole(UserRole UserRole)
        {
            try
            {
                bool loginModel = _UserService.AddUserRole(UserRole);
                if (loginModel != null)
                {
                    return loginModel;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteUserRole(int Id)
        {
            try
            {
                bool loginModel = _UserService.DeleteUserRole(Id);
                if (loginModel != null)
                {
                    return loginModel;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public IEnumerable<EnumModel> GetRoleName()
        {
            List<EnumModel> currencyList = new List<EnumModel>();
            foreach (var role in Enum.GetValues(typeof(RoleName)))
            {
                EnumModel rolename = new EnumModel();
                rolename.Id = (int)(RoleName)Enum.Parse(typeof(RoleName), role.ToString(), true);
                rolename.Name = Convert.ToString((RoleName)Enum.Parse(typeof(RoleName), role.ToString(), true));
                currencyList.Add(rolename);
            }
            return currencyList;
        }

        [HttpGet]
        public dynamic GetAllUser()
        {
            try
            {
                dynamic UserList;
                UserList = _UserService.GetAllUser();
                return UserList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        public dynamic GetUser(int Id)
        {
            try
            {
                dynamic UserList;
                UserList = _UserService.GetUser(Id);
                return UserList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        public dynamic GetUserRole(int Id)
        {
            try
            {
                dynamic UserList;
                UserList = _UserService.GetUserRole(Id);
                return UserList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}