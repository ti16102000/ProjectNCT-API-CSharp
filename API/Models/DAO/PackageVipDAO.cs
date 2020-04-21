using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class PackageVipDAO
    {
        public static bool CreatePV(PackageVip pv)
        {
            var en = new ProjectNCTEntities();
            en.PackageVips.Add(pv);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<PackageVip> GetListPV()
        {
            var en = new ProjectNCTEntities();
            return en.PackageVips.ToList() ?? null;
        }
        public static PackageVip GetPVipByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.PackageVips.Find(id);
        }
        public static bool DelPV(int id)
        {
            var en = new ProjectNCTEntities();
            en.PackageVips.Remove(en.PackageVips.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}