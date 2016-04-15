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

        #region OrderFilter
        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, int> options = new Dictionary<string, int>();
        private KeyValuePair<string, int> selectedOption;
        #endregion

        public class QuantityInOrder
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public int Quantity { get; set; }


            public QuantityInOrder(DateTime from, DateTime to, OrderProductTransaction parent)
            {
                From = from;
                To = to;

                var ps = API.getPackagesInRange(from, to, parent.packages);
                int sum = 0;
                foreach (var pack in ps)
                {
                    sum += pack.QUANTITY_IN_ORDER;
                }
                Quantity = sum;
            }
        }

        public OrderProductTransaction(List<ORDER_PRODUCT> packages, STAFF User)
        {
            QuantityInOrders = new List<QuantityInOrder>();
            this.packages = packages;
            this.packagesTotal =  new ORDER_PRODUCT();
            this.packagesTotal.PRODUCT_INFO = packages.First().PRODUCT_INFO;
            this.packagesTotal.PRODUCT_INFO_ID = packages.First().PRODUCT_INFO_ID;
            FromTime = API.getLastPlanDate(User);
            ToTime = API.geTodayDate();
            //first range will be from the last production schedule
            //create time up to now
            QuantityInOrders.Add(new QuantityInOrder(FromTime, ToTime, this));
            options.Add(FromTime.ToLongDateString()+" - "+ ToTime.ToLongDateString(), 0);
            selectedOption = options.First();
            productPrice = (double) API.getlastPrice(this.packagesTotal.PRODUCT_INFO.PRODUCT_PRICE);
        }
    }
}
