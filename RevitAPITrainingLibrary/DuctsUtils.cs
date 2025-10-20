using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class DuctsUtils
    {
        public static List<DuctType> GetDuctTypes(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            List<DuctType> ductTypes = new FilteredElementCollector(document)
                .OfClass(typeof(DuctType))
                .Cast<DuctType>()
                .ToList();

            return ductTypes;
        }
    }
}
