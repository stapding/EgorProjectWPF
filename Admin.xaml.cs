using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private Frame frame = null;

        public Admin(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            loadUsers();

            List<Category> categories = JsonRepository.findAllCategories();
            foreach (Category category in categories) {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = category.Name;
                checkBox.Checked += onCheckBoxChanged;
                categoryFilter.Items.Add(checkBox);
            }
        }

        private Filter filtering = Filter.NONE;
        private Sort sorting = Sort.NONE;
        private List<String> categories = new List<string>();

        private void onCheckBoxChanged(object sender, EventArgs e)
        {
            
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked.Value)
            {
                categories.Add(checkBox.Content.ToString());
            } else
            {
                categories.Remove(checkBox.Content.ToString());
            }

            loadUsers();
        }

        private void loadUsers()
        {
            if (data != null)
            {
                data.Children.Clear();

                List<Product> products = JsonRepository.findAllProducts();

                if (sorting != Sort.NONE)
                {
                    switch (sorting)
                    {
                        case Sort.PRICE_UP:
                            products = products.OrderBy(v => v.Price).ToList();
                            break;
                        case Sort.PRICE_DOWN:
                            products = products.OrderByDescending(v => v.Price).ToList();
                            break;
                    }
                }

                if (search.Text != "")
                {
                    string standard = search.Text.Trim();
                    string lower = standard.ToLower();
                    string upper = standard.ToUpper();
                    products = products.Where(v => v.Name.Contains(lower) || v.Name.Contains(upper) || v.Name.Contains(standard)).ToList();
                }

                if (filtering != Filter.NONE)
                {
                    switch (filtering)
                    {
                        case Filter.YES_PRODUCT:
                            products = products.Where(v => v.Count > 0).ToList();
                            break;
                        case Filter.NO_PRODUCT:
                            products = products.Where(v => v.Count == 0).ToList();
                            break;
                    }
                }

                if(categories.Count != 0)
                {
                    products = products.Where(v => categories.Contains(v.Category.Name)).ToList();
                }

                if (products == null)
                {
                    MessageBox.Show("Пользователи не найдены");
                }

                usersCount.Text = products.Count.ToString();
                foreach (Product user in products)
                {


                    //Grid grid = new Grid();
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());
                    //grid.ColumnDefinitions.Add(new ColumnDefinition());

                    //Border b0 = new Border();
                    //b0.SetValue(Grid.ColumnProperty, 0);
                    //b0.Style = data.Resources["border"] as Style;
                    //TextBlock tb = new TextBlock();
                    //tb.Text = user.Id.ToString();
                    //tb.Style = data.Resources["border_text"] as Style;
                    //b0.Child = tb;
                    //grid.Children.Add(b0);

                    //Border b1 = new Border();
                    //b1.SetValue(Grid.ColumnProperty, 1);
                    //b1.Style = data.Resources["border"] as Style;
                    //TextBlock tb1 = new TextBlock();
                    //tb1.Text = user.Name;
                    //tb1.Style = data.Resources["border_text"] as Style;
                    //b1.Child = tb1;
                    //grid.Children.Add(b1);

                    //Border b2 = new Border();
                    //b2.SetValue(Grid.ColumnProperty, 2);
                    //b2.Style = data.Resources["border"] as Style;
                    //TextBlock tb2 = new TextBlock();
                    //tb2.Text = user.Description;
                    //tb2.Style = data.Resources["border_text"] as Style;
                    //tb2.VerticalAlignment = VerticalAlignment.Center;
                    //b2.Child = tb2;
                    //grid.Children.Add(b2);


                    //Border b3 = new Border();
                    //b3.SetValue(Grid.ColumnProperty, 3);
                    //b3.Style = data.Resources["border"] as Style;
                    //TextBlock tb3 = new TextBlock();
                    //tb3.Text = user.Category.Name;
                    //tb3.Style = data.Resources["border_text"] as Style;
                    //tb3.VerticalAlignment = VerticalAlignment.Center;
                    //b3.Child = tb3;
                    //grid.Children.Add(b3);

                    //Border b4 = new Border();
                    //b4.SetValue(Grid.ColumnProperty, 4);
                    //b4.Style = data.Resources["border"] as Style;
                    //Button button = new Button();
                    //button.Click += onEdit;
                    //button.SetValue(Button.TagProperty, "b" + user.Id);
                    //button.Content = "Редактировать";
                    //button.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    //button.Foreground = new SolidColorBrush(Color.FromRgb(80, 80, 80));
                    //button.FontSize = 12;
                    //button.VerticalAlignment = VerticalAlignment.Center;
                    //button.HorizontalAlignment = HorizontalAlignment.Center;
                    //b4.Child = button;
                    //grid.Children.Add(b4);

                    //Border b5 = new Border();
                    //b5.SetValue(Grid.ColumnProperty,5);
                    //b5.Style = data.Resources["border"] as Style;
                    //Button button1 = new Button();
                    //button1.Click += delete;
                    //button1.SetValue(Button.TagProperty, "b1" + user.Id);
                    //button1.Content = "Удалить";
                    //button1.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    //button1.Foreground = new SolidColorBrush(Color.FromRgb(80, 80, 80));
                    //button1.FontSize = 12;
                    //button1.VerticalAlignment = VerticalAlignment.Center;
                    //button1.HorizontalAlignment = HorizontalAlignment.Center;
                    //b5.Child = button1;
                    //grid.Children.Add(b5);

                    //grid.Tag = "g" + user.Id;

                    data.Children.Add(new ProductUserControl(new ListProduct("g" + user.Id, user.Id, user.Name, user.Description, user.Category.Name, user.Price + " ₽", "Осталось " + user.Count,user.Url,user.Count),user,null));
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadUsers();
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            if (sort.Text == "Без сортировки")
            {
                sorting = Sort.NONE;
            } else if (sort.Text == "Цена(по убыванию)")
            {
                sorting = Sort.PRICE_DOWN;
            } else if (sort.Text == "Цена(по возрастанию)")
            {
                sorting = Sort.PRICE_UP;
            }
            loadUsers();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedIndex == 0)
            {
                filtering = Filter.NONE;
            }
            else if (filter.SelectedIndex == 1)
            {
                filtering = Filter.YES_PRODUCT;
            }
            else if (filter.SelectedIndex == 2)
            {
                filtering = Filter.NO_PRODUCT;
            }
                loadUsers();
        }

        //public void delete(object sender,EventArgs e)
        //{
        //    Button button = sender as Button;
        //    Grid grid = data.Children.OfType<Grid>().Where(g => g.Tag != null && g.Tag.ToString().Equals("g" + button.Tag.ToString().Substring(2))).First();
        //    string email = grid.Tag.ToString().Substring(1);

        //    data.Children.Remove(grid);
        //    JsonRepository.deleteByEmail(email);
        //}

        //public void onEdit(object sender, EventArgs e)
        //{
        //    Button button= sender as Button;
        //    Grid grid = data.Children.OfType<Grid>().Where(g => g.Tag!=null && g.Tag.ToString().Equals("g" + button.Tag.ToString().Substring(1))).First();
        //    string email = grid.Tag.ToString().Substring(1);
        //    string newEmail = null;
        //    string name = null;
        //    string surname = null;
        //    Role role = Role.USER;

        //    List<Border> borders = grid.Children.OfType<Border>().ToList();

        //    if (borders[0].Child.GetType() == typeof(TextBlock))
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Border border = borders.ToList()[i];
        //            if (i == 3)
        //            {
        //                ComboBox comboBox = new ComboBox();
        //                comboBox.Height = 25;
        //                comboBox.Margin = new Thickness(0, 0, 10, 0);
        //                comboBox.SelectedIndex = 1;
        //                comboBox.Items.Add("Админ");
        //                comboBox.Items.Add("Бегун");
        //                border.Child = comboBox;
        //            }
        //            else
        //            {
        //                TextBox tb = new TextBox();
        //                tb.Height = 25;
        //                tb.Margin = new Thickness(0, 0, 10, 0);
        //                border.Child = tb;
        //            }
        //        }
        //    } else
        //    {
        //        newEmail = ((TextBox) borders[2].Child).Text;
        //        name = ((TextBox)borders[0].Child).Text;
        //        surname = ((TextBox)borders[1].Child).Text;
        //        role = ((ComboBox)borders[3].Child).Text == "Админ" ? Role.ADMIN : Role.USER;

        //        if (!validate(newEmail, name, surname)) return;

        //        User user = JsonRepository.findByEmail(email);
        //        JsonRepository.deleteByEmail(email);
        //        user.Email = newEmail;
        //        user.Name = name;
        //        user.Surname = surname;
        //        user.Role = role;
        //        JsonRepository.addUser(user);

        //        loadUsers();

        //    }


        //}

        //private bool validate(string email,string name,string surname)
        //{
        //    if (email == "" || string.IsNullOrEmpty(email) ||
        //        surname == "" || string.IsNullOrEmpty(surname) ||
        //        name == "" || string.IsNullOrEmpty(name))
        //    {
        //        MessageBox.Show("Заполните все поля");
        //        return false;
        //    }

        //    string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
        //    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
        //    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        //    if (!Regex.IsMatch(email, validEmailPattern))
        //    {
        //        MessageBox.Show("Введите правильный email");
        //        return false;
        //    }

        //    if (JsonRepository.findByEmail(email) != null)
        //    {
        //        MessageBox.Show("Данный email уже используется");
        //        return false;
        //    }

        //    return true;
        //}


        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if (cb_role.Text == "Админ")
        //    {
        //        role = Role.ADMIN;
        //    }
        //    else if (cb_role.Text == "Бегун")
        //    {
        //        role = Role.USER;
        //    }


        //    switch (cb_sort.Text)
        //    {
        //        case "Имя":
        //            sort = Sort.NAME; break;
        //        default:
        //            sort = Sort.NONE; break;
        //    }


        //    loadUsers();
        //}


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

    }

    enum Sort
    {
        PRICE_UP,
        PRICE_DOWN,
        NONE
    }

    enum Filter
    {
        NONE,
        NO_PRODUCT,
        YES_PRODUCT
    }

    public class ListProduct
    {

        public ListProduct(string tag, int id, string name, string description, string category, string price,string countStr, string url,int count)
        {
            this.Tag = tag;
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.CountStr = countStr;
            this.Url = url;
            this.Count = count;
        }

        public string Tag { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Price { get; set; }

        public string CountStr { get; set; }

        public int Count { get; set; }

        public string Url { get; set; }
    }
}


