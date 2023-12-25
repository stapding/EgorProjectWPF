using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rus
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private Frame frame = null;

        public Registration(Frame frame)
        {
            InitializeComponent();
            this.frame = frame; 
        }

        private User user = new User();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "photo (.png) |*.png | photo (.jpg) |*.jpg";
            if (ofd.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                user.Photo = ofd.FileName;
                Image img = new Image();
                img.Source = image;
                photo.Child = img;
                photo_name.Text = ofd.FileName.Substring(ofd.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            email.Text = "";
            password.Text = "";
            r_password.Text = "";
            surname.Text = "";
            name.Text = "";
            sx.Text = "";
            photo.Child = photo_text;
            photo_name.Text = "";
            birthdate.Text = "";
            country.Text = "";
            user = new User();
            if (Security.user != null)
            {
                user = Security.user;

                setRunnerData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!validate()) return;

            user.Email = email.Text;
            user.Password = password.Text;
            user.Surname = surname.Text;
            user.Name = name.Text;
            if (sx.Text == "Мужской")
                user.X = Sx.MEN;
            else if (sx.Text == "Женский")
                user.X = Sx.WOMAN;
            user.Birthdate = (DateTime)birthdate.SelectedDate;
            user.Country = country.Text;

            if (JsonRepository.findByEmail(user.Email) != null && Security.user == null)
            {
                MessageBox.Show("Данный email уже используется");
                return;
            }

            User status = JsonRepository.addUser(user);

            if (status != null)
            {
                Security.user = status;

                frame.GoBack();
            }
        }

        private bool validate()
        {

            if (email.Text == "" || string.IsNullOrEmpty(email.Text) ||
                password.Text == "" || string.IsNullOrEmpty(password.Text) ||
                r_password.Text == "" || string.IsNullOrEmpty(r_password.Text) ||
                surname.Text == "" || string.IsNullOrEmpty(surname.Text) ||
                name.Text == "" || string.IsNullOrEmpty(name.Text) ||
                sx.Text == "" || string.IsNullOrEmpty(sx.Text) ||
                birthdate.Text == "" || string.IsNullOrEmpty(birthdate.Text) ||
                country.Text == "" || string.IsNullOrEmpty(country.Text))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }

            if (!password.Text.Equals(r_password.Text))
            {
                MessageBox.Show("Пароли должнры совпадать");
                return false;
            }

            if (password.Text.Length < 6)
            {
                MessageBox.Show("Пароль должжен быть не менее 6 символов");
                return false;
            }

            if (!password.Text.Any(v => char.IsUpper(v)))
            {
                MessageBox.Show("Пароль должжен содержать минимум 1 прописную букву");
                return false;
            }

            if (!password.Text.Any(v => char.IsDigit(v)))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 цифру");
                return false;
            }

            if (!password.Text.Any(c => "!@#$%^".Contains(c)))
            {
                MessageBox.Show("Пароль должен содержать один из символов !@#$%^");
                return false;
            }

            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            if (!Regex.IsMatch(email.Text, validEmailPattern))
            {
                MessageBox.Show("Введите правильный email");
                return false;
            }


            return true;
        }

        private void setRunnerData()
        {

            email.Text = user.Email;
            password.Text = user.Password;
            r_password.Text = user.Password;
            surname.Text = user.Surname;
            name.Text = user.Name;
            if (user.X == Sx.MEN)
                sx.Text = "Мужской";
            else if (user.X == Sx.WOMAN)
                sx.Text = "Женский";
            BitmapImage image = new BitmapImage(new Uri(user.Photo, UriKind.RelativeOrAbsolute));
            Image img = new Image();
            img.Source = image;
            photo.Child = img;
            photo_name.Text = user.Photo.Substring(user.Photo.LastIndexOf('\\') + 1);
            birthdate.Text = user.Birthdate.ToString();
            country.Text = user.Country;
        }
    }
}
