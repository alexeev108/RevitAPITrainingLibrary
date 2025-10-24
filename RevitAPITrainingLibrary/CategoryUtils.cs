using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class CategoryUtils
    {
        public static List<Category> GetCategories(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var categoryList = new List<Category>();
            Categories categories = document.Settings.Categories;
            foreach (Category category in categories)
            {
                categoryList.Add(category);
            }
            return categoryList.OrderBy(c => c.Name).ToList();
        }
    }
}
