using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class DirectorModel
    {
        public List<STAFF> EmployeeList;
        public STAFF SelectedEmployee;
        //public IEnumerable<CLIENT> ClientList;
        //public IEnumerable<SALE_ORDER> OrderList;
        public DirectorModel(SWEET_FACTORYEntities sweetFactory)
        {
            EmployeeList = (sweetFactory.STAFF.ToList()).FindAll(s=>s.POST.DEPARTMENT.DEPARTMENT_ID == 3);
            SelectedEmployee = EmployeeList.First();
        }
    }
}