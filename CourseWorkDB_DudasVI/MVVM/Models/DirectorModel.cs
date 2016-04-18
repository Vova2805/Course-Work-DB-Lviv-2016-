using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class DirectorModel
    {
        public List<STAFF> EmployeeList;
        public STAFF SelectedEmployee;
        //public IEnumerable<CLIENT> ClientList;
        //public IEnumerable<SALE_ORDER> OrderList;
        public DirectorModel()
        {
            EmployeeList = Session.FactoryEntities.STAFF.ToList().FindAll(s => s.POST.DEPARTMENT.DEPARTMENT_ID == 3);
            SelectedEmployee = EmployeeList.First();
        }
    }
}