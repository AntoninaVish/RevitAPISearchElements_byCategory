using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISearchElements_byCategory
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //будем искать двери в модели экземпляры семейств

            //создаем переменную типа списка
            //FilteredElementCollector позволяет собирать экземпляры семейств из модели
            List<FamilyInstance> fInstance = new FilteredElementCollector(doc)

            .OfCategory(BuiltInCategory.OST_Doors) //собираем элементы из модели, которые отностятся к категории двери
            .WhereElementIsNotElementType() //собираем экземпляры семейств, а не типы дверей
             //приводим двери к типу FamilyInstance, так как отдельного типа дверей нету
             //всю выборку приводим к даному типу FamilyInstance
             .Cast<FamilyInstance>()
             .ToList(); //создаем список

            TaskDialog.Show("Doors count", fInstance.Count.ToString());//выводим количество элементов и информацию по дверям

            return Result.Succeeded;
        }
    }
}
