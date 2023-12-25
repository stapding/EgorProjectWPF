using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Page
    {

        private Category category;
        private Frame frame;

        public AddCategory(Frame frame,Category category)
        {
            InitializeComponent();

            this.category = category;
            this.frame = frame;

            if(category != null ) {
                name.Text = category.Name;
            }
        }

        private Category user = new Category();

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            user = new Category();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!validate()) return;

            user.Name = name.Text;


            JsonRepository.addCategory(user);

            Sock.sendData();

            frame.GoBack();
        }

        private bool validate()
        {
            if (name.Text == "" || string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }

            return true;
        }
    }
}
