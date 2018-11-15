using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.WebServices.Models
{
    public class IdentityModel
    {
        private string _name;
        private string _password;
        private DateTime _expiredDate;
        public IdentityModel( string name,string password,DateTime expiredDate )
        {
            _name = name;
            _password = password;
            _expiredDate = expiredDate;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Password
        {
            get { return _password; }
        }

        public DateTime ExpirationDate
        {
            get { return _expiredDate; }
        }
    }
}
