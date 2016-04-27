using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class NewOrderItem : ViewModelBaseInside
    {
        private SALE_ORDER _saleOrder;
        private ClientListItem.OrderState state;
        private bool canAddDelivery;
        private decimal total;
        private decimal paid;
        private decimal discount;
        private string orderStatus;
        private string deliveryStatus;
        private ClientListItem CurrentClient;
        private bool isSaler;
        private bool isSalerReal;
        private DateTime _requiredDate;

        private ObservableCollection<string> orderStatusStrings = new ObservableCollection<string>();
        private ObservableCollection<string> deliveryStatusStrings = new ObservableCollection<string>(); 

        public NewOrderItem(SALE_ORDER saleOrder, ClientListItem currentClient)
        {
            this.CurrentClient = currentClient;
            _saleOrder = saleOrder;
            if (SaleOrder.ORDER_STATUS == null) saleOrder.ORDER_STATUS = "Активне";
            if (saleOrder.ORDER_STATUS.Equals("Активне"))
            {
                state = ClientListItem.OrderState.Active;
            }
            else if (saleOrder.ORDER_STATUS.Equals("Реалізоване"))
            {
                state = ClientListItem.OrderState.Done;
            }
            else if (saleOrder.ORDER_STATUS.Equals("Скасоване"))
            {
                state = ClientListItem.OrderState.Canceled;
            }
            Total = saleOrder.TOTAL;
            Discount = saleOrder.DISCOUNT;
            Paid = saleOrder.PAID;
            OrderStatus = saleOrder.ORDER_STATUS;
            DeliveryStatus = saleOrder.DELIVERY_STATUS;
            OrderStatusStrings.Add("Активне"); orderStatusStrings.Add("Реалізоване"); orderStatusStrings.Add("Скасоване");
            DeliveryStatusStrings.Add("Замовлено"); deliveryStatusStrings.Add("Не замовлено");
            IsSaler = Session.userType == UserType.Saler;
            IsSalerReal = IsSaler;
            if (OrderStatus.Equals("Реалізоване")) IsSaler = false;
            RequiredDate = API.getTodayDate().AddDays(3);
        }

        public DateTime RequiredDate
        {
            get { return _requiredDate; }
            set
            {
                _requiredDate = value;
                SaleOrder.REQUIRED_DATE = _requiredDate;
                OnPropertyChanged("RequiredDate");
            }
        }

        public bool IsSaler
        {
            get { return isSaler; }
            set
            {
                isSaler = value;
                OnPropertyChanged("IsSaler");
            }
        }

        public bool IsSalerReal
        {
            get { return isSalerReal; }
            set
            {
                isSalerReal = value;
                OnPropertyChanged("IsSalerReal");
            }
        }

        public SALE_ORDER SaleOrder
        {
            get { return _saleOrder; }
            set
            {
                _saleOrder = value;
                OnPropertyChanged("SaleOrder");
            }
        }

        public decimal Total
        {
            get { return total; }
            set
            {
                total = value;
                SaleOrder.TOTAL = value;
                OnPropertyChanged("Total");
            }
        }

        public decimal Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                SaleOrder.DISCOUNT = value;
                OnPropertyChanged("Discount");
            }
        }

        public decimal Paid
        {
            get { return paid; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), out temp))
                {
                    //change in bd
                    if (paid != null && paid != value)
                        using (var connection = new SWEET_FACTORYEntities())
                        {
                            using (var dbContextTransaction = connection.Database.BeginTransaction())
                            {
                                try
                                {
                                    var order =
                                        connection.SALE_ORDER.ToList().Find(o => o.SALE_ORDER_ID == SaleOrder.SALE_ORDER_ID);
                                    if (order.PAID != value)
                                        order.PAID = value;
                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();

                                }
                                catch (Exception e)
                                {
                                    dbContextTransaction.Rollback();
                                }
                            }
                        }
                    paid = value;
                }

                OnPropertyChanged("Paid");
            }
        }

        public string DeliveryStatus
        {
            get { return deliveryStatus; }
            set
            {
                deliveryStatus = value;
                SaleOrder.DELIVERY_STATUS = deliveryStatus;
                OnPropertyChanged("DeliveryStatus");
            }
        }

        private bool changed = false;
        public string OrderStatus
        {
            get { return orderStatus; }
            set
            {
                if (OrderStatus!=null && value!=null && value.Equals("Реалізоване") && !OrderStatus.Equals(value))
                {
                    OrderStatus = null;
                    changeOrderStatus(OrderStatus);
                }
                else
                {
                    orderStatus = value;
                    SaleOrder.ORDER_STATUS = OrderStatus;
                }
                OnPropertyChanged("OrderStatus");
            }
        }

        private async void changeOrderStatus(string status)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {

                var result = await metroWindow.ShowMessageAsync("Попередження","Будуть внесені зміни щодо кількості товарів на складі. \nСтатус замовлення не можливо буде змінити.\nБажаєте продовжити? ",MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                //add new delivery order to current
                                var dataContext = metroWindow.DataContext as CommonViewModel;
                                if (dataContext != null)
                                {
                                    var order =
                                        connection.SALE_ORDER.ToList()
                                            .Find(o => o.SALE_ORDER_ID == SaleOrder.SALE_ORDER_ID);
                                    order.ORDER_STATUS = "Реалізоване";
                                    //change quantity of released product
                                    var productsList = order.ORDER_PRODUCT.ToList();
                                    //first sale older released products
                                    foreach (var product in productsList)
                                    {
                                        var releasedProducts =
                                            connection.RELEASED_PRODUCT.ToList()
                                                .Where(p => p.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID).ToList();
                                        releasedProducts.Sort( //sort by production date
                                            (pr1, pr2) =>
                                            {
                                                return pr1.PRODUCTION_DATE > pr2.PRODUCTION_DATE
                                                    ? 1
                                                    : pr1.PRODUCTION_DATE == pr2.PRODUCTION_DATE ? 0 : -1;
                                            }
                                            );
                                        int quantity = product.QUANTITY_IN_ORDER;
                                        while (quantity > 0)
                                        {
                                            var rel_product = releasedProducts.First();
                                            if (rel_product.QUANTITY <= quantity)
                                            {
                                                quantity -= rel_product.QUANTITY;
                                                releasedProducts.Remove(rel_product);
                                            }
                                            else if (rel_product.QUANTITY > quantity)
                                            {
                                                rel_product.QUANTITY -= quantity;
                                                quantity = 0;
                                            }
                                            if (releasedProducts.Count == 0)
                                            {
                                                await metroWindow.ShowMessageAsync("Невдача", "На складі не вистачило товарів. \nНе можливо реалізувати замовлення.\n Сформуйте замовлення до відділу виробництва.");
                                                throw new Exception();
                                            }
                                        }
                                    }

                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();
                                    await metroWindow.ShowMessageAsync("Вітання",
                                        "Зміни внесено! Замовлення підтверджено і реалізовано.");
                                    changed = true;
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();
                                    await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                                }
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
                if (changed)
                {
                    IsSaler = false;
                    OrderStatus = "Реалізоване";
                    SaleOrder.ORDER_STATUS = OrderStatus;
                    changed = false;
                }
                else
                {
                    OrderStatus = orderStatus;
                }
            }
        }

        public bool CanAddDelivery
        {
            get { return canAddDelivery; }
            set
            {
                canAddDelivery = value;
                if (state == ClientListItem.OrderState.Active) canAddDelivery = true;
                else
                {
                    canAddDelivery = false;
                }
                foreach (var delivery in CurrentClient.DeliveryList)
                {
                    delivery.CanAddDelivery = canAddDelivery;
                }
                OnPropertyChanged("CanAddDelivery");
            }
        }
        
        public ClientListItem.OrderState State
        {
            get { return state; }
            set
            {
                state = value;
                CanAddDelivery = canAddDelivery;
                OnPropertyChanged("State");
            }
        }

        public ICommand AddNewDelivery
        {
            get { return new RelayCommand<object>(AddNewDeliveryFuc); }
        }

        public ICommand CanselPaidChange
        {
            get { return new RelayCommand<object>(CanselPaidChangeFunc); }
        }

        public void CanselPaidChangeFunc(object obj)
        {
            Paid = SaleOrder.PAID;
        }

        public async void AddNewDeliveryFuc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                using (var connection = new SWEET_FACTORYEntities())
                {
                    using (var dbContextTransaction = connection.Database.BeginTransaction())
                    {
                        try
                        {
                            //add new delivery order to current
                            var dataContext = metroWindow.DataContext as CommonViewModel;
                            if (dataContext != null)
                            {
                                ADDRESS addr = new ADDRESS();

                                var newDeliveryAddress = dataContext.SelectedClient.NewDelivery.DeliveryAddress;
                                API.CopyAddress(ref addr, newDeliveryAddress.ADDRESS1);
                                int id = connection.ADDRESS.ToList().Max(a => a.ADDRESS_ID) + 1;
                                addr.ADDRESS_ID = id;
                                connection.ADDRESS.Add(addr);
                                connection.SaveChanges();


                                newDeliveryAddress.DELIVERY_ADDRESS_FROM = addr.ADDRESS_ID;
                                newDeliveryAddress.DELIVERY_ADDRESS_TO = newDeliveryAddress.ADDRESS.ADDRESS_ID;
                                newDeliveryAddress.ADDRESS1 = null;
                                newDeliveryAddress.DELIVERY = null;
                                newDeliveryAddress.ADDRESS = null;
                                newDeliveryAddress.DEL_ADDRESS_ID =
                                    connection.DELIVERY_ADDRESS.ToList().Max(a => a.DEL_ADDRESS_ID) + 1;
                                connection.DELIVERY_ADDRESS.Add(newDeliveryAddress);
                                connection.SaveChanges();

                                var newDelivery = dataContext.SelectedClient.NewDelivery.Delivery;

                                newDelivery.SALE_ORDER_ID = this.SaleOrder.SALE_ORDER_ID;
                                newDelivery.SALE_ORDER = null;
                                newDelivery.DEL_ADDRESS_ID = newDeliveryAddress.DEL_ADDRESS_ID;
                                newDelivery.DELIVERY_ID =
                                    connection.DELIVERY.ToList().Max(a => a.DELIVERY_ID) + 1;
                                newDelivery.DELIVERY_ADDRESS = null;
                                newDelivery.DELIVERY_DATE = API.getTodayDate();
                                newDelivery.RAWSTUFF_ORDER = null;
                                connection.DELIVERY.Add(newDelivery);
                                //change order total
                                var order =
                                    connection.SALE_ORDER.ToList()
                                        .Where(
                                            o =>
                                                o.SALE_ORDER_ID ==
                                                dataContext.SelectedClient.SelectedOrder.SaleOrder.SALE_ORDER_ID)
                                        .FirstOrDefault();

                                order.TOTAL += dataContext.SelectedClient.NewDelivery.Total;
                                order.DELIVERY_STATUS = "Замовлено";
                                dataContext.SelectedClient.SelectedOrder.Total = order.TOTAL;
                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                                await metroWindow.ShowMessageAsync("Вітання",
                                    "Зміни внесено! Дані про клієнта збережено");

                                var deliveries =
                                    connection.DELIVERY.ToList()
                                        .Where(
                                            d =>
                                                d.SALE_ORDER_ID ==
                                                dataContext.SelectedClient.SelectedOrder.SaleOrder.SALE_ORDER_ID)
                                        .ToList();
                                dataContext.SelectedClient.DeliveryList = new ObservableCollection<DeliveryListItem>();
                                foreach (var delivery in deliveries)
                                {
                                    dataContext.SelectedClient.DeliveryList.Add(new DeliveryListItem(delivery,
                                        dataContext.SelectedClient.SelectedOrder.SaleOrder.TOTAL));
                                }
                                dataContext.SelectedClient.DeliveryList = dataContext.SelectedClient.DeliveryList;
                                dataContext.SelectedClient.NewDelivery =
                                    new DeliveryListItem(dataContext.SelectedClient.InitializeDelivery(),
                                        dataContext.SelectedClient.SelectedOrder != null
                                            ? dataContext.SelectedClient.SelectedOrder.SaleOrder.TOTAL
                                            : 0);
                            }
                            else
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
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

        public ObservableCollection<string> DeliveryStatusStrings
        {
            get { return deliveryStatusStrings; }
            set
            {
                deliveryStatusStrings = value;
                OnPropertyChanged("DeliveryStatusStrings");
            }
        }

        public ObservableCollection<string> OrderStatusStrings
        {
            get { return orderStatusStrings; }
            set
            {
                orderStatusStrings = value;
                OnPropertyChanged("OrderStatusStrings");
            }
        }
    }
}