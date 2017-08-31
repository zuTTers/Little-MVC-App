using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.ViewModels
{
    public class UserViewModel
    {
        
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        

    }
    public class UserListViewModel
    {
        public IList<Users> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}