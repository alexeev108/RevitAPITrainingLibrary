using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class FamilyInstanceUtils
    {
        public static FamilyInstance CreateFamilyInstance(ExternalCommandData commandData, 
            FamilySymbol oFamSymb, 
            XYZ insertionPoint, 
            Level oLevel1)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FamilyInstance familyInstance = null;

            using (var ts = new Transaction(document, "Create family instance"))
            {
                ts.Start();

                if (!oFamSymb.IsActive)
                {
                    oFamSymb.Activate();
                    document.Regenerate();
                }
                familyInstance = document.Create.NewFamilyInstance(insertionPoint, 
                    oFamSymb, 
                    oLevel1, 
                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                ts.Commit();
            }
            return familyInstance;
        }

        public static FamilyInstance InsertFamilyInstance(ExternalCommandData commandData,
           FamilySymbol oFamSymb,
           XYZ insertionPoint,
           Level oLevel1,
           Wall wall)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FamilyInstance familyInstance = null;

            using (var ts = new Transaction(document, "Create family instance"))
            {
                ts.Start();

                if (!oFamSymb.IsActive)
                {
                    oFamSymb.Activate();
                    document.Regenerate();
                }
                familyInstance = document.Create.NewFamilyInstance(insertionPoint,
                    oFamSymb,
                    wall,
                    oLevel1,
                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                ts.Commit();
            }
            return familyInstance;
        }
    }
}
