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
        private ClientListItem CurrentClient;

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
    }
}