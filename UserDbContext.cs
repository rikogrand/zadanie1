using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie1
{
    public class UserDbContext
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public List<PaymentDbContext> Payments { get; set; }
        public string Login { get; set; }
        
        public string Password { get; set; }
        public int PIN { get; set; }

    }
}
