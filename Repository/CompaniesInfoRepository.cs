using AutoMapper;
using CompanyManagement.Data;
using CompanyManagement.Helpers;
using CompanyManagement.Interface;
using CompanyManagement.Model;
using CompanyManagement.ViewModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyManagement.Repository
{
    public class CompaniesInfoRepository : ICompaniesInfoRepository
    {
        private readonly CompanyDbContext _dbContext;
        private readonly IMapper _mapper;
        public CompaniesInfoRepository(IMapper mapper, CompanyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<BaseResponseModel<IEnumerable<GetCompanyInfoVM>>> GetAllCompany()
        {
            try
            {
                var result = await _dbContext.CompanyMaster.Where(x => !x.IsDeleted).ToListAsync();

                var companyDetail = _mapper.Map<IEnumerable<GetCompanyInfoVM>>(result);

                return new BaseResponseModel<IEnumerable<GetCompanyInfoVM>> { Success = true, Message = "The data has been retrieved successfully.", Data = companyDetail, TotalRecords = companyDetail.Count() };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponseObject<GetCompanyInfoVM>> GetCompanyById(Guid id)
        {
            try
            {
                var result = await _dbContext.CompanyMaster.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
                
                if (result == null)
                {
                    return new BaseResponseObject<GetCompanyInfoVM> { Success = false, Message = "It seems that the data you are looking for could not be found. Please try again." };
                }

                var companyDetail = _mapper.Map<GetCompanyInfoVM>(result);
                return new BaseResponseObject<GetCompanyInfoVM> { Success = true, Message = "The data has been retrieved successfully.", Data = companyDetail };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> AddCompany(AddCompanyInfoVM entity)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(entity);
                
                company.Id = new Guid();
                company.CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();

                _dbContext.CompanyMaster.Add(company);
                await _dbContext.SaveChangesAsync();
                return new BaseResponse { Success = true, Message = "Company added successfully." };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> UpdateCompany(UpdateCompanyInfoVM entity)
        {
            try
            {
                bool isDuplicateFound = await _dbContext.CompanyMaster
                 .AnyAsync(x => (x.Email.ToLower().Trim() == entity.Email.ToLower().Trim() || x.CompanyName.ToLower().Trim() == entity.CompanyName.ToLower().Trim()) && x.Id != entity.Id && !x.IsDeleted);

                if (isDuplicateFound)
                    return new BaseResponse { Success = false, Message = "Company name or email already exists." };

                var company = _mapper.Map<CompanyInfo>(entity);

                company.UpdatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();

                _dbContext.Entry(company).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new BaseResponse { Success = true, Message = "Company details updated successfully."};
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BaseResponse> DeleteCompany(Guid id)
        {
            try
            {
                var result = await _dbContext.CompanyMaster.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
                
                if (result == null)
                {
                    return new BaseResponse { Success = false, Message = "It seems that the data you are looking for could not be found. Please try again."};
                }
                result.IsDeleted = true;
                result.UpdatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
                _dbContext.CompanyMaster.Update(result);
                await _dbContext.SaveChangesAsync();

                return new BaseResponse { Success = true, Message = "Company deleted successfully." };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
