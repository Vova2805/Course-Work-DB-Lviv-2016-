using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SpecialistModel
    {
        public WAREHOUSE CurrentWarehouse;
        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();


        public SpecialistModel(SWEET_FACTORYEntities FactoryEntities)
        {
            var groupedPackages =
                FactoryEntities.ORDER_PRODUCT.ToList()
                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
            foreach (var group in groupedPackages)
            {
                productPackagesList.Add(new OrderProductTransaction(group.Value, Session.User));
            }
            FromTime = API.getLastPlanDate(Session.User);
            ToTime = API.getTodayDate();
            options.Add(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString(), 0);
            OptionsList = options.Keys.ToList();
            selectedOption = OptionsList.First();
        }

        #region OrderFilter

        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, int> options = new Dictionary<string, int>();
        public List<string> OptionsList;
        public string selectedOption;

        #endregion
    }
}