using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SpecialistModel
    {
        public List<OrderProductTransaction> productPackagesList = new List<OrderProductTransaction>();
        public WAREHOUSE CurrentWarehouse;
        



        public SpecialistModel(SWEET_FACTORYEntities FactoryEntities)
        {
           var groupedPackages = FactoryEntities.ORDER_PRODUCT.ToList().GroupBy(pr=>pr.PRODUCT_INFO.PRODUCT_TITLE).ToDictionary(group => group.Key, group => group.ToList());
            foreach (var group in groupedPackages)
            {
                productPackagesList.Add(new OrderProductTransaction(group.Value,Session.User));
            }
        }


    }
}