using System.Windows;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;


namespace zadanie1
{
    public partial class MainWindow : Window
    {
        public List<PaymentDbContext> Payments { get; set; }
        public List<CategoryDbContext> Categories { get; set; }
        public UserDbContext CurrentUser { get; set; }
        public  MainWindow(UserDbContext user)
        {
            InitializeComponent();
            CurrentUser = user;
            
            using (ApplicationContext db = new ApplicationContext())
            {

                Payments = db.Payments.Where(payment => payment.User == CurrentUser).ToList();
                Categories = db.Categories.ToList();
                //Categories.Insert(0, new CategoryDbContext { Id = 0, Category = "Отсутствует" });

            }
            paymentcountGrid.ItemsSource = Payments;
            categoryblock.ItemsSource = Categories;
            categoryblock.DisplayMemberPath = "Category";

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (paymentcountGrid.SelectedIndex >= 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Payments.Attach((PaymentDbContext)paymentcountGrid.SelectedItem);
                    db.Payments.Remove((PaymentDbContext)paymentcountGrid.SelectedItem);
                    db.SaveChanges();
                   Payments = db.Payments.Where(x => x.User == CurrentUser).ToList();
                   paymentcountGrid.ItemsSource = Payments;
                  
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            add add  = new add(CurrentUser);
            add.mainwindow = this;
            add.Show();
           
        }
       

        private void primarydate_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void choice_Click(object sender, RoutedEventArgs e)
        {
            DateTime primary;
            DateTime final;
            if (primarydate.SelectedDate == null)
            {
                primary = DateTime.MinValue;
                
            }
            else
            {
                primary = (DateTime)primarydate.SelectedDate;
            }
            if (finaldate.SelectedDate == null)
            {
                final = DateTime.MaxValue;

            }
            else
            {
               final = (DateTime)finaldate.SelectedDate;
            }

            if (categoryblock.SelectedIndex > 0)
            {
                paymentcountGrid.ItemsSource = Payments.Where(x=> x.Category == categoryblock.SelectedItem && x.Date > primary && x.Date < final);

            }
            else
            {
                paymentcountGrid.ItemsSource = Payments.Where(x =>  x.Date > primary && x.Date < final);
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            paymentcountGrid.ItemsSource = Payments;
        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "otchet";
            dialog.DefaultExt = ".docx";
            dialog.Filter = "Документ Microsoft Word|*.docx";
            bool? result = dialog.ShowDialog();
            zadanie1.otchet.MakeReport(dialog.FileName, Payments, CurrentUser.FIO);
        }

        }
    }



