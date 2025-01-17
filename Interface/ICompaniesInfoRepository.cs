using CompanyManagement.Helpers;
using CompanyManagement.Model;
using CompanyManagement.ViewModel;

namespace CompanyManagement.Interface
{
    public interface ICompaniesInfoRepository
    {
        Task<BaseResponseModel<IEnumerable<GetCompanyInfoVM>>> GetAllCompany();
        Task<BaseResponseObject<GetCompanyInfoVM>> GetCompanyById(Guid id);
        Task<BaseResponse> AddCompany(AddCompanyInfoVM entity);
        Task<BaseResponse> UpdateCompany(UpdateCompanyInfoVM entity);
        Task<BaseResponse> DeleteCompany(Guid id);
    }
}
