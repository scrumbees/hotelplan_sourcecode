using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerMsgApp.Model;
using System.Net.Mail;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Twilio;
using System.Data.Entity;

namespace CustomerMsgApp.Service
{
    public partial class UserService
    {
        private readonly CustomerContext _UserService = new CustomerContext();

        public int AddNewUser(User User)
        {
            try
            {
                User user = new User();
                user = User;
                //_UserService.User.Add(user);
                _UserService.Entry(User).State = user.Id == 0 ? EntityState.Added : EntityState.Modified;
                _UserService.SaveChanges();
                return User.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public bool AddUserRole(UserRole UserRole)
        {
            try
            {
                UserRole userrole = new UserRole();
                userrole = UserRole;
                _UserService.UserRole.Add(userrole);
                _UserService.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool DeleteUserRole(int Id)
        {
            try
            {

                List<UserRole> Listuserrole = _UserService.UserRole.Where(u => u.UserId == Id).ToList();
                foreach (var item in Listuserrole)
                {
                    UserRole userrole = new UserRole();
                    userrole = item;
                    _UserService.UserRole.Remove(userrole);
                    _UserService.SaveChanges();
                }



                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public dynamic GetAllUser()
        {
            var UserDataList = _UserService.User.AsQueryable();

            var users = (from n in UserDataList
                         select new
                         {
                             n.Id,
                             n.UserName,
                             n.Password,
                             n.FirstName,
                             n.LastName,
                             n.Email,
                             n.MobileNo,
                             n.CreateDate
                         }).ToList();

            var user = (from row in users
                        select new
                        {
                            Id = row.Id,
                            UserName = row.UserName,
                            Password = row.Password,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            FullName = row.FirstName + " " + row.LastName,
                            MobileNo = row.MobileNo,
                            Email = row.Email,
                            CreateDate = row.CreateDate,
                        }).OrderByDescending(x => x.CreateDate).AsQueryable();

            //if (NotificationSearch.iSortingCols == 1)
            //{
            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 0) { user = user.OrderBy(p => p.UserName); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 0) { user = user.OrderByDescending(p => p.UserName); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 1) { user = user.OrderBy(p => p.Password); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 1) { user = user.OrderByDescending(p => p.Password); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 2) { user = user.OrderBy(p => p.FirstName); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 2) { user = user.OrderByDescending(p => p.FirstName); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 3) { user = user.OrderBy(p => p.LastName); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 3) { user = user.OrderByDescending(p => p.LastName); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 4) { user = user.OrderBy(p => p.Email); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 4) { user = user.OrderByDescending(p => p.Email); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 5) { user = user.OrderBy(p => p.MobileNo); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 5) { user = user.OrderByDescending(p => p.MobileNo); }

            //    if (NotificationSearch.sSortDir_0 == "asc" && NotificationSearch.iSortCol_0 == 6) { user = user.OrderBy(p => p.CreateDate); }
            //    if (NotificationSearch.sSortDir_0 == "desc" && NotificationSearch.iSortCol_0 == 6) { user = user.OrderByDescending(p => p.CreateDate); }
            //}
            //var datatable = new
            //{
            //    sEcho = NotificationSearch.sEcho,
            //    iTotalRecords = user.OrderByDescending(e => e.CreateDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).Count(),
            //    iTotalDisplayRecords = user.Count(),
            //    aaData = user.OrderByDescending(e => e.CreateDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).ToList()
            //};
            return user;
        }

        public dynamic GetUser(int Id)
        {
            var UserDataList = _UserService.User.Where(u => u.Id == Id).AsQueryable();

            var users = (from n in UserDataList
                         select new
                         {
                             n.Id,
                             n.UserName,
                             n.Password,
                             n.FirstName,
                             n.LastName,
                             n.Email,
                             n.MobileNo,
                             n.CreateDate
                         }).ToList();

            var user = (from row in users
                        select new
                        {
                            Id = row.Id,
                            UserName = row.UserName,
                            Password = row.Password,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            MobileNo = row.MobileNo,
                            Email = row.Email,
                            CreateDate = row.CreateDate,
                        }).OrderByDescending(x => x.CreateDate).AsQueryable();

            return user;
        }

        public dynamic GetUserRole(int UserId)
        {
            var UserDataList = _UserService.UserRole.Where(u => u.UserId == UserId).AsQueryable();
            var users = (from n in UserDataList
                         select n).ToList();
            return users;
        }

        public int CheckUserNameExist(string userName, int userId)
        {
            try
            {
                int c;
                if (userId != 0)
                {
                    c = _UserService.User.Where(u => u.UserName == userName && u.Id != userId).Count();
                }
                else
                {
                    c = _UserService.User.Where(u => u.UserName == userName).Count();
                }

                return c;
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }
}
