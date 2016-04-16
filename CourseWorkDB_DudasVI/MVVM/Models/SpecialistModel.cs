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
        public List<string> CategoriesList;
        public string selectedCategory;
        public decimal priceFrom;
        public decimal priceTo;
        #region OrderFilter

        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, RegionInfo> options = new Dictionary<string, RegionInfo>();
        public List<string> OptionsList;
        public string selectedOption;
        public bool filterByPrice = false;

        #endregion



        public SpecialistModel(SWEET_FACTORYEntities FactoryEntities)
        {
            CategoriesList = FactoryEntities.CATEGORY.ToList().Select(c=>c.CATEGORY_TITLE).ToList();
            CategoriesList.Insert(0,"Всі категорії");
            priceFrom = FactoryEntities.PRODUCT_PRICE.ToList().Min(p => p.PRICE_VALUE);
            priceTo = FactoryEntities.PRODUCT_PRICE.ToList().Max(p => p.PRICE_VALUE);
            selectedCategory = CategoriesList.First();
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
            options.Add(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString(), new RegionInfo(0, FromTime, ToTime));
            OptionsList = options.Keys.ToList();
            selectedOption = OptionsList.First();
        }

        public class RegionInfo
        {
            public int index { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }

            public RegionInfo(int index, DateTime from, DateTime to)
            {
                this.index = index;
                this.from = from;
                this.to = to;
            }
        }
    }
}