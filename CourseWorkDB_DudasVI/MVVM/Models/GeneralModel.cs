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

            warehouses = new List<WarehouseListItem>();
            var tempWarehouses = Session.FactoryEntities.WAREHOUSE.ToList();
            foreach (var warehouse in tempWarehouses)
            {
                warehouses.Add(new WarehouseListItem(warehouse));
            }

            CurrentWarehouse = warehouses.First();
            warehousesStrings = new List<string>();
            int i = 0;
            foreach (var warehouse in warehouses)
            {
                warehousesStrings.Add(API.ConvertAddress(warehouse.Warehouse.ADDRESS1, ++i + "."));
            }
            warehousesStrings.Insert(0, "Всі склади");
            CurrentWarehouseString = warehousesStrings.First();
            InOutComeFlow = new List<WarehouseProductTransaction>();
            var order_products =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                    .Where(op => op.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
            var scheduleProductInfos = Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                .Where(psi => psi.PRODUCTION_SCHEDULE.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
            foreach (var package in order_products)
            {
                InOutComeFlow.Add(new WarehouseProductTransaction(package));
            }
            foreach (var package in scheduleProductInfos)
            {
                InOutComeFlow.Add(new WarehouseProductTransaction(package));
            }
            InOutComeFlow.Sort(
                (transaction1, transaction2) =>
                {
                    return transaction1.Date > transaction2.Date ? 1 : transaction1.Date == transaction2.Date ? 0 : -1;
                });
            var productsOnWarehouse =
                Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
            var distinctProduct = productsOnWarehouse.GroupBy(p => p.PRODUCT_INFO.PRODUCT_TITLE).ToDictionary(group => group.Key, group => group.ToList());
            this.ProductsOnWarehouse = new List<ReleasedProductListItem>();
            foreach (var product in distinctProduct)
            {
                var releasedProduct = new ReleasedProductListItem(product.Value.First());
                foreach (var item in product.Value)
                {
                    releasedProduct.Quantity += item.QUANTITY;
                }
                this.ProductsOnWarehouse.Add(releasedProduct);
            }
            if (productsOnWarehouse.Count > 0)
            {
                this.SelectedProductOnWarehouse = this.ProductsOnWarehouse.First();
            }

            
        }
    }
}