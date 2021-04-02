using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models
{
    public class UserInfo
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool CanCreate { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanRead { get; set; }

        public bool CanDelete { get; set; }

        public UserInfo() { }

        public UserInfo(string name, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            this.Name = name;
            this.Email = email;
            this.CanCreate = canCreate;
            this.CanUpdate = canUpdate;
            this.CanRead = canRead;
            this.CanDelete = canDelete;
        }

        public UserInfo(string name, string password, string email, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.CanCreate = canCreate;
            this.CanUpdate = canUpdate;
            this.CanRead = canRead;
            this.CanDelete = canDelete;
        }
    }
}