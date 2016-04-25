using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SalerModel : GeneralModel
    {
        public SalerModel()
        {
            #region First

            var temp = Session.FactoryEntities.CLIENT.ToList();
            Clients = new List<ClientListItem>();
            ClientsTitle = new List<string>();
            foreach (var client in temp)
            {
                Clients.Add(new ClientListItem(client));
            }
            Clients.ToList().Sort((client1, client2) =>
            {
                var name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME, true);
                var surname = string.Compare(client1.Client.CLIENT_SURNAME, client2.Client.CLIENT_SURNAME, true);
                var middle_name = string.Compare(client1.Client.CLIENT_MIDDLE_NAME, client2.Client.CLIENT_MIDDLE_NAME);
                return surname == 0 ? name == 0 ? middle_name == 0 ? 0 : middle_name : name : surname;
            });
            foreach (var client in Clients)
            {
                ClientsTitle.Add(client.GeneralInfo);
            }
            if (Clients.Count > 0)
            {
                SelectedClient = Clients.First();
                SelectedClientTitle = ClientsTitle.First();
            }

            #endregion

            #region Second

            var tempProducts = Session.FactoryEntities.PRODUCT_INFO.ToList();
            ProductsList = new List<ProductListElement>();
            foreach (var product in tempProducts)
            {
                ProductsList.Add(new ProductListElement(product, this));
            }
            if (ProductsList.Count > 0)
            {
                selectedProduct = ProductsList.First();
            }

            #endregion

            #region Third

            WarehousesList = Session.FactoryEntities.WAREHOUSE.ToList();
            if (WarehousesList.Count > 0)
                SelectedWarehouse = WarehousesList.First();
            WarehousesListTitles = new List<string>();
            foreach (var warehouse in WarehousesList)
            {
                WarehousesListTitles.Add(API.ConvertAddress(warehouse.ADDRESS1));
            }
            if (WarehousesListTitles.Count > 0)
                SelectedWarehouseTitle = WarehousesListTitles.First();
            Distance = 0;
            var deliveries = Session.FactoryEntities.DELIVERY.ToList();
            deliveries.Sort(
                (del1, del2) =>
                {
                    return del1.DELIVERY_DATE > del2.DELIVERY_DATE
                        ? 1
                        : del1.DELIVERY_DATE == del2.DELIVERY_DATE ? 0 : -1;
                }
                );
            CostPerKM = deliveries.Last().COST_PER_KM;

            #endregion
        }

        //TabPages

        #region First

        public List<ClientListItem> Clients;
        public ClientListItem SelectedClient;

        #endregion

        #region Second

        public List<ProductListElement> ProductsList;
        public ProductListElement selectedProduct;

        #endregion

        #region Third

        public List<string> ClientsTitle;
        public string SelectedClientTitle;
        public List<WAREHOUSE> WarehousesList;
        public WAREHOUSE SelectedWarehouse;
        public List<string> WarehousesListTitles;
        public string SelectedWarehouseTitle;
        public decimal Distance;
        public decimal CostPerKM;

        #endregion

        #region So on

        #endregion

        #region OrderFilter

        public DateTime FromTime;
        public DateTime ToTime;

        public Dictionary<string, CommonViewModel.RegionInfo> options =
            new Dictionary<string, CommonViewModel.RegionInfo>();

        public List<string> OptionsList;
        public string selectedOption;
        public bool filterByPrice = false;
        public List<string> Labels;
        public string xTitle;
        public string yTitle;

        #endregion
    }
}