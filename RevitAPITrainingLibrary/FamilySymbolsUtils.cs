using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class FamilySymbolsUtils
    {
        public static List<FamilySymbol> GetFamilySymbols(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            var familySymbols = new FilteredElementCollector(document)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();

            return familySymbols;
        }

        public static List<FamilySymbol> GetFurnitureFamilySymbols(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            List<FamilySymbol> familyInstances = collector
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList ();

            List <FamilySymbol> newSymbol = new List<FamilySymbol> ();
            foreach (FamilySymbol familySymbol in familyInstances)
            {
                if (familySymbol.Category.Name == "Мебель")
                    newSymbol.Add (familySymbol);
            }                
                
            return newSymbol;
        }        
    }
}
