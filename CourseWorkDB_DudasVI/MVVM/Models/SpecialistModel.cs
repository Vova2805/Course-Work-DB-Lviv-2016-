using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SpecialistModel
    {
        public List<string> CategoriesList;
        public WAREHOUSE CurrentWarehouse;
        public decimal priceFrom;
        public decimal priceTo;
        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();
        public string selectedCategory;
        public SeriesCollection LineSeriesInstance;
        public SeriesCollection PieSeriesInstance;
        public SeriesCollection BarSeriesInstance;


        public SpecialistModel(SWEET_FACTORYEntities FactoryEntities)
        {
            CategoriesList = FactoryEntities.CATEGORY.ToList().Select(c => c.CATEGORY_TITLE).ToList();
            CategoriesList.Insert(0, "Всі категорії");
            priceFrom = FactoryEntities.PRODUCT_PRICE.ToList().Min(p => p.PRICE_VALUE);
            priceTo = FactoryEntities.PRODUCT_PRICE.ToList().Max(p => p.PRICE_VALUE);
            selectedCategory = CategoriesList.First();
            var groupedPackages =
                FactoryEntities.ORDER_PRODUCT.ToList()
                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
            var i = 0;
            foreach (var group in groupedPackages)
            {
                productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value, Session.User));
            }
            FromTime = API.getLastPlanDate(Session.User);
            ToTime = API.getTodayDate();
            options.Add(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString(),
                new RegionInfo(0, FromTime, ToTime));
            OptionsList = options.Keys.ToList();
            selectedOption = OptionsList.First();
            LineSeriesInstance = new SeriesCollection();
            PieSeriesInstance = new SeriesCollection();
            BarSeriesInstance = new SeriesCollection();
            Labels = new List<string>();
        }

        public class RegionInfo
        {
            public RegionInfo(int index, DateTime from, DateTime to)
            {
                this.index = index;
                this.from = from;
                this.to = to;
            }

            public int index { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }

        #region OrderFilter

        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, RegionInfo> options = new Dictionary<string, RegionInfo>();
        public List<string> OptionsList;
        public string selectedOption;
        public bool filterByPrice = false;
        public List<string> Labels;
        public string xTitle;
        public string yTitle;

        #endregion
    }
}