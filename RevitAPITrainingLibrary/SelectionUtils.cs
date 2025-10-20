using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class SelectionUtils
    {
        public static Element PickObject(ExternalCommandData commandData, string message = "Выберете элемент")
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var selectedRef = uIDocument.Selection.PickObject(ObjectType.Element, message);
            var selectedElement = document.GetElement(selectedRef);
            return selectedElement;
        }

        public static List<Element> PickObjects(ExternalCommandData commandData, string message = "Выберете элементы")
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var selectedRef = uIDocument.Selection.PickObjects(ObjectType.Element, message);
            List<Element> elementList = selectedRef.Select(selectedObject => document.GetElement(selectedObject)).ToList();
            return elementList;
        }

        public static List<Element> PipesSelection(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var pipes = new FilteredElementCollector(document)
                .OfClass(typeof(Pipe))
                .Cast<Element>()
                .ToList();
            return pipes;
        }

        public static List<Element> WallsSelection(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var walls = new FilteredElementCollector(document)
                .OfClass(typeof(Wall))
                .Cast<Element>()
                .ToList();
            return walls;
        }

        public static List<Element> DoorsSelection(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var doors = new FilteredElementCollector(document)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .Cast<Element>()
                .ToList();
            return doors;
        }

        public static List<XYZ> GetPoints(ExternalCommandData commandData, 
            string promtMessage, ObjectSnapTypes objectSnapTypes)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            
            List<XYZ> points = new List<XYZ>();

            while (true)
            {
                XYZ pickedPoint = null;
                try
                {
                    pickedPoint = uIDocument.Selection.PickPoint(objectSnapTypes, promtMessage);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    break;                    
                }
                points.Add(pickedPoint);
            }
            return points;
        }

        public static T GetObject<T>(ExternalCommandData commandData, string promtMessage)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;
            Reference selectedObject = null;
            T elem;
            try
            {
                selectedObject = uIDocument.Selection.PickObject(ObjectType.Element, promtMessage);
            }
            catch (Exception)
            {
                return default(T);
            }
            elem = (T)(Object)document.GetElement(selectedObject.ElementId);
            return elem;
        }

        public static XYZ GetPoint(ExternalCommandData commandData, string promtMessage, ObjectSnapTypes objectSnapTypes)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
                        
            XYZ pickedPoint = uIDocument.Selection.PickPoint(objectSnapTypes, promtMessage);
                
            return pickedPoint;
        }
    }
}
