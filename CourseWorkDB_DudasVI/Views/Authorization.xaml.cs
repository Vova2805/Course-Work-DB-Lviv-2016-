using System;
using System.Linq;
using System.Threading;
using System.Windows.Media.Animation;
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

        public void ChangeOpacityLoginDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            LoginDetails.Opacity = 0;
        }
        public void ChangeOpacityProgressRing(object sender, System.Windows.RoutedEventArgs e)
        {
             ProgressRing.Opacity = 100;
        }
        public void ChangeOpacityLoginDetailsB(object sender, System.Windows.RoutedEventArgs e)
        {
            LoginDetails.Opacity = 100;
        }
        public void ChangeOpacityProgressRingB(object sender, System.Windows.RoutedEventArgs e)
        {
            ProgressRing.Opacity = 0;
        }

        private async void SubmitClick(object sender, System.Windows.RoutedEventArgs e)
        {
            
            Thread thread =  new Thread(new ThreadStart( () =>
            {
                LoginDetails.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new EventHandler<System.Windows.RoutedEventArgs>(ChangeOpacityLoginDetails), sender, new object[] { e});
                ProgressRing.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new EventHandler<System.Windows.RoutedEventArgs>(ChangeOpacityProgressRing), sender, new object[] { e});
            }));
            //thread.Start();

            SWEET_FACTORYEntities sweetFactory = new SWEET_FACTORYEntities();
            LoginBlock.Text = "specialist_test";
            PassBlock.Password = "test";

            var resultUser =
             sweetFactory.STAFF.ToList().SingleOrDefault(user => user.LOGIN.Equals(LoginBlock.Text) && user.PASSWORD.Equals(PassBlock.Password));

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
                HomeWindowSpecialist homeWindowSpecialist = new HomeWindowSpecialist();
                homeWindowSpecialist.Show();

                
                //thread.Interrupt();
                //Back(sender, e);
                this.Close();
            }
            else
            {
               await this.ShowMessageAsync("Помилка", "Не правильний пароль чи логін. Перевірте введені дані і спробуйте знову");
                Back(sender,e);
            }
           
        }

        private void Back(Object sender, System.Windows.RoutedEventArgs e)
        {
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                LoginDetails.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new EventHandler<System.Windows.RoutedEventArgs>(ChangeOpacityLoginDetailsB), sender, new object[] { e });
                ProgressRing.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new EventHandler<System.Windows.RoutedEventArgs>(ChangeOpacityProgressRingB), sender, new object[] { e });
            }));
            thread1.Start();
        }
    }
}
