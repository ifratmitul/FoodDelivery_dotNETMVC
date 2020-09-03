using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models.ViewModels
{
    public class SubCategoryAndCategoryViewModel
    {
        public IEnumerable<Catagory> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        public  List<string> subCategoryList  { get; set; }
        public string StatusMessage { get; set; }

    }
}
