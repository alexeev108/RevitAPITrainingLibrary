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
        public static FamilySymbol GetDoorsFamilySymbols(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            FamilySymbol doorType = collector
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_Doors)
                .OfType<FamilySymbol>()
                .Where(x => x.Name.Equals("0915 x 2134 мм"))
                .Where(x => x.FamilyName.Equals("Одиночные-Щитовые"))
                .FirstOrDefault();
            return doorType;
        }

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

        public static List<FamilySymbol> GetSheetsFamilySymbols(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            List<FamilySymbol> familyInstances = collector
                .OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType()
                .Cast<FamilySymbol>()
                .ToList();

            return familyInstances;
        }

        public static FamilySymbol GetWindowsFamilySymbols(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            FamilySymbol windowType = collector
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_Windows)
                .OfType<FamilySymbol>()
                .Where(x => x.Name.Equals("0406 x 0610 мм"))
                .Where(x => x.FamilyName.Equals("Фиксированные"))
                .FirstOrDefault();
            return windowType;
        }

        public static FamilySymbol GetOpeningFamilySymbol(ExternalCommandData commandData)
        {
            UIApplication uiApplication = commandData.Application;
            UIDocument uIDocument = uiApplication.ActiveUIDocument;
            Document arDocument = uIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(arDocument);
            FamilySymbol openingFam = collector
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_GenericModel)
                .OfType<FamilySymbol>()                
                .Where(x => x.FamilyName.Equals("Отверстия"))
                .FirstOrDefault();
            return openingFam;
        }
    }
}
