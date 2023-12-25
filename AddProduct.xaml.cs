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
    public partial class AddProduct : Page
    {
        private Frame frame = null;
        private Product product;

        public AddProduct(Frame frame,Product product)
        {
            InitializeComponent();
            this.frame = frame;
            this.product = product;

            int index = 0;
            int currentIndex = 0;
            foreach(var item in JsonRepository.findAllCategories())
            {
                TextBlock tb = new TextBlock();
                tb.Text = item.Name;
                tb.Tag = item.Id;
                sx.Items.Add(tb);
                if(this.product!=null && item.Name == this.product.Category.Name)
                {
                    currentIndex = index;
                }
                index++;
            }

            if (this.product != null)
            {
                name.Text = this.product.Name;
                description.Text = this.product.Description;
                price.Text = this.product.Price + "";
                count.Text = this.product.Count + "";
                sx.SelectedIndex = currentIndex;

                if (this.product.Url != null)
                {
                    user.Url = this.product.Url;
                    Image img = new Image();
                    photo.Child = img;
                    photo_name.Text = this.product.Url.Substring(this.product.Url.LastIndexOf('\\') + 1);
                }
            }
        }

        private Product user = new Product();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "photo (.png) |*.png | photo (.jpg) |*.jpg";
            if (ofd.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                user.Url = ofd.FileName;
                Image img = new Image();
                img.Source = image;
                photo.Child = img;
                photo_name.Text = ofd.FileName.Substring(ofd.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            description.Text = "";
            price.Text = "";
            count.Text = "";
            photo.Child = photo_text;
            photo_name.Text = "";
            user = new Product();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!validate()) return;

            user.Name = name.Text;
            user.Description = description.Text;
            int.TryParse(price.Text,out int p);
            int.TryParse(count.Text,out int c);

            if (p < 0) p = 0;
            else if (c < 0) c = 0;


            user.Count = c;
            user.Price = p;

            if (this.product != null) user.Id = this.product.Id;


            TextBlock tb = sx.SelectedItem as TextBlock;
            user.Category = new Category(int.Parse(tb.Tag.ToString()));


            JsonRepository.addProduct(user);

            Sock.sendData();

                frame.GoBack();
        }

        private bool validate()
        {
            if (name.Text == "" || string.IsNullOrEmpty(name.Text) ||
                description.Text == "" || string.IsNullOrEmpty(description.Text))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }

            return true;
        }

    }
}
