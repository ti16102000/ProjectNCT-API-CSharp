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
        public static PackageVip GetPVipByID(int id)
        {
            return PackageVipDAO.GetPVipByID(id);
        }
        #endregion

        #region Order
        public static bool CreateOrd(OrderVip o)
        {
            var package = GetPVipByID(o.PVipID??0);
            UpdateVip(o.UserID, package.PVipMonths);
            return OrderVipDAO.CreateOrd(o);
        }
        public static IEnumerable<OrderVip> GetListOrd()
        {
            return OrderVipDAO.GetListOrd();
        }
        public static IEnumerable<OrderVip> GetListOrdByIDUser(int id)
        {
            return OrderVipDAO.GetListOrdByIDUser(id);
        }
        #endregion

        #region User
        public static bool UpdateVip(int idUser, int month)
        {
            return UserDAO.UpdateVip(idUser, month);
        }
        #endregion
    }
}