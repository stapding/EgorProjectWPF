
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
    /// Логика взаимодействия для CategoryUserControl.xaml
    /// </summary>
    public partial class CategoryUserControl : UserControl
    {

        private Category category;

        public CategoryUserControl(Category category)
        {
            InitializeComponent();

            DataContext = category;

            this.category = category;
        }

        public void Delete(object sender, EventArgs e)
        {
            if (!JsonRepository.existsProductByCategoryId(this.category.Id))
            {
                JsonRepository.deleteCategoryById(this.category.Id);
                this.Visibility = Visibility.Collapsed;
                Sock.sendData();
            } else
            {
                MessageBox.Show("Невозможно удалить категорию. Существуют товары с данной категорией");
            }
        }
    }
}
