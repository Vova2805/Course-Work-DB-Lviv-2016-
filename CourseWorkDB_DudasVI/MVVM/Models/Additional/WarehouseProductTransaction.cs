using System;
using CourseWorkDB_DudasVI.General;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class WarehouseProductTransaction : ViewModelBase
    {

        private bool _isMarked = true;
        private int _quantity;
        private PRODUCT_INFO _product;
        private string _category;
        private DateTime _date;
        private bool _flowType; //true - in, false - out
        private double _moneyQuantity;

        //from warehouse
        public WarehouseProductTransaction(ORDER_PRODUCT order_product)
        {
            this.Quantity = order_product.QUANTITY_IN_ORDER;
            this.Category = order_product.PRODUCT_INFO.CATEGORY.CATEGORY_TITLE;
            this.Product = order_product.PRODUCT_INFO;
            this.Date = order_product.SALE_ORDER.REQUIRED_DATE;
            this.FlowType = false;
            MoneyQuantity = Quantity * (double)API.getlastPrice(Product.PRODUCT_PRICE).PRICE_VALUE;
        }

        //to warehouse
        public WarehouseProductTransaction(SCHEDULE_PRODUCT_INFO schedule_product)
        {
            this.Quantity = schedule_product.QUANTITY_IN_SCHEDULE;
            this.Category = schedule_product.PRODUCT_INFO.CATEGORY.CATEGORY_TITLE;
            this.Product = schedule_product.PRODUCT_INFO;
            this.Date = schedule_product.PRODUCTION_SCHEDULE.REQUIRED_DATE;
            this.FlowType = true;
            MoneyQuantity = Quantity*(double)API.getlastPrice(Product.PRODUCT_PRICE).PRICE_VALUE;
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