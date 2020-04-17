using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class QualityDAO
    {
        public static bool CreateQuality(Quality q)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            en.Qualities.Add(q);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static Quality GetQualityByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Qualities.Find(id) ?? null;
        }
        public static IEnumerable<Quality> GetListQuality()
        {
            var en = new ProjectNCTEntities();
            return en.Qualities.ToList() ?? null;
        }
        public static bool UpdateQuality(Quality q)
        {
            var en = new ProjectNCTEntities();
            var item = en.Qualities.SingleOrDefault(s => s.ID == q.ID);
            item.QualityName = q.QualityName;
            item.QualityVip = q.QualityVip;
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}