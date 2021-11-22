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
using System.Data.Entity;


namespace zadanie1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class PaymentDbContext
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CategoryDbContext Category { get; set; }

        public string PayDis { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Cost => Quantity * Price;
        public UserDbContext User { get; set; }
        public int UserId { get; set; }
    }
}




    



