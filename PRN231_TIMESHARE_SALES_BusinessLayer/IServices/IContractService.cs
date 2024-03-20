using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IContractService
    {
        public ResponseResult<ContractViewModel> GetContract(int id);
        public DynamicModelResponse.DynamicModelsResponse<ContractViewModel> GetContracts(PagingRequest paging, ContractViewModel filter);
        public ResponseResult<ContractViewModel> CreateContract(ContractRequestModel request);
        public ResponseResult<ContractViewModel> UpdateContract(ContractRequestModel request, int id);
        public ResponseResult<ContractViewModel> DeleteContract(int id);
        public EnumViewModel GetConstractType();
    }
}
