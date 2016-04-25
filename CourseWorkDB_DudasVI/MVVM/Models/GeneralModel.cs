using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class GeneralModel
    {
        public SeriesCollection BarSeriesInstance;
        public List<string> CategoriesList;


        public string ChangedText;
        public WarehouseListItem CurrentWarehouse;
        public string CurrentWarehouseString;
        public string DateFilterString;
        public decimal Engaged;
        public bool ExtendedMode;
        public bool filterByPrice = false;
        public string FlowDirection;

        public DateTime FromTime;
        public List<WarehouseProductTransaction> InOutComeFlow;
        public List<string> Labels;
        public SeriesCollection LineSeriesInstance;

        public Dictionary<string, CommonViewModel.RegionInfo> options =
            new Dictionary<string, CommonViewModel.RegionInfo>();

        public List<string> OptionsList;
        public SeriesCollection PieSeriesInstance;
        public decimal priceFrom;
        public decimal priceTo;

        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();
        public List<ProductPriceListElement> ProductPriceList;
        public double ProductPricePersentage;
        public double ProductPriceValue;

        public List<ProductListElement> ProductsList;
        public List<ReleasedProductListItem> ProductsOnWarehouse;
        public List<string> ProductsTitleList;
        public List<PRODUCTION_SCHEDULE> Schedules;
        public string selectedCategory;
        public string selectedOption;
        public ProductListElement SelectedProduct;
        public PRODUCTION_SCHEDULE SelectedProductionSchedule;
        public ReleasedProductListItem SelectedProductOnWarehouse;
        public PRODUCT_PRICE SelectedProductPrice;
        public string SelectedProductTitle;
        public int tabIndex;
        public string TotalIncome;
        public string TotalOutcome;
        public DateTime ToTime;
        public string ValueRange;

        public List<WarehouseListItem> warehouses;
        public List<string> warehousesStrings;
        public string xTitle;
        public string yTitle;

        public bool isSaler;

        public GeneralModel()
        {
            CategoriesList = Session.FactoryEntities.CATEGORY.ToList().Select(c => c.CATEGORY_TITLE).ToList();
            CategoriesList.Insert(0, "Всі категорії");
            priceFrom = Session.FactoryEntities.PRODUCT_PRICE.ToList().Min(p => p.PRICE_VALUE);
            priceTo = Session.FactoryEntities.PRODUCT_PRICE.ToList().Max(p => p.PRICE_VALUE);
            selectedCategory = CategoriesList.First();
            ChangedText = "";
            LineSeriesInstance = new SeriesCollection();
            PieSeriesInstance = new SeriesCollection();
            BarSeriesInstance = new SeriesCollection();

            FromTime = API.getLastPlanDate(Session.User);
            ToTime = API.getTodayDate();
            options.Add(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString(),
                new CommonViewModel.RegionInfo(0, FromTime, ToTime));
            OptionsList = options.Keys.ToList();
            selectedOption = OptionsList.First();
            Labels = new List<string>();
            Schedules = new List<PRODUCTION_SCHEDULE>();
            warehouses = new List<WarehouseListItem>();
            var tempWarehouses = Session.FactoryEntities.WAREHOUSE.ToList();
            foreach (var warehouse in tempWarehouses)
            {
                warehouses.Add(new WarehouseListItem(warehouse));
            }
            foreach (var warehouse in warehouses)
            {
                Schedules.AddRange(
                    Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList().
                        Where(ps => ps.WAREHOUSE_ID == warehouse.Warehouse.WAREHOUSE_ID).ToList()
                    );
            }
            if (Schedules.Count > 0)
            {
                SelectedProductionSchedule = Schedules.First();
            }

            var groupedPackages =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
            var i = 0;
            foreach (var group in groupedPackages)
            {
                productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value, Session.User));
            }
        }
    }
}