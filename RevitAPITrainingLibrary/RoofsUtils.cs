using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class RoofsUtils
    {
        public static void CreateRoofExtrusion(ExternalCommandData commandData, Level level, List<Wall> walls)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            RoofType roofType = GetRoofType(commandData);

            double wallWidth = walls[0].Width;
            double dt = wallWidth / 2;  

            XYZ point1_dt = new XYZ(-dt, -dt, 0);
            XYZ point2_dt = new XYZ(-dt, dt, 0);

            LocationCurve locationCurve = walls[3].Location as LocationCurve;
            XYZ p1 = locationCurve.Curve.GetEndPoint(0);
            XYZ p2 = locationCurve.Curve.GetEndPoint(1);


            CurveArray curveArray = new CurveArray();
            curveArray.Append(Line.CreateBound(p1 + point1_dt, new XYZ((p1 + point1_dt).X, ((p1 + point1_dt).Y + (p2 + point2_dt).Y) / 2, 10)));
            curveArray.Append(Line.CreateBound(new XYZ((p1 + point1_dt).X, ((p1 + point1_dt).Y + (p2 + point2_dt).Y) / 2, 10), p2 + point2_dt));

            using (var ts = new Transaction(document, "Создание крыши"))
            {
                ts.Start();

                ReferencePlane referencePlane = document.Create.NewReferencePlane(
                    new XYZ(0, 0, walls[0].get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsDouble()),
                    new XYZ(0, 0, walls[0].get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsDouble() + 10),
                    new XYZ(0, 20, walls[0].get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsDouble()),
                    document.ActiveView);
                document.Create.NewExtrusionRoof(curveArray, referencePlane, level, roofType, p2.Y, walls[0].get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble());

                ts.Commit();
            }
        }

        public static RoofType GetRoofType(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            RoofType roofType = collector
                .OfClass(typeof(RoofType))                
                .OfType<RoofType>()
                .Where(x => x.Name.Equals("Типовой - 400мм"))
                .Where(x => x.FamilyName.Equals("Базовая крыша"))
                .FirstOrDefault();
            return roofType;
        }
    }
}
