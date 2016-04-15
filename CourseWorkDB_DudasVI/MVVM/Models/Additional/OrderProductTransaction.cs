using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductTransaction
    {
        public bool isChecked = true;
        public ORDER_PRODUCT packagesTotal;
        public double productPrice;
        public List<QuantityInOrder> QuantityInOrders;
        public List<ORDER_PRODUCT> packages;

        public class QuantityInOrder
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public int Quantity { get; set; }


            public QuantityInOrder(DateTime from, DateTime to, int quantity)
            {
                From = from;
                To = to;
                Quantity = quantity;
            }
        }

        public void addPeriod(DateTime from, DateTime to)
        {
            
        }

        public void deletePeriod(int index)
        {
            
        }

        public OrderProductTransaction(List<ORDER_PRODUCT> packages, STAFF User)
        {
            QuantityInOrders = new List<QuantityInOrder>();
            this.packages = packages;
            this.packagesTotal =  new ORDER_PRODUCT();
            this.packagesTotal.PRODUCT_INFO = packages.First().PRODUCT_INFO;
            this.packagesTotal.PRODUCT_INFO_ID = packages.First().PRODUCT_INFO_ID;

            int QUANTITY_IN_ORDER = 0;
            DateTime last = API.getLastPlanDate(User);
            var p = API.getPackagesInRange(last, API.geTodayDate(), packages);
            foreach (var pack in p)
            {
                QUANTITY_IN_ORDER += pack.QUANTITY_IN_ORDER;
            }
            QuantityInOrders.Add(new QuantityInOrder(last, API.geTodayDate(), QUANTITY_IN_ORDER));
            productPrice = (double) API.getlastPrice(this.packagesTotal.PRODUCT_INFO.PRODUCT_PRICE);
        }
    }
}
