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
            Mapper.CreateMap<GeneralModel, CommonViewModel>();
            Mapper.CreateMap<CommonViewModel, GeneralModel>();
        }
    }
}