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
    /// Логика взаимодействия для Admin2.xaml
    /// </summary>
    public partial class Admin2 : Page
    {

        private Frame frame;

        public Admin2(Frame frame)
        {
            InitializeComponent();

            this.frame = frame;

            Button_Click_1(null, null);

            Sock.startServer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.Children.Clear();
            List<Category> products = JsonRepository.findAllCategories();

            foreach (Category product in products)
            {
                CategoryUserControl us = new CategoryUserControl(product);
                us.Tag = "g" + product.Id;
                us.MouseDown += Edit1;
                data.Children.Add(us);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            data.Children.Clear();
            List<Product> products = JsonRepository.findAllProducts();

            foreach (Product product in products)
            {
                ProductUserControl us = new ProductUserControl(new ListProduct("g" + product.Id, product.Id, product.Name, product.Description, product.Category.Name, product.Price + " ₽", "Осталось " + product.Count, product.Url, product.Count), product,null);
                us.MouseDown += Edit;
                data.Children.Add(us); 
            }
        }

        private void Button_Click_2(object sender, EventArgs e)
        {
            frame.Navigate(new AddProduct(frame,null));
        }

        private void Edit(object sender, EventArgs e)
        {
            frame.Navigate(new AddProduct(frame,JsonRepository.findById(int.Parse(((ProductUserControl) sender).grid.Tag.ToString().Substring(1)))));
        }

        private void Edit1(object sender, EventArgs e)
        {
            frame.Navigate(new AddCategory(frame, JsonRepository.findCategoryById(int.Parse(((CategoryUserControl) sender).Tag.ToString().Substring(1)))));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddCategory(frame, null));
        }
    }
}
