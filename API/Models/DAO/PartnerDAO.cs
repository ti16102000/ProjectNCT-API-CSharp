using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class PartnerDAO
    {
        public static bool CreatePartner(Partner p)
        {
            p.PartnerActive = true;
            p.PartnerDayCreate = DateTime.Now;
            var en = new ProjectNCTEntities();
            en.Partners.Add(p);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static Partner GetPartnerByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Partners.SingleOrDefault(s => s.ID == id) ?? null;
        }
        public static IEnumerable<Partner> GetListPartner()
        {
            var en = new ProjectNCTEntities();
            return en.Partners.ToList() ?? null;
        }
        public static bool DelPartner(int id)
        {
            var en = new ProjectNCTEntities();
            en.Partners.Remove(en.Partners.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}