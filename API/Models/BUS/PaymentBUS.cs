using API.Models.DAO;
using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.BUS
{
    public class PaymentBUS
    {
        #region Payment
        public static bool CreatePayment(Payment p)
        {
            return PaymentDAO.CreatePayment(p);
        }
        public static IEnumerable<Payment> GetListPayment()
        {
            return PaymentDAO.GetListPayment();
        }
        public static bool DelPayment(int id)
        {
            return PaymentDAO.DelPayment(id);
        }
        #endregion

        #region PackageVip
        public static bool CreatePV(PackageVip pv)
        {
            return PackageVipDAO.CreatePV(pv);
        }
        public static IEnumerable<PackageVip> GetListPV()
        {
            return PackageVipDAO.GetListPV();
        }
        public static bool DelPV(int id)
        {
            return PackageVipDAO.DelPV(id);
        }
        #endregion
    }
}