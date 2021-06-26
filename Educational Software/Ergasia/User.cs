using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    public class User
    {
        int id;
        string first_name, last_name, email, role;
        public User(int id, string first_name, string last_name, string email, string role)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.role = role;
        }

        public int getId()
        {
            return id;
        }
        public string getFirst_name()
        {
            return first_name;
        }
        public string getLast_name()
        {
            return last_name;
        }
        public string getEmail()
        {
            return email;
        }
        public string getRole()
        {
            return role;
        }
    }
}
