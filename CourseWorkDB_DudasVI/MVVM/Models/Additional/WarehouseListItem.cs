using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class WarehouseListItem : ViewModelBaseInside
    {
        private int _ItemsQuantity;
        private PRODUCTION_SCHEDULE _newSchedule;
        private ObservableCollection<ScheduleProductInfo> _scheduleProductInfos;
        private decimal _totalPrice;
        private int _totalQuantity;
        private WAREHOUSE _warehouse;
        private CommonViewModel dataContext;
        private decimal _capacity;
        private decimal _engaged;
        private decimal _free;

        public WarehouseListItem(WAREHOUSE warehouse)
        {
            Warehouse = warehouse;
            InitializeNewSchedule();
            ItemsQuantity = 0;
            Capacity = Warehouse.CAPACITY;
            Free = Warehouse.FREE_SPACE;
            Engaged = Capacity - Free;
        }

        private void InitializeNewSchedule()
        {
            NewSchedule = new PRODUCTION_SCHEDULE();
            NewSchedule.WAREHOUSE = Warehouse;
            NewSchedule.WAREHOUSE_ID = Warehouse.WAREHOUSE_ID;
            NewSchedule.STAFF = Session.User;
            NewSchedule.STAFF_ID = Session.User.STAFF_ID;
            NewSchedule.CREATED_DATE = API.getTodayDate();
            NewSchedule.REQUIRED_DATE = API.getTodayDate();
            NewSchedule.SCHEDULE_ID = Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList().Max(p => p.SCHEDULE_ID) + 1;
            ScheduleProductInfos = new ObservableCollection<ScheduleProductInfo>();
            ItemsQuantity = 0;
            UpdateTotal();
        }

        public bool addScheduleProduct(PRODUCT_INFO product, int quantity)
        {
            try
            {
                if (dataContext == null) initialiseDataContext();
                //check if existed
                var scheduleProductInfo = new SCHEDULE_PRODUCT_INFO();
                scheduleProductInfo.PRODUCT_INFO_ID = product.PRODUCT_INFO_ID;
                scheduleProductInfo.QUANTITY_IN_SCHEDULE = quantity;
                scheduleProductInfo.PRODUCT_INFO = product;

                var existed =
                    ScheduleProductInfos.ToList().Find(s => s.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
                if (existed != null)
                {
                    existed.Quantity = quantity;
                }
                else
                {
                    initialize(scheduleProductInfo);
                    ScheduleProductInfos.Add(new ScheduleProductInfo(scheduleProductInfo, dataContext));
                    //fire sync
                    ScheduleProductInfos.Last().Quantity = ScheduleProductInfos.Last().Quantity;
                }
                //end
                UpdateTotal();
                ScheduleProductInfos = _scheduleProductInfos;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateTotal()
        {
            TotalPrice = 0;
            TotalQuantity = 0;
            foreach (var prodInfo in _scheduleProductInfos)
            {
                TotalQuantity += prodInfo.Quantity;
                TotalPrice += API.getlastPrice(prodInfo.ProductInfo.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE*
                              prodInfo.Quantity;
            }
        }

        

        public bool removeScheduleProduct(PRODUCT_INFO product)
        {
            var scheduleProductInfo =
                ScheduleProductInfos.ToList().Find(s => s.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            if (scheduleProductInfo != null)
                if (ScheduleProductInfos.Remove(scheduleProductInfo))
                {
                    UpdateTotal();
                    ScheduleProductInfos = _scheduleProductInfos;
                    return true;
                }
            return false;
        }

        public bool removeScheduleProduct(SCHEDULE_PRODUCT_INFO product)
        {
            var prod =
                ScheduleProductInfos.ToList().Find(pr => pr.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            if (ScheduleProductInfos.Remove(prod))
            {
                UpdateTotal();
                ScheduleProductInfos = _scheduleProductInfos;
                return true;
            }
            return false;
        }

        private void initialize(SCHEDULE_PRODUCT_INFO product)
        {
            product.SCHEDULE_ID = _newSchedule.SCHEDULE_ID;
            product.RELEASED_QUANTITY = 0;
        }

        public bool Contains(PRODUCT_INFO product)
        {
            var elem =
                ScheduleProductInfos.ToList().Find(e => e.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            return elem == null ? false : true;
        }

        public ScheduleProductInfo ContainsProductInfo(PRODUCT_INFO product)
        {
            var elem =
                ScheduleProductInfos.ToList().Find(e => e.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            return elem;
        }

        private void initialiseDataContext()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                dataContext = specialistWindow.DataContext as CommonViewModel;
            }
        }

        public class ScheduleProductInfo : ViewModelBaseInside
        {
            private readonly CommonViewModel dataContext;
            private SCHEDULE_PRODUCT_INFO _ProductInfo;
            private int _Quantity;

            public ScheduleProductInfo(SCHEDULE_PRODUCT_INFO productInfo, CommonViewModel dataContext)
            {
                this.dataContext = dataContext;
                _ProductInfo = productInfo;
                _Quantity = productInfo.QUANTITY_IN_SCHEDULE;
            }

            public SCHEDULE_PRODUCT_INFO ProductInfo
            {
                get { return _ProductInfo; }
                set
                {
                    _ProductInfo = value;
                    OnPropertyChanged("ProductInfo");
                }
            }

            public int Quantity
            {
                get { return _Quantity; }
                set
                {
                    _Quantity = value;
                    ProductInfo.QUANTITY_IN_SCHEDULE = _Quantity;
                    SyncQuantity(dataContext, _ProductInfo.PRODUCT_INFO, _Quantity);
                    OnPropertyChanged("Quantity");
                }
            }

            public ICommand RemoveScheduleItem
            {
                get { return new RelayCommand<string>(RemoveScheduleItemFuct); }
            }
            private void SyncQuantity(CommonViewModel dataContext, PRODUCT_INFO product, int quantity)
            {
                //sync quantity
                var ProductListItem =
                    dataContext.ProductsList.ToList()
                        .Find(pr => pr.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
                var ReleasedProductItem =
                    dataContext.ProductsOnWarehouse.ToList()
                        .Find(pr => pr.ReleasedProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
                if (ProductListItem != null && ProductListItem.QuantityNeeded != ProductListItem.Quantity + quantity)
                    ProductListItem.QuantityNeeded = ProductListItem.Quantity + quantity;
                if (ReleasedProductItem != null &&
                    ReleasedProductItem.QuantityNeeded != ReleasedProductItem.Quantity + quantity)
                    ReleasedProductItem.QuantityNeeded = ReleasedProductItem.Quantity + quantity;
            }

            public void RemoveScheduleItemFuct(object obj)
            {
                Quantity = 0; // remove from schedule
            }
        }
       
        #region Properties

        public PRODUCTION_SCHEDULE NewSchedule
        {
            get { return _newSchedule; }
            set
            {
                _newSchedule = value;
                OnPropertyChanged("NewSchedule");
            }
        }

        public WAREHOUSE Warehouse
        {
            get { return _warehouse; }
            set
            {
                _warehouse = value;
                OnPropertyChanged("Warehouse");
            }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                _totalQuantity = value;
                OnPropertyChanged("TotalQuantity");
            }
        }

        public ObservableCollection<ScheduleProductInfo> ScheduleProductInfos
        {
            get { return _scheduleProductInfos; }
            set
            {
                _scheduleProductInfos = value;
                OnPropertyChanged("ScheduleProductInfos");
                OnPropertyChanged("TotalQuantity");
                OnPropertyChanged("TotalPrice");
            }
        }

        public int ItemsQuantity
        {
            get { return _ItemsQuantity; }
            set
            {
                _ItemsQuantity = value;
                OnPropertyChanged("ItemsQuantity");
            }
        }
        public decimal Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                OnPropertyChanged("Capacity");
            }
        }

        public decimal Engaged
        {
            get { return _engaged; }
            set
            {
                _engaged = value;
                OnPropertyChanged("Engaged");
            }
        }

        public decimal Free
        {
            get { return _free; }
            set
            {
                _free = value;
                OnPropertyChanged("Free");
            }
        }

        public ICommand ClearSchedule
        {
            get { return new RelayCommand<string>(ClearScheduleFunc); }
        }

        public ICommand SaveSchedule
        {
            get { return new RelayCommand<string>(SaveScheduleScheduleFunc); }
        }

        public async void ClearScheduleFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result = await metroWindow.ShowMessageAsync("Підтвердження",
                            "Весь поточний план виробництва буде очищено. Бажаєте продовжити?",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    while (ScheduleProductInfos.Count > 0)
                    {
                        ScheduleProductInfos.ElementAt(0).Quantity = 0;
                    }
                }
            }
        }
        public async void SaveScheduleScheduleFunc(object obj)
        {
            //saver schedule to db
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result = await metroWindow.ShowMessageAsync("Зьереження", "Зберегти поточний план?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                               var productionSchedule = new PRODUCTION_SCHEDULE();
                                NewSchedule.SCHEDULE_ID =
                                    connection.PRODUCTION_SCHEDULE.ToList().Max(s => s.SCHEDULE_ID) + 1;
                                NewSchedule.STAFF_ID = Session.User.STAFF_ID;
                                NewSchedule.CREATED_DATE = API.getTodayDate();
                                NewSchedule.SCHEDULE_STATE = "Активний";
                                NewSchedule.WAREHOUSE_ID = this.Warehouse.WAREHOUSE_ID;
                                NewSchedule.SCHEDULE_TOTAL = TotalPrice;
                                CopyProductionSchedule(ref productionSchedule, NewSchedule);
                                
                                connection.PRODUCTION_SCHEDULE.Add(productionSchedule);
                                connection.SaveChanges();
                                foreach (var product in ScheduleProductInfos)
                                    {
                                        SCHEDULE_PRODUCT_INFO ProductInfo = new SCHEDULE_PRODUCT_INFO();
                                        product.ProductInfo.SCHEDULE_PRODUCT_INFO_ID =
                                            connection.SCHEDULE_PRODUCT_INFO.ToList()
                                                .Max(s => s.SCHEDULE_PRODUCT_INFO_ID) + 1;
                                        product.ProductInfo.SCHEDULE_ID = productionSchedule.SCHEDULE_ID;
                                        product.ProductInfo.QUANTITY_IN_SCHEDULE = product.Quantity;
                                        product.ProductInfo.PRODUCT_INFO_ID =
                                            product.ProductInfo.PRODUCT_INFO.PRODUCT_INFO_ID;
                                        product.ProductInfo.RELEASED_QUANTITY = 0;
                                        CopyScheduleProductInfo(ref ProductInfo, product.ProductInfo);
                                        connection.SCHEDULE_PRODUCT_INFO.Add(ProductInfo);
                                        connection.SaveChanges();
                                    }

                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                                InitializeNewSchedule();
                                await metroWindow.ShowMessageAsync("Вітання",
                                        "Зміни внесено! Новий план виробництва збережено");
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
        }


        private void CopyScheduleProductInfo(ref SCHEDULE_PRODUCT_INFO one, SCHEDULE_PRODUCT_INFO two)
        {
            one.SCHEDULE_PRODUCT_INFO_ID = two.SCHEDULE_PRODUCT_INFO_ID;
            one.PRODUCT_INFO_ID = two.PRODUCT_INFO_ID;
            one.SCHEDULE_ID = two.SCHEDULE_ID;
            one.QUANTITY_IN_SCHEDULE = two.QUANTITY_IN_SCHEDULE;
            one.RELEASED_QUANTITY = two.RELEASED_QUANTITY;
        }

        private void CopyProductionSchedule(ref PRODUCTION_SCHEDULE one, PRODUCTION_SCHEDULE two)
        {
            one.SCHEDULE_ID = two.SCHEDULE_ID;
            one.REQUIRED_DATE = two.REQUIRED_DATE;
            one.CREATED_DATE = two.CREATED_DATE;
            one.STAFF_ID = two.STAFF_ID;
            one.SCHEDULE_STATE = two.SCHEDULE_STATE;
            one.SCHEDULE_TOTAL = two.SCHEDULE_TOTAL;
            one.WAREHOUSE_ID = two.WAREHOUSE_ID;
        }
        #endregion
    }
}