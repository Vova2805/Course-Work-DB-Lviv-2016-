namespace CourseWorkDB_DudasVI.General
{
    public class Session
    {
        static Session()
        {
            FactoryEntities = new SWEET_FACTORYEntities();
        }

        public static STAFF User { get; set; }
        public static SWEET_FACTORYEntities FactoryEntities { get; private set; }
    }
}