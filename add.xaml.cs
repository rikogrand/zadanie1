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
using System.Windows.Shapes;

namespace zadanie1
{
    /// <summary>
    /// Логика взаимодействия для add.xaml
    /// </summary>
    public partial class add : Window
    {
        public MainWindow mainwindow { get; set; }
        public add(UserDbContext User)
        {

            InitializeComponent();
            List<CategoryDbContext> category = new List<CategoryDbContext>();
            using (ApplicationContext db = new ApplicationContext())
            { 
                category = db.Categories.ToList();
            }
            categorybox.ItemsSource = category;
            categorybox.DisplayMemberPath = "Category";

        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Convert.ToInt32(quantitybox.Text);
            quantity++;
            quantitybox.Text = Convert.ToString(quantity);
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Convert.ToInt32(quantitybox.Text);
            quantity--;
            quantitybox.Text = Convert.ToString(quantity);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (categorybox.SelectedIndex > -1 && !String.IsNullOrEmpty(paydescbox.Text) && !String.IsNullOrEmpty(quantitybox.Text) && !String.IsNullOrEmpty(pricebox.Text))
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    PaymentDbContext pay = new PaymentDbContext()
                    {
                        PayDis = paydescbox.Text,
                        Quantity = Convert.ToInt32(quantitybox.Text),
                        Price = Convert.ToDecimal(pricebox.Text),
                        Category = (CategoryDbContext)categorybox.SelectedItem,
                        Date = DateTime.Now, User = mainwindow.CurrentUser


                    };
                    db.Payments.Add(pay);
                    db.Categories.Attach((CategoryDbContext)categorybox.SelectedItem);
                    db.Users.Attach(mainwindow.CurrentUser);
                    db.SaveChanges();
                    mainwindow.Payments = db.Payments.Where(x => x.User == mainwindow.CurrentUser).ToList();
                    mainwindow.paymentcountGrid.ItemsSource = mainwindow.Payments;
                    
                }
                Close();
            }
            else
            {
                MessageBox.Show("Заполните все значения");
            }
        }
    
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
