using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class ViewsUtils
    {
        public static List<ViewPlan> GetFloorPlanViews(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var views = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .Cast<ViewPlan>()
                .Where(p => p.ViewType == ViewType.FloorPlan)
                .ToList();
            return views;
        }
        public static View3D Get3DView(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document arDocument = uIDocument.Document;

            View3D view3D = new FilteredElementCollector(arDocument)
                .OfClass(typeof(View3D))
                .OfType<View3D>()
                .Where(p => !p.IsTemplate)
                .FirstOrDefault();
                
            return view3D;
        }
    }
}
