using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductTransaction
    {
        public bool isChecked = true;
        public List<ORDER_PRODUCT> packages;
        public ORDER_PRODUCT packagesTotal;
        public double productPrice;
        public List<QuantityInOrder> QuantityInOrders;

        public OrderProductTransaction(List<ORDER_PRODUCT> packages, STAFF User)
        {
            QuantityInOrders = new List<QuantityInOrder>();
            this.packages = packages;
            packagesTotal = new ORDER_PRODUCT();
            packagesTotal.PRODUCT_INFO = packages.First().PRODUCT_INFO;
            packagesTotal.PRODUCT_INFO_ID = packages.First().PRODUCT_INFO_ID;
            var FromTime = API.getLastPlanDate(User);
            var ToTime = API.getTodayDate();
            //first range will be from the last production schedule
            //create time up to now
            QuantityInOrders.Add(new QuantityInOrder(FromTime, ToTime, this));
            productPrice = (double) API.getlastPrice(packagesTotal.PRODUCT_INFO.PRODUCT_PRICE);
        }


        public class QuantityInOrder
        {
            public QuantityInOrder(DateTime from, DateTime to, OrderProductTransaction parent)
            {
                From = from;
                To = to;

                var ps = API.getPackagesInRange(from, to, parent.packages);
                var sum = 0;
                foreach (var pack in ps)
                {
                    sum += pack.QUANTITY_IN_ORDER;
                }
                Quantity = sum;
            }

            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public int Quantity { get; set; }
        }
    }
}