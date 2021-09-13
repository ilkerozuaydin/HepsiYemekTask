using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<List<ProductResponseModel>>> GetProductList(ProductForGetListDto dto);

        Task<IDataResult<ProductResponseModel>> GetProduct(string id);

        Task<IDataResult<ProductResponseModel>> Add(ProductForAddUpdateDto dto);

        Task<IDataResult<ProductResponseModel>> Update(string id,ProductForAddUpdateDto dto);

        Task<IResult> Delete(string id);
    }
}