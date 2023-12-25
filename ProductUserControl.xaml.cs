using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Логика взаимодействия для ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {

        private ListProduct product;
        private Product prod;
        private int counter;
        private Action action;

        public ProductUserControl(ListProduct listProduct,Product product,Action action)
        {
            InitializeComponent();

            DataContext = listProduct;

            this.product = listProduct;
            this.prod = product;
            this.action = action;

            if (Security.user != null)
            {
                if (Security.user.Role == Role.USER)
                {
                    this.add.Visibility = Visibility.Visible;
                    this.add.Click += onClickAdd;
                    this.remove.Visibility = Visibility.Visible;
                    this.remove.Click += onClickRemove;
                    this.count.Visibility = Visibility.Visible;

                    this.count.Text = "0";
                    foreach (var item in Security.user.productIds)
                    {
                        if (item == product.Id)
                        {
                            this.count.Text = (++counter).ToString();
                        }
                    }
                } else
                {
                    this.delete.Visibility = Visibility.Visible;
                    this.delete.Click += Delete;
                }
            }


        }

        public void onClickAdd(object sender, EventArgs e)
        {
            if (this.product.Count > 0)
            {
                Security.user.productIds.Add(this.product.Id);
                this.count.Text = (++counter).ToString();
                productCount.Text = "Осталось " + --this.product.Count;

                this.prod.Count = this.product.Count;
                JsonRepository.editProduct(this.prod);
                JsonRepository.editUser(Security.user);

                if(action!=null) action();
            }
        }

        public void onClickRemove(object sender, EventArgs e)
        {
            if (this.counter>0)
            {
                Security.user.productIds.Remove(this.product.Id);
                this.count.Text = (--counter).ToString();
                productCount.Text = "Осталось " + ++this.product.Count;

                this.prod.Count = this.product.Count;
                JsonRepository.editProduct(this.prod);
                JsonRepository.editUser(Security.user);

                if (action != null) action();
            }

        }

        public void Delete(object sender, EventArgs e)
        {
            JsonRepository.deleteById(this.product.Id);
            this.Visibility = Visibility.Collapsed;
            Sock.sendData();
        }
    }
}
