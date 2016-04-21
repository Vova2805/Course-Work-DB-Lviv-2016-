using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class DirectorModel
    {
        public List<STAFF> EmployeeList;
        public STAFF SelectedEmployee;
        //public IEnumerable<CLIENT> ClientList;
        //public IEnumerable<SALE_ORDER> OrderList;


        #region Second
        public List<ClientListItem> Clients;
        public ClientListItem SelectedClient;
        #endregion

        #region Second

        public List<ProductListElement> ProductsList;
        public List<string> ProductsTitleList;
        public string SelectedProductTitle;
        public ProductListElement SelectedProduct;
        public List<ProductPriceListElement> ProductPriceList;
        public PRODUCT_PRICE SelectedProductPrice;
        public double ProductPriceValue;
        public double ProductPricePersentage;
        public PRODUCTION_SCHEDULE CurrentProductionSchedule;

        #endregion

        #region Forth
        public List<WarehouseListItem> warehouses;
        public WarehouseListItem CurrentWarehouse;
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


        public DirectorModel()
        {
            EmployeeList = Session.FactoryEntities.STAFF.ToList().FindAll(s => s.POST.DEPARTMENT.DEPARTMENT_ID == 3);
            SelectedEmployee = EmployeeList.First();

            List<CLIENT> temp = Session.FactoryEntities.CLIENT.ToList();
            Clients = new List<ClientListItem>();
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
            if(Clients.Count>0)
            SelectedClient = Clients.First();


            #region Second

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
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();
            ProductsTitleList = ProductsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList();
            ProductsTitleList.Insert(0, "Всі продукти");
            SelectedProductTitle = ProductsTitleList.First();

            #endregion

            #region Third

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
            warehousesStrings.Insert(0,"Всі склади");
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
            ProductsOnWarehouse =
                Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();

            #endregion
        }
    }
}