using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
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

            #region Saler

            Mapper.CreateMap<SalerModel, SalerViewModel>();
            Mapper.CreateMap<SalerViewModel, SalerModel>();

            #endregion

            #region Specialist

            Mapper.CreateMap<SpecialistModel, SpecialistViewModel>();
            Mapper.CreateMap<SpecialistViewModel, SpecialistModel>();

            #endregion
        }
    }
}