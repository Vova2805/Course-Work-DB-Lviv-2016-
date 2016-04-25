using System;
using CourseWorkDB_DudasVI.General;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class WarehouseProductTransaction : ViewModelBaseInside
    {
        private string _category;
        private DateTime _date;
        private bool _flowType; //true - in, false - out

        private bool _isMarked = true;
        private double _moneyQuantity;
        private PRODUCT_INFO _product;
        private int _quantity;

        //from warehouse
        public WarehouseProductTransaction(ORDER_PRODUCT order_product)
        {
            Quantity = order_product.QUANTITY_IN_ORDER;
            Category = order_product.PRODUCT_INFO.CATEGORY.CATEGORY_TITLE;
            Product = order_product.PRODUCT_INFO;
            Date = order_product.SALE_ORDER.REQUIRED_DATE;
            FlowType = false;
            MoneyQuantity = Quantity*(double) API.getlastPrice(Product.PRODUCT_PRICE).PRICE_VALUE;
        }

        //to warehouse
        public WarehouseProductTransaction(SCHEDULE_PRODUCT_INFO schedule_product)
        {
            Quantity = schedule_product.QUANTITY_IN_SCHEDULE;
            Category = schedule_product.PRODUCT_INFO.CATEGORY.CATEGORY_TITLE;
            Product = schedule_product.PRODUCT_INFO;
            Date = schedule_product.PRODUCTION_SCHEDULE.REQUIRED_DATE;
            FlowType = true;
            MoneyQuantity = Quantity*(double) API.getlastPrice(Product.PRODUCT_PRICE).PRICE_VALUE;
        }

        #region Properties

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public bool FlowType
        {
            get { return _flowType; }
            set
            {
                _flowType = value;
                OnPropertyChanged("FlowType");
            }
        }

        public bool IsMarked
        {
            get { return _isMarked; }
            set
            {
                _isMarked = value;
                OnPropertyChanged("IsMarked");
            }
        }

        public PRODUCT_INFO Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public double MoneyQuantity
        {
            get { return _moneyQuantity; }
            set
            {
                _moneyQuantity = value;
                OnPropertyChanged("MoneyQuantity");
            }
        }

        #endregion
    }
}