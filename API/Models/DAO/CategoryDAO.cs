using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class CategoryDAO
    {
        public static bool CreateCate(Category cate)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            en.Categories.Add(cate);
            return en.SaveChanges()>0 ? true : false;
        }
        public static Category GetCateByID(int id)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Categories.Find(id) ?? null;
        }
        public static List<Category> GetListSuperCate()
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Categories.Where(w => w.ID_root == null).ToList() ?? null;
        }
        public static List<Category> GetListSubCateByID(int id)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            var r = en.Categories.Where(w => w.ID_root == id).ToList();
            return   r     ?? null;
        }
        public static List<Category> GetListSubCate()
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Categories.Where(w => w.ID_root !=null).ToList() ?? null;
        }
        public static bool UpdateCate(Category cate)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            var item = en.Categories.SingleOrDefault(s => s.ID == cate.ID);
            item.CateName = cate.CateName;
            item.ID_root = cate.ID_root;
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool DelCate(int id)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            var item = en.Categories.SingleOrDefault(s => s.ID == id);
            en.Categories.Remove(item);
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}