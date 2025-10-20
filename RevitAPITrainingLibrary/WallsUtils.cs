using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class WallsUtils
    {
        public static List<WallType> GetWallTypes(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            List<WallType> wallTypes = new FilteredElementCollector(document)
                .OfClass(typeof(WallType))
                .Cast<WallType>()
                .ToList();

            return wallTypes;
        }
    }
}
