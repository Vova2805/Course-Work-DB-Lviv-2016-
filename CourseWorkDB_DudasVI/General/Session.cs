using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.General
{
    public enum UserType
    {
        Director = 0,
        Saler,
        Specialist
    }

    public class Session
    {
        static Session()
        {
            FactoryEntities = new SWEET_FACTORYEntities();
        }

        public static STAFF User { get; set; }
        public static SWEET_FACTORYEntities FactoryEntities { get; set; }
        public static UserType userType { get; set; }
        public static  CommonViewModel dataContext { get; set; }

    }
}