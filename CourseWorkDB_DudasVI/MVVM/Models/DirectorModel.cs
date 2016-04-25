using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class DirectorModel:GeneralModel
    {
        public DirectorModel():base()
        {
            EmployeeList = Session.FactoryEntities.STAFF.ToList().FindAll(s => s.POST.DEPARTMENT.DEPARTMENT_ID == 3);
            SelectedEmployee = EmployeeList.First();

            var temp = Session.FactoryEntities.CLIENT.ToList();
            Clients = new List<ClientListItem>();
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
            if (Clients.Count > 0)
                SelectedClient = Clients.First();

            ProductsList = new List<ProductListElement>();
            foreach (var product in Session.FactoryEntities.PRODUCT_INFO.ToList())
            {
                ProductsList.Add(new ProductListElement(product,this));
            }
            SelectedProduct = ProductsList.First();
            SelectedProductPrice = API.getlastPrice(SelectedProduct.ProductInfo.PRODUCT_PRICE);
            ProductPriceValue = (double) SelectedProductPrice.PRICE_VALUE;
            ProductPricePersentage = (double) SelectedProductPrice.PERSENTAGE_VALUE;
            ProductPriceList = new List<ProductPriceListElement>();
            foreach (var price in SelectedProduct.ProductInfo.PRODUCT_PRICE.ToList())
            {
                ProductPriceList.Add(new ProductPriceListElement(price));
            }
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();
            ProductsTitleList = ProductsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList();
            ProductsTitleList.Insert(0, "Всі продукти");
            SelectedProductTitle = ProductsTitleList.First();

            Schedules =
                Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList()
                    .Where(ps => ps.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID)
                    .ToList();
            SelectedProductionSchedule = Schedules.First();
            schedulePackages =
                Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                    .Where(pack => pack.SCHEDULE_ID == SelectedProductionSchedule.SCHEDULE_ID).ToList();
        }

        public List<STAFF> EmployeeList;
        public STAFF SelectedEmployee;
        public List<ClientListItem> Clients;
        public ClientListItem SelectedClient;
        public PRODUCTION_SCHEDULE CurrentProductionSchedule;
        public List<PRODUCTION_SCHEDULE> Schedules;
        public PRODUCTION_SCHEDULE SelectedProductionSchedule;
        public List<SCHEDULE_PRODUCT_INFO> schedulePackages;
    }
}