using AutoMapper;
using CompanyManagement.Model;
using CompanyManagement.ViewModel;
namespace CompanyManagement.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyInfo, AddCompanyInfoVM>().ReverseMap();
            CreateMap<CompanyInfo, UpdateCompanyInfoVM>().ReverseMap();
            CreateMap<GetCompanyInfoVM, CompanyInfo>().ReverseMap();

        }
    }
}
