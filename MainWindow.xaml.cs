using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Category category = new Category("Автоматы");
            //category = JsonRepository.addCategory(category);

            //Product product = new Product("AK-74M", "Автомат калибра 5,45 мм, разработанный в 1970 году советским конструктором М. Т. Калашниковым и принятый на вооружение вооружённых сил СССР в 1974 году. Является дальнейшим развитием АКМ. Разработка АК-74 связана с переходом на новый малоимпульсный патрон 5,45×39 мм.", category, 1000, 10, "C:\\Users\\10a\\Downloads\\ak.jpg");
            //product = JsonRepository.addProduct(product);


            //Category category1 = new Category("Гранатомёты");
            //category1 = JsonRepository.addCategory(category1);

            //Product product1 = new Product("РПО-А 'Шмель'", "Cоветский и российский реактивный пехотный огнемёт одноразового применения. Представляет собой термобарическую реактивную гранату, начинённую огнесмесью. Во время войны в Афганистане получил прозвище «Шайта́н-труба́».", JsonRepository.findCategoryById(2), 10000, 100, "C:\\Users\\10a\\Downloads\\rpo.png");
            //product1 = JsonRepository.addProduct(product1);



            MainFrame.Navigate(new Auth(MainFrame));
        }

        public void requestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack)
            MainFrame.GoBack();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Basket(MainFrame));
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                back.Visibility = Visibility.Visible;
            } else
            {
                back.Visibility = Visibility.Hidden;
            }

            if(Security.user==null || (Security.user!=null && Security.user.Role==Role.ADMIN))
            {
                logout.Visibility = Visibility.Hidden;
            } else
            {
                logout.Visibility = Visibility.Visible;
            }
        }
    }
}
