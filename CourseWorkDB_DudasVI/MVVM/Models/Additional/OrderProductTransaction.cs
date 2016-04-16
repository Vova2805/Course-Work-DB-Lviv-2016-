using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductTransaction:ViewModelBase
    {

        private bool _isChecked;
        public List<ORDER_PRODUCT> _packages;
        private ORDER_PRODUCT _packagesTotal;
        public double _productPrice;
        public List<OrderProductTransaction.QuantityInOrder> _QuantityInOrders;

        #region Properties

        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("isChecked");
            }
        }

        public ORDER_PRODUCT packagesTotal
        {
            get { return _packagesTotal; }
            set
            {
                _packagesTotal = value;
                OnPropertyChanged("packagesTotal");
            }
        }

        public double productPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value;
                OnPropertyChanged("productPrice");
            }
        }

        public List<OrderProductTransaction.QuantityInOrder> QuantityInOrders
        {
            get { return _QuantityInOrders; }
            set
            {
                _QuantityInOrders = value;
                OnPropertyChanged("QuantityInOrders");
            }
        }

        public List<ORDER_PRODUCT> packages
        {
            get { return _packages; }
            set
            {
                _packages = value;
                OnPropertyChanged("packages");
            }
        }

        #endregion

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


        public class QuantityInOrder:ViewModelBase
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

            private DateTime _From;
            private DateTime _To;
            private int _Quantity;

            public DateTime From
            {
                get { return _From; }
                set
                {
                    _From = value;
                    OnPropertyChanged("From");
                }
            }

            public DateTime To {
                get { return _To; }
                set
                {
                    _To = value;
                    OnPropertyChanged("To");
                }
            }
            public int Quantity {
                get { return _Quantity; }
                set
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
    }
}