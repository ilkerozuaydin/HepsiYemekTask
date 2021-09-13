using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<CategoryResponseModel>>> GetCategoryList(CategoryForGetListDto dto);

        Task<IDataResult<CategoryResponseModel>> GetCategory(string id);

        Task<IDataResult<CategoryResponseModel>> Add(CategoryForAddUpdateDto dto);

        Task<IDataResult<CategoryResponseModel>> Update(string id, CategoryForAddUpdateDto dto);

        Task<IResult> Delete(string id);

    }
}