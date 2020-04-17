using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class CategoryView
    {
        public int ID { get; set; }
        public string CateName { get; set; }
        public Nullable<int> ID_root { get; set; }
        public List<CategoryView> ListSubCate { get; set; }
    }
}