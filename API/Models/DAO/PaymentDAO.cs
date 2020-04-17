using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class PaymentDAO
    {
        public static bool CreatePayment(Payment p)
        {
            var en = new ProjectNCTEntities();
            en.Payments.Add(p);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<Payment> GetListPayment()
        {
            var en = new ProjectNCTEntities();
            return en.Payments.ToList()??null;
        }
        public static bool DelPayment(int id)
        {
            var en = new ProjectNCTEntities();
            en.Payments.Remove(en.Payments.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}