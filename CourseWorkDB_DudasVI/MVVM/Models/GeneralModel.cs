using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Resources;
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
        public bool FilterByPrice = false;
        public string FlowDirection;
        public DateTime FromTime;
        public List<WarehouseProductTransaction> InOutComeFlow;
        public List<string> Labels;
        public SeriesCollection LineSeriesInstance;

        public Dictionary<string, CommonViewModel.RegionInfo> Options =
            new Dictionary<string, CommonViewModel.RegionInfo>();

        public List<string> OptionsList;
        public SeriesCollection PieSeriesInstance;
        public decimal PriceFrom;
        public decimal PriceTo;

        public List<OrderProductTransaction> ProductPackagesList = new List<OrderProductTransaction>();
        public List<ProductPriceListElement> ProductPriceList;
        public double ProductPricePersentage;
        public double ProductPriceValue;

        public List<ProductListElement> ProductsList;
        public List<ReleasedProductListItem> ProductsOnWarehouse;
        public List<string> ProductsTitleList;
        public List<PRODUCTION_SCHEDULE> Schedules;
        public string SelectedCategory;
        public string SelectedOption;
        public ProductListElement SelectedProduct;
        public PRODUCTION_SCHEDULE SelectedProductionSchedule;
        public ReleasedProductListItem SelectedProductOnWarehouse;
        public PRODUCT_PRICE SelectedProductPrice;
        public string SelectedProductTitle;
        public int TabIndex;
        public string TotalIncome;
        public string TotalOutcome;
        public DateTime ToTime;
        public string ValueRange;

        public List<WarehouseListItem> Warehouses;
        public List<string> WarehousesStrings;
        public string XTitle;
        public string YTitle;

        public bool IsSaler;

        public List<ClientListItem> Clients;
        public ClientListItem SelectedClient;
        public List<string> ClientsTitle;
        public string SelectedClientTitle;
        public List<WAREHOUSE> WarehousesList;
        public WAREHOUSE SelectedWarehouse;
        public List<string> WarehousesListTitles;
        public string SelectedWarehouseTitle;
        public decimal Distance;
        public decimal CostPerKm;


        public PRODUCTION_SCHEDULE CurrentProductionSchedule;

        //Expired period(calculation of expenses)
        public int Days;
        public decimal LostMoney;
        public int LostProducts;
        public List<SCHEDULE_PRODUCT_INFO> SchedulePackages;


        public List<STAFF> EmployeeList;
        public STAFF SelectedEmployee;

        public GeneralModel()
        {
           
        }
    }
}