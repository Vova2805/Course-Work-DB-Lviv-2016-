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