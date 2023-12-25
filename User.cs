using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Rus
{
    public class User
    {
        private string email;
        private string password;
        private string name;
        private string surname;
        private Sx sx;
        private string photo;
        private DateTime birthdate;
        private string country;
        private Role role;
        public List<int> productIds { get; set; }

        public User() {
            this.Role = Role.USER;
            this.productIds = new List<int>();
        }

        public User(string email)
        {
            this.email = email;
            this.Role = Role.USER;
            this.productIds = new List<int>();
        }

        public User(string email, string password, string name, string surname, Sx sx, string photo, DateTime birthdate, string country)
        {
            this.email = email;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.sx = sx;
            this.photo = photo;
            this.birthdate = birthdate;
            this.country = country;
            this.productIds = new List<int>();
        }
        
        public Role Role {
            get { return role; }
            set { this.role = value; }
        }


        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set
            {
                this.surname = value;
            }
        }

        public Sx X
        {
            get { return this.sx; }
            set
            {
                this.sx = value;
            }
        }

        public DateTime Birthdate
        {
            get { return this.birthdate; }
            set
            {
                this.birthdate = value;
            }
        }

        public string Country
        {
            get { return this.country; }
            set
            {
                this.country = value;
            }
        }

        public string Photo
        {
            get { return this.photo; }
            set
            {
                this.photo = value;
            }
        }

    }

    public enum Sx
    {
        MEN,
        WOMAN
    }

    public enum Role
    {
        ADMIN,USER
    }
}
