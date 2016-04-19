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
        public SeriesCollection BarSeriesInstance;
        public SeriesCollection LineSeriesInstance;
        public SeriesCollection PieSeriesInstance;
        public int tabIndex;


        public SpecialistModel()
        {
            LineSeriesInstance = new SeriesCollection();
            PieSeriesInstance = new SeriesCollection();
            BarSeriesInstance = new SeriesCollection();
            Labels = new List<string>();

            #region First

            CategoriesList = Session.FactoryEntities.CATEGORY.ToList().Select(c => c.CATEGORY_TITLE).ToList();
            CategoriesList.Insert(0, "Всі категорії");
            priceFrom = Session.FactoryEntities.PRODUCT_PRICE.ToList().Min(p => p.PRICE_VALUE);
            priceTo = Session.FactoryEntities.PRODUCT_PRICE.ToList().Max(p => p.PRICE_VALUE);
            selectedCategory = CategoriesList.First();
            var groupedPackages =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
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

            #endregion

            #region Second

            ProductsList = new List<ProductListElement>();
            foreach (var product in Session.FactoryEntities.PRODUCT_INFO.ToList())
            {
                ProductsList.Add(new ProductListElement(product));
            }
            SelectedProduct = ProductsList.First().ProductInfo;
            SelectedProductPrice = API.getlastPrice(SelectedProduct.PRODUCT_PRICE);
            ProductPriceValue = (double) SelectedProductPrice.PRICE_VALUE;
            ProductPricePersentage = (double) SelectedProductPrice.PERSENTAGE_VALUE;
            ProductPriceList = new List<ProductPriceListElement>();
            foreach (var price in SelectedProduct.PRODUCT_PRICE.ToList())
            {
                ProductPriceList.Add(new ProductPriceListElement(price));
            }
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();
            ProductsTitleList = ProductsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList();
            ProductsTitleList.Insert(0,"Всі продукти");
            SelectedProductTitle = ProductsTitleList.First();

            #endregion

            #region Third

            warehouses = Session.FactoryEntities.WAREHOUSE.ToList();
            CurrentWarehouse = warehouses.First();
            warehousesStrings = new List<string>();
            i = 0;
            foreach (var warehouse in warehouses)
            {
                warehousesStrings.Add(ConvertAddress(warehouse,(++i).ToString()+"."));
            }
            CurrentWarehouseString = warehousesStrings.First();
            InOutComeFlow = new List<WarehouseProductTransaction>();
            List<ORDER_PRODUCT> order_products =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                    .Where(op => op.WAREHOUSE_ID == CurrentWarehouse.WAREHOUSE_ID).ToList();
            List<SCHEDULE_PRODUCT_INFO> scheduleProductInfos = Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                .Where(psi => psi.PRODUCTION_SCHEDULE.WAREHOUSE_ID == CurrentWarehouse.WAREHOUSE_ID).ToList();
            foreach (var package in order_products)
            {
                InOutComeFlow.Add(new WarehouseProductTransaction(package));
            }
            foreach (var package in scheduleProductInfos)
            {
                InOutComeFlow.Add(new WarehouseProductTransaction(package));
            }
            InOutComeFlow.Sort((transaction1, transaction2) =>
            {
                return transaction1.Date > transaction2.Date ? 1 : transaction1.Date == transaction2.Date ? 0 : -1;
            } );
            ProductsOnWarehouse =
                Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.WAREHOUSE_ID).ToList();

            #endregion
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

        //TabPages

        #region First

        public List<string> CategoriesList;
        public decimal priceFrom;
        public decimal priceTo;
        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();
        public string selectedCategory;

        #endregion

        #region Second

        public List<ProductListElement> ProductsList;
        public List<string> ProductsTitleList;
        public string SelectedProductTitle;
        public PRODUCT_INFO SelectedProduct;
        public List<ProductPriceListElement> ProductPriceList;
        public PRODUCT_PRICE SelectedProductPrice;
        public double ProductPriceValue;
        public double ProductPricePersentage;
        public PRODUCTION_SCHEDULE CurrentProductionSchedule;

        #endregion

        #region Third

        public List<WAREHOUSE> warehouses;
        public WAREHOUSE CurrentWarehouse;
        public decimal Engaged;
        public List<string> warehousesStrings;
        public string CurrentWarehouseString;
        public List<WarehouseProductTransaction> InOutComeFlow;
        public string DateFilterString;
        public string ValueRange;
        public string TotalIncome;
        public string TotalOutcome;
        public string FlowDirection;
        public List<RELEASED_PRODUCT> ProductsOnWarehouse;
        #endregion

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
        public static string ConvertAddress(WAREHOUSE warehouse, string number = "")
        {
            ADDRESS address = warehouse.ADDRESS1;
            string response = "";
            if (address != null)
            {
                response = number+" "+address.COUNTRY + ", " + address.REGION + ", " + address.CITY + ", " + address.STREET + ", " +
                           address.BUILDING_NUMBER;
            }
            return response;
        }
    }
}

#region Third

#endregion