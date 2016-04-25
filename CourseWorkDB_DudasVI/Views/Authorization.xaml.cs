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
    public partial class Authorization : MetroWindow
    {
        public Authorization()
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

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            //var thread = new Thread(() =>
            //{
            //    LoginDetails.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            //        new EventHandler<RoutedEventArgs>(ChangeOpacityLoginDetails), sender, e);
            //    ProgressRing.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            //        new EventHandler<RoutedEventArgs>(ChangeOpacityProgressRing), sender, e);
            //});
            //thread.Start();
            var shoice = LoginBlock.Text;
            switch (shoice)
            {
                case "1":
                {
                    LoginBlock.Text = "director_test";
                }
                    break;
                case "3":
                {
                    LoginBlock.Text = "specialist_test";
                }
                    break;
                case "2":
                {
                    LoginBlock.Text = "saler_test";
                }
                    break;
            }

            PassBlock.Password = "test";
            STAFF resultUser = null;
            resultUser =
                Session.FactoryEntities.STAFF.ToList()
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
                MetroWindow currentMetroWindow = null;
                switch (shoice)
                {
                    case "1":
                    {
                        Session.userType = UserType.Director;
                        currentMetroWindow = new HomeWindowAdmin();
                        currentMetroWindow.Show();
                    }
                        break;
                    case "3":
                    {
                        Session.userType = UserType.Specialist;
                        currentMetroWindow = new HomeWindowSpecialist();
                        currentMetroWindow.Show();
                    }
                        break;
                    case "2":
                    {
                        Session.userType = UserType.Saler;
                        currentMetroWindow = new HomeWindowSale();
                        currentMetroWindow.Show();
                    }
                        break;
                }
                //thread.Interrupt();
                //Back(sender, e);
                Close();
            }
            else
            {
                Error();
            }
        }

        private async void Error()
        {
            await
                this.ShowMessageAsync("Помилка",
                    "Не правильний логін чи пароль. Перевірте введені дані і спробуйте знову");
            Back(null, null);
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