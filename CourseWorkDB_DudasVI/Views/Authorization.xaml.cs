using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CourseWorkDB_DudasVI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeOpacityLoginDetails(object sender, RoutedEventArgs e)
        {
            LoginDetails.Opacity = 0;
        }

        public void ChangeOpacityProgressRing(object sender, RoutedEventArgs e)
        {
            ProgressRing.Opacity = 100;
        }

        public void ChangeOpacityLoginDetailsB(object sender, RoutedEventArgs e)
        {
            LoginDetails.Opacity = 100;
        }

        public void ChangeOpacityProgressRingB(object sender, RoutedEventArgs e)
        {
            ProgressRing.Opacity = 0;
        }

        private async void SubmitClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                LoginDetails.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<RoutedEventArgs>(ChangeOpacityLoginDetails), sender, e);
                ProgressRing.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<RoutedEventArgs>(ChangeOpacityProgressRing), sender, e);
            });
            //thread.Start();

            var sweetFactory = new SWEET_FACTORYEntities();
            LoginBlock.Text = "specialist_test";
            PassBlock.Password = "test";

            var resultUser =
                sweetFactory.STAFF.ToList()
                    .SingleOrDefault(
                        user => user.LOGIN.Equals(LoginBlock.Text) && user.PASSWORD.Equals(PassBlock.Password));

            if (resultUser != null)
            {
                //switch (resultUser.POST_ID)
                //{
                //    case 1:
                //        {
                //            HomeWindowAdmin homeWindow = new HomeWindowAdmin();
                //            homeWindow.Show();

                //            Session.User = resultUser;
                //            thread.Interrupt();
                //            this.Close();
                //        }
                //        break;
                //    case 2:
                //        {
                //            HomeWindowSale homeWindowSale = new HomeWindowSale();
                //            homeWindowSale.Show();

                //            Session.User = resultUser;
                //            thread.Interrupt();
                //            this.Close();
                //        }
                //        break;
                //    case 3:
                //        {
                //            HomeWindowSpecialist homeWindowSpecialist = new HomeWindowSpecialist();
                //            homeWindowSpecialist.Show();

                //            Session.User = resultUser;
                //            thread.Interrupt();
                //            this.Close();
                //        }
                //        break;
                //    default:
                //        {
                //            await this.ShowMessageAsync("Помилка", "Не правильний пароль чи логін. Перевірте введені дані і спробуйте знову");
                //            Back(sender,e);
                //        }
                //        break;
                //}
                Session.User = resultUser;
                var homeWindowSpecialist = new HomeWindowSpecialist();
                homeWindowSpecialist.Show();
                //HomeWindowAdmin homeWindow = new HomeWindowAdmin();
                //homeWindow.Show();

                //thread.Interrupt();
                //Back(sender, e);
                Close();
            }
            else
            {
                await
                    this.ShowMessageAsync("Помилка",
                        "Не правильний пароль чи логін. Перевірте введені дані і спробуйте знову");
                Back(sender, e);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var thread1 = new Thread(() =>
            {
                LoginDetails.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<RoutedEventArgs>(ChangeOpacityLoginDetailsB), sender, e);
                ProgressRing.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<RoutedEventArgs>(ChangeOpacityProgressRingB), sender, e);
            });
            thread1.Start();
        }
    }
}