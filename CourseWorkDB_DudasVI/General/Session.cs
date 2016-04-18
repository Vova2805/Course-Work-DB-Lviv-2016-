namespace CourseWorkDB_DudasVI.General
{
    public class Session
    {
        public static STAFF User { get; set; }
        public static SWEET_FACTORYEntities FactoryEntities { get; private set; }
        static Session()
        {
            FactoryEntities = new SWEET_FACTORYEntities();
        }
    }
}