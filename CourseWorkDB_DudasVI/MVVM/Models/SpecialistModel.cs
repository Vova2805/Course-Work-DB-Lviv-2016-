using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SpecialistModel:GeneralModel
    {
       


        public SpecialistModel():base()
        {

            //Order packages
            var groupedPackages =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
            var i = 0;
            foreach (var group in groupedPackages)
            {
                productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value, Session.User));
            }
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();

            Schedules =
                Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList()
                    .Where(ps => ps.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID)
                    .ToList();
            SelectedProductionSchedule = Schedules.First();
            schedulePackages =
                Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                    .Where(pack => pack.SCHEDULE_ID == SelectedProductionSchedule.SCHEDULE_ID).ToList();
            ProductsList = new List<ProductListElement>();
            foreach (var product in Session.FactoryEntities.PRODUCT_INFO.ToList())
            {
                ProductsList.Add(new ProductListElement(product, this));
            }
            SelectedProduct = ProductsList.First();
            SelectedProductPrice = API.getlastPrice(SelectedProduct.ProductInfo.PRODUCT_PRICE);
            ProductPriceValue = (double)SelectedProductPrice.PRICE_VALUE;
            ProductPricePersentage = (double)SelectedProductPrice.PERSENTAGE_VALUE;
            ProductPriceList = new List<ProductPriceListElement>();
            foreach (var price in SelectedProduct.ProductInfo.PRODUCT_PRICE.ToList())
            {
                ProductPriceList.Add(new ProductPriceListElement(price));
            }

            ProductsTitleList = ProductsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList();
            ProductsTitleList.Insert(0, "Всі продукти");
            SelectedProductTitle = ProductsTitleList.First();
        }


        //TabPages
        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();
        public PRODUCTION_SCHEDULE CurrentProductionSchedule;
        public List<PRODUCTION_SCHEDULE> Schedules;
        public PRODUCTION_SCHEDULE SelectedProductionSchedule;
        public List<SCHEDULE_PRODUCT_INFO> schedulePackages;

       
    }
}