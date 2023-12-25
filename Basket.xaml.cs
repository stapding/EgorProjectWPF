using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rus
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {

        private Frame frame;

        public Basket(Frame frame)
        {
            InitializeComponent();

            this.frame = frame;

            var set = new HashSet<int>(Security.user.productIds);


            foreach(int id in set)
            {
                Product product = JsonRepository.findById(id);
                if (product!=null)
                data.Children.Add(new ProductUserControl(new ListProduct("g" + product.Id, product.Id, product.Name, product.Description, product.Category.Name, product.Price + " ₽", "Осталось " + product.Count, product.Url, product.Count), product,updatePrice));
            }

            updatePrice();
        }


        public void updatePrice()
        {
            var set = new HashSet<int>(Security.user.productIds);

            int sum = 0;

            foreach (int id in set)
            {
                Product product = JsonRepository.findById(id);
                int count = 0;
                foreach(var productId in Security.user.productIds)
                {
                    if(productId==id)
                    {
                        count++;
                    }
                }
                sum += product.Price * count;
            }

            price.Text = "Всего: " + sum + " ₽";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Security.user.productIds.Count != 0)
            {
                List<Product> products = new List<Product>();
                List<int> usedIds = new List<int>();
                foreach (int id in Security.user.productIds)
                {
                    if (!usedIds.Contains(id))
                    {
                        usedIds.Add(id);

                        Product product = JsonRepository.findById(id);
                        int count = 0;
                        foreach (var productId in Security.user.productIds)
                        {
                            if (productId == product.Id)
                            {
                                count++;
                            }
                        }
                        product.Count = count;

                        products.Add(product);
                    }
                }

                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();

                Order order = new Order(myuuidAsString, DateTime.Now, Security.user, products);


                string json = JsonSerializer.Serialize(order);

                TcpClient client = null;
                NetworkStream stream = null;

                try
                {
                    client = new TcpClient("localhost", 12345);
                    stream = client.GetStream();

                    byte[] data = Encoding.ASCII.GetBytes(json);

                    stream.Write(data, 0, data.Length);

                    Console.WriteLine($"Sent: {json}");

                    Security.user.productIds.Clear();
                    JsonRepository.editUser(Security.user);

                    this.frame.GoBack();
                } catch (SocketException ex)
                {
                } finally
                {
                    if(stream!=null) stream.Close();
                    if(client!=null) client.Close();
                }
            }
        }
    }
}
