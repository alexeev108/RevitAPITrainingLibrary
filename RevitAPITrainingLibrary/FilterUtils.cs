using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class FilterUtils
    {
        public static List<ParameterFilterElement> GetFilters(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var filters = new FilteredElementCollector(document)
                .OfClass(typeof(ParameterFilterElement))
                .Cast<ParameterFilterElement>()
                .ToList();

            return filters;
        }
    }
}
