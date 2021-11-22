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
    /// Логика взаимодействия для open.xaml
    /// </summary>
    public partial class open : Window
    {
        public List<UserDbContext> Userss { get; set; }
        public open()
        {
            InitializeComponent();
            using (ApplicationContext db = new ApplicationContext())

            {
                Userss = db.Users.ToList();
            }
            login.ItemsSource = Userss;
            login.DisplayMemberPath = "Login";

        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {

            UserDbContext chel;

            using (ApplicationContext db = new ApplicationContext())

            {
                chel = Userss.Find(x => x.Login == login.Text && x.Password == Security(Password.Password).ToLower());

            }
            if (chel == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            MainWindow main = new MainWindow(chel);
            main.Show();
            Close();
        }
        public static string Security(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    }
