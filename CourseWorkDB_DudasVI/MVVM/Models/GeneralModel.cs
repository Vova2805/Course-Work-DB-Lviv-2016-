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
        public SeriesCollection LineSeriesInstance;
        public SeriesCollection PieSeriesInstance;
        public int tabIndex;

        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, CommonViewModel.RegionInfo> options = new Dictionary<string, CommonViewModel.RegionInfo>();
        public List<string> OptionsList;
        public string selectedOption;
        public bool filterByPrice = false;
        public List<string> Labels;
        public string xTitle;
        public string yTitle;


        public string ChangedText;
        public List<string> CategoriesList;
        public decimal priceFrom;
        public decimal priceTo;
        public string selectedCategory;

        public List<WarehouseListItem> warehouses;
        public WarehouseListItem CurrentWarehouse;
        public bool ExtendedMode;
        public WarehouseListItem allWarehouses;
        public decimal Engaged;
        public List<string> warehousesStrings;
        public string CurrentWarehouseString;
        public List<WarehouseProductTransaction> InOutComeFlow;
        public List<ReleasedProductListItem> ProductsOnWarehouse;
        public ReleasedProductListItem SelectedProductOnWarehouse;
        public string DateFilterString;
        public string ValueRange;
        public string TotalIncome;
        public string TotalOutcome;
        public string FlowDirection;

        public List<ProductListElement> ProductsList;
        public List<string> ProductsTitleList;
        public string SelectedProductTitle;
        public ProductListElement SelectedProduct;
        public List<ProductPriceListElement> ProductPriceList;
        public PRODUCT_PRICE SelectedProductPrice;
        public double ProductPriceValue;
        public double ProductPricePersentage;

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
        }
    }
}