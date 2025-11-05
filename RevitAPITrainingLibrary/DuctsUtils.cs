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

        public static List<Duct> GetDuctsInReferenceFile(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document arDocument = uIDocument.Document;
            Document ovDocument = arDocument.Application.Documents
                .OfType<Document>()
                .Where(x=>x.Title.Contains("ОВ"))
                .FirstOrDefault();

            if (ovDocument == null)                           
                return null;            

            List<Duct> ductsInReferenceFile = new FilteredElementCollector(ovDocument)
                .OfClass(typeof(Duct))
                .OfType<Duct>()
                .ToList();

            return ductsInReferenceFile;
        }

        public static List<FamilyInstance> SetOpeningsToDuctsIntersectionsWithWalls(ExternalCommandData commandData, List<Duct> ducts, View3D view3D, FamilySymbol familySymbol)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document arDocument = uIDocument.Document;
            List<FamilyInstance> familyInstances = new List<FamilyInstance>();

            ReferenceIntersector referenceIntersector = new ReferenceIntersector(new ElementClassFilter(typeof(Wall)), FindReferenceTarget.Element, view3D);
            foreach (Duct duct in ducts)
            {
                Line curve = (duct.Location as LocationCurve).Curve as Line;
                XYZ point = curve.GetEndPoint(0);
                XYZ direction = curve.Direction;

                List<ReferenceWithContext> intersections = referenceIntersector.Find(point, direction)
                    .Where(x=>x.Proximity <= curve.Length)
                    .Distinct(new ReferenceWithContextElementEqualityComparer())
                    .ToList();

                foreach (ReferenceWithContext refer in intersections)
                {
                    double proximity = refer.Proximity;
                    Reference reference = refer.GetReference();
                    Wall wall = arDocument.GetElement(reference.ElementId) as Wall;
                    Level level = arDocument.GetElement(wall.LevelId) as Level;
                    XYZ pointOpening = point + (direction * proximity);

                    FamilyInstance openings = FamilyInstanceUtils.InsertFamilyInstance(commandData, familySymbol, pointOpening, level, wall);
                    familyInstances.Add(openings);
                }
            }
            return familyInstances;
        }
    }
}
