using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Views.UserControls;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SalerModel
    {
        public SalerModel()
        {
            #region First

            List<CLIENT> temp = Session.FactoryEntities.CLIENT.ToList();
            Clients = new List<ClientListItem>();
            ClientsTitle = new List<string>();
            foreach (var client in temp)
            {
                Clients.Add(new ClientListItem(client));
            }
            Clients.ToList().Sort((client1, client2) =>
            {
                int name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME, true);
                int surname = string.Compare(client1.Client.CLIENT_SURNAME, client2.Client.CLIENT_SURNAME, true);
                int middle_name = string.Compare(client1.Client.CLIENT_MIDDLE_NAME, client2.Client.CLIENT_MIDDLE_NAME);
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

            List<PRODUCT_INFO> tempProducts = Session.FactoryEntities.PRODUCT_INFO.ToList();
            ProductsList = new List<ProductListElement>();
            foreach (var product in tempProducts)
            {
                ProductsList.Add(new ProductListElement(product,this));
            }
            if (ProductsList.Count > 0)
            {
                selectedProduct = ProductsList.First();
            }

            #endregion

            #region Third
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
        #endregion

        #region So on
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
    }
}