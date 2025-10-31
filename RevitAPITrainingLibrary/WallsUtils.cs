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
using System.Xml.Linq;

namespace RevitAPITrainingLibrary
{
    public class WallsUtils
    {
        public static List<Wall> CreateWalls(ExternalCommandData commandData, Level level_1, Level level_2)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            List<Wall> walls = new List<Wall>();

            using (var ts = new Transaction(document, "Создание стен"))
            {
                ts.Start();

                foreach (var curve in GetFixedCurves(commandData))
                {
                    Wall wall = Wall.Create(document, curve, level_1.Id, false);
                    wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).Set(level_2.Id);
                    walls.Add(wall);
                }

                ts.Commit();
            }
           
            return walls;
        }

        public static List<Curve> GetFixedCurves(ExternalCommandData commandData)
        {
            double width = UnitUtils.ConvertToInternalUnits(10000, UnitTypeId.Millimeters);
            double depth = UnitUtils.ConvertToInternalUnits(5000, UnitTypeId.Millimeters);
            double dx = width / 2;  
            double dy = depth / 2;

            List<XYZ> points = new List<XYZ>();
            points.Add(new XYZ(-dx, -dy, 0));
            points.Add(new XYZ(dx, -dy, 0));
            points.Add(new XYZ(dx, dy, 0));
            points.Add(new XYZ(-dx, dy, 0));
            points.Add(new XYZ(-dx, -dy, 0));

            var curves = new List<Curve>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                var nextPoint = points[i + 1];
                var currentPoint = points[i];

                Curve curve = Line.CreateBound(currentPoint, nextPoint);
                curves.Add(curve);
            }

            return curves;
        }

        public static List<Curve> GetCurvesByPoints(ExternalCommandData commandData)
        {           
            List<XYZ> points = SelectionUtils.GetPoints(commandData, "Выберите точки", ObjectSnapTypes.Endpoints);

            //if (points.Count < 2)
            //    return ;

            var curves = new List<Curve>();
            for (int i = 0; i < points.Count; i++)
            {
                if (i == 0)
                    continue;
                var prevPoint = points[i - 1];
                var currentPoint = points[i];

                Curve curve = Line.CreateBound(prevPoint, currentPoint);
                curves.Add(curve);
            }

            return curves;
        }

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

        public static XYZ CoordCenterToInsert(Wall wall)
        {
            LocationCurve locationCurve = wall.Location as LocationCurve;
            XYZ point_1 = locationCurve.Curve.GetEndPoint(0);
            XYZ point_2 = locationCurve.Curve.GetEndPoint(1);
            XYZ point = (point_1 + point_2) / 2;
            return point;
        }        
    }


}
