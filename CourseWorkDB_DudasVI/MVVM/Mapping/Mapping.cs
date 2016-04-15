using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.MVVM.ViewModels.Additional;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Mapping
{
    class Mapping
    {
        public static void Create()
        {
            #region Director
            AutoMapper.Mapper.CreateMap<DirectorModel, DirectorViewModel>();
            AutoMapper.Mapper.CreateMap< DirectorViewModel, DirectorModel>();
            #endregion

            #region Specialist
            AutoMapper.Mapper.CreateMap<OrderProductTransaction, OrderProductTransactionVm>();
            AutoMapper.Mapper.CreateMap<OrderProductTransactionVm, OrderProductTransaction>();
            AutoMapper.Mapper.CreateMap<SpecialistModel, SpecialistViewModel>();
            AutoMapper.Mapper.CreateMap<SpecialistViewModel, SpecialistModel>();
            #endregion
        }
    }
}
