using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.MVVM.ViewModels.Additional;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Mapping
{
    internal class Mapping
    {
        public static void Create()
        {
            #region Director

            Mapper.CreateMap<DirectorModel, DirectorViewModel>();
            Mapper.CreateMap<DirectorViewModel, DirectorModel>();

            #endregion

            #region Specialist

            Mapper.CreateMap<OrderProductTransaction, OrderProductTransactionVm>();
            Mapper.CreateMap<OrderProductTransactionVm, OrderProductTransaction>();
            Mapper.CreateMap<SpecialistModel, SpecialistViewModel>();
            Mapper.CreateMap<SpecialistViewModel, SpecialistModel>();

            #endregion
        }
    }
}