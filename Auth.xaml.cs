using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
using System.Windows.Threading;

namespace Rus
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        private Frame frame = null;

        public Auth(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private DispatcherTimer timer = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            email.Text = "";
            password.Text = "";
        }

        private bool captchaEnabled = false;
        private string capthcaCode = "";
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            User user = JsonRepository.findByEmail(email.Text);
            if (user == null || !user.Password.Equals(password.Text) || (captchaEnabled && !captchaTb.Text.Equals(capthcaCode)))
            {
                MessageBox.Show("Неверный логин или пароль");
                if (!captchaEnabled)
                {
                    captchaEnabled = true;
                    captcha.Height = 50;
                    captcha.Visibility = Visibility.Visible;
                    captchaImage_MouseDown(null, null);
                    return;
                } else
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 10);
                    timer.Tick += tick;
                    timer.Start();
                    return;
                }
            }
            Security.user = user;
            MessageBox.Show("Вы авторизовались");
            if (user.Role == Role.USER)
            {
                frame.Navigate(new Admin(frame));
            } else
            {
                frame.Navigate(new Admin2(frame));
            }
        }

        private void tick(object sender, EventArgs e)
        {
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            captchaImage_MouseDown(null, null);
            timer.Stop();
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void captchaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cpt = CaptchaGenerator.Generate();
            capthcaCode = cpt.Item2;
            captchaImage.Source = BitmapToImageSource(cpt.Item1);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Admin(frame));
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Registration(frame));
        }
    }
}
