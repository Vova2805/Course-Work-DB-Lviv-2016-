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
    public class DeliveryListItem : ViewModelBaseInside
    {
        private DELIVERY _delivery;
        private DELIVERY_ADDRESS _deliveryAddress;
        private decimal _total;
        private decimal _costPerKm;
        private decimal _Kms;
        private decimal _OrderTotal;
        private bool canAddDelivery;

        public DeliveryListItem(DELIVERY delivery, decimal orderTotal)
        {
            _delivery = delivery;
            _deliveryAddress = delivery.DELIVERY_ADDRESS;
            Total = 0;
            CostPerKm = delivery.COST_PER_KM;
            Kms = DeliveryAddress.DISTANCE;
            this.OrderTotal = orderTotal;
        }

        public DELIVERY Delivery
        {
            get { return _delivery; }
            set
            {
                _delivery = value;
                OnPropertyChanged("Delivery");
            }
        }

        public bool CanAddDelivery
        {
            get { return canAddDelivery; }
            set
            {
                canAddDelivery = value;OnPropertyChanged("CanAddDelivery");
            }
        }

        public DELIVERY_ADDRESS DeliveryAddress
        {
            get { return _deliveryAddress; }
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged("DeliveryAddress");
            }
        }

        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                Delivery.DELIVERY_TOTAL = _total;
                OnPropertyChanged("Total");
            }
        }

        public decimal CostPerKm
        {
            get { return _costPerKm; }
            set
            {
                _costPerKm = value;
                Total = 0;
                Total = ((decimal) (OrderTotal < 50000 ? 0.02 : 0.01)*OrderTotal) + _Kms*_costPerKm;
                Delivery.COST_PER_KM = _costPerKm;
                OnPropertyChanged("CostPerKm");
            }
        }

        public decimal Kms
        {
            get { return _Kms; }
            set
            {
                _Kms = value;
                Total = 0;
                Total = ((decimal) (OrderTotal < 50000 ? 0.02 : 0.01)*OrderTotal) + _Kms*_costPerKm;
                DeliveryAddress.DISTANCE = _Kms;
                OnPropertyChanged("Kms");
            }
        }

        public decimal OrderTotal
        {
            get { return _OrderTotal; }
            set
            {
                _OrderTotal = value;
                Total = 0;
                Total = ((decimal) (OrderTotal < 50000 ? 0.02 : 0.01)*OrderTotal) + _Kms*_costPerKm;
                OnPropertyChanged("OrderTotal");
            }
        }

        public ICommand DeleteDelivery
        {
            get { return new RelayCommand<object>(DeleteDeliveryFunc); }
        }

        public ICommand DeleteDeliveryNewOrder
        {
            get { return new RelayCommand<object>(DeleteDeliveryNewOrderFunc); }
        }

        private async void DeleteDeliveryFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result = await metroWindow.ShowMessageAsync("Попередження", "Буде видалено доставку із замовлення.Бажаєте продовжити?",MessageDialogStyle.AffirmativeAndNegative);
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
                                    var del_address =
                                        connection.DELIVERY_ADDRESS.ToList()
                                            .Find(a => a.DEL_ADDRESS_ID == DeliveryAddress.DEL_ADDRESS_ID);
                                    connection.DELIVERY_ADDRESS.Remove(del_address);
                                    var delivery1 = connection.DELIVERY.ToList()
                                            .Find(a => a.DELIVERY_ID == Delivery.DELIVERY_ID);
                                    connection.DELIVERY.Remove(delivery1);
                                    //change order total
                                    var order =
                                        connection.SALE_ORDER.ToList()
                                            .Where(
                                                o =>
                                                    o.SALE_ORDER_ID ==
                                                    dataContext.SelectedClient.SelectedOrder.SaleOrder.SALE_ORDER_ID).FirstOrDefault();

                                    order.TOTAL -= dataContext.SelectedClient.NewDelivery.Total;
                                    var deliveries1 = order.DELIVERY.ToList();
                                    if (deliveries1.Count == 0) order.DELIVERY_STATUS = "Не замовлено";
                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();
                                    dataContext.SelectedClient.SelectedOrder.Total = order.TOTAL;
                                    await metroWindow.ShowMessageAsync("Вітання",
                                    "Зміни внесено! Доставку успішно видалено");
                                    var deliveries =
                                       connection.DELIVERY.ToList()
                                           .Where(
                                               d =>
                                                   d.SALE_ORDER_ID ==
                                                   dataContext.SelectedClient.SelectedOrder.SaleOrder.SALE_ORDER_ID).ToList();
                                    dataContext.SelectedClient.DeliveryList = new ObservableCollection<DeliveryListItem>();
                                    foreach (var delivery in deliveries)
                                    {
                                        dataContext.SelectedClient.DeliveryList.Add(new DeliveryListItem(delivery, dataContext.SelectedClient.SelectedOrder.SaleOrder.TOTAL));
                                    }
                                    dataContext.SelectedClient.DeliveryList = dataContext.SelectedClient.DeliveryList;
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();
                                    await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни.");
                                }
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни.");
                            }
                        }
                    }
                }
            }
        }

        private async void DeleteDeliveryNewOrderFunc(object obj)
        {
               //add new delivery order to current
                                var dataContext = Session.dataContext;
                                if (dataContext != null)
                                {
                                    dataContext.SelectedClient.NewOrder.Total -=
                                        dataContext.SelectedClient.NewDelivery.Total;
                                    var deliveries1 = dataContext.SelectedClient.NewOrderDeliveries.ToList();
                                    if (deliveries1.Count == 0)
                                        dataContext.SelectedClient.NewOrder.DeliveryStatus = "Не замовлено";
                dataContext.SelectedClient.NewOrderDeliveries = dataContext.SelectedClient.NewOrderDeliveries;
            }
                            
                        
        }
    }
}