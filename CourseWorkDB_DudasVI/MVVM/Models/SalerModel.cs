using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
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
            foreach (var client in temp)
            {
                Clients.Add(new ClientListItem(client));
            }
            if(Clients.Count>0)
            SelectedClient = Clients.First();

            #endregion

            #region Second

            List<PRODUCT_INFO> tempProducts = Session.FactoryEntities.PRODUCT_INFO.ToList();
            Products = new List<Product>();
            foreach (var product in tempProducts)
            {
                Products.Add(new Product(product));
            }
            if (Products.Count > 0)
            {
                selectedProduct = Products.First();
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

        public List<Product> Products;
        public Product selectedProduct;
        #endregion

        #region Third

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