﻿using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductTransaction : ViewModelBaseInside
    {
        private bool _isChecked = true;
        public int _Number;
        public List<ORDER_PRODUCT> _packages;
        private ORDER_PRODUCT _packagesTotal;
        public double _productPrice;
        public string _productTitle;
        public List<QuantityInOrder> _QuantityInOrders;

        public OrderProductTransaction(int number, string title, List<ORDER_PRODUCT> packages, STAFF User)
        {
            Number = number + 1;
            ProductTitle = " №" + Number + ". " + title;
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
            productPrice = (double) API.getlastPrice(packagesTotal.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE;
        }


        public class QuantityInOrder : ViewModelBaseInside
        {
            private DateTime _From;
            private int _Quantity;
            private DateTime _To;

            #region Properties

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

            public DateTime From
            {
                get { return _From; }
                set
                {
                    _From = value;
                    OnPropertyChanged("From");
                }
            }

            public DateTime To
            {
                get { return _To; }
                set
                {
                    _To = value;
                    OnPropertyChanged("To");
                }
            }

            public int Quantity
            {
                get { return _Quantity; }
                set
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }


        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("isChecked");
            }
        }

        public int Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                OnPropertyChanged("Number");
            }
        }

        public string ProductTitle
        {
            get { return _productTitle; }
            set
            {
                _productTitle = value;
                OnPropertyChanged("ProductTitle");
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

        public List<QuantityInOrder> QuantityInOrders
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
    }
}