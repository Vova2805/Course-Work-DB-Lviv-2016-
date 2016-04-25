﻿using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Resources;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SpecialistModel:GeneralModel
    {
        public SpecialistModel():base()
        {

            //Order packages
           
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();
           

            warehouses = new List<WarehouseListItem>();
            var tempWarehouses = Session.FactoryEntities.WAREHOUSE.ToList().Where(w=>w.STAFF_ID == Session.User.STAFF_ID);
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
            warehousesStrings.Insert(0, ResourceClass.ALL_WAREHOUSES);
            CurrentWarehouseString = warehousesStrings.First();


            ProductsList = new List<ProductListElement>();
            foreach (var product in Session.FactoryEntities.PRODUCT_INFO.ToList())
            {
                ProductsList.Add(new ProductListElement(product,this));
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

           
            LostProducts = 0;
            LostMoney = 0;
            Days = 7;
        }


        //TabPages
        
        public PRODUCTION_SCHEDULE CurrentProductionSchedule;
        public List<SCHEDULE_PRODUCT_INFO> schedulePackages;


        //Expired period (calculation of expenses)
        public int Days;
        public int LostProducts;
        public decimal LostMoney;



    }
}