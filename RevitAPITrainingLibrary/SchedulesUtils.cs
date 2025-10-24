using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class SchedulesUtils
    {
        public static List<ViewSchedule> GetAllSchedules(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var schedules = new FilteredElementCollector(document)
                .OfClass(typeof(ViewSchedule))
                .Cast<ViewSchedule>()               
                .ToList();
            return schedules;
        }

        public static List<ViewSheet> GetAllSheets(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;                       
                
            var sheets = new FilteredElementCollector(document)
                .OfClass(typeof(ViewSheet))
                .Cast<ViewSheet>()
                .ToList();

            return sheets;                           
        }

        public static List<ViewPlan> GetAllViews(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;          
              
               
            var views = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .Cast<ViewPlan>()
                .ToList();
            return views;             
                        
           
        }
    }
}
