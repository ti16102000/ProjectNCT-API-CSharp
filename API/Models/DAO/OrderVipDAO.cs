using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class OrderVipDAO
    {
        public static bool CreateOrd(OrderVip o)
        {
            o.OrdDayCreate = DateTime.Now;
            var en = new ProjectNCTEntities();
            en.OrderVips.Add(o);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<OrderVip> GetListOrd()
        {
            var en = new ProjectNCTEntities();
            return en.OrderVips.OrderByDescending(o => o.OrdDayCreate).ToList();
        }
        public static IEnumerable<OrderVip> GetListOrdByIDUser(int id)
        {
            var en = new ProjectNCTEntities();
            return en.OrderVips.Where(w=>w.UserID==id).OrderByDescending(o => o.OrdDayCreate).ToList();
        }
    }
}