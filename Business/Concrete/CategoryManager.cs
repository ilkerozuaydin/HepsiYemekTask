using Business.Abstract;
using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Category;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{

    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICacheManager _cacheManager;

        public CategoryManager(ICategoryRepository categoryRepository, ICacheManager cacheManager)
        {
            _categoryRepository = categoryRepository;
            _cacheManager = cacheManager;
        }

        public async Task<IDataResult<CategoryResponseModel>> Add(CategoryForAddUpdateDto dto)
        {

            var isCategoryExists = await _categoryRepository.GetListAsync(t => t.Name == dto.Name);
            if (isCategoryExists.FirstOrDefault() != null)
            {
                return new ErrorDataResult<CategoryResponseModel>(Messages.CategoryAlreadyExist);
            }
            var newCategory = dto.Map<Category>();
            await _categoryRepository.AddAsync(newCategory);
            _cacheManager.RemoveByPattern("Category*");
            _cacheManager.RemoveByPattern("Product*");
            return new SuccessDataResult<CategoryResponseModel>(newCategory.Map<CategoryResponseModel>(), Messages.AddedSuccessfully);
        }

        public async Task<IResult> Delete(string id)
        {
            var isCategoryExists = await _categoryRepository.GetListAsync(t => t.Id == ObjectId.Parse(id));
            if (isCategoryExists.FirstOrDefault() == null)
            {
                return new ErrorResult(Messages.CategoryNotFound);
            }

            await _categoryRepository.DeleteAsync(id);
            _cacheManager.RemoveByPattern("Category*");
            _cacheManager.RemoveByPattern("Product*");
            return new SuccessResult(Messages.DeletedSuccessfully);
        }
        public async Task<IDataResult<CategoryResponseModel>> GetCategory(string id)
        {
            var cacheKey = "Category_" + id;
            var isCacheExists = _cacheManager.IsAdd(cacheKey);
            if (isCacheExists)
            {
                return new SuccessDataResult<CategoryResponseModel>(_cacheManager.Get<CategoryResponseModel>(cacheKey));
            }
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                var returnCategory = category.Map<CategoryResponseModel>();

                _cacheManager.Add(cacheKey, returnCategory, 600);
                return new SuccessDataResult<CategoryResponseModel>(returnCategory);
            }
            else
            {
                return new ErrorDataResult<CategoryResponseModel>(Messages.CategoryNotFound);
            }
        }

        public async Task<IDataResult<List<CategoryResponseModel>>> GetCategoryList(CategoryForGetListDto dto)
        {
            var cacheKey = "Categories_" + JsonConvert.SerializeObject(dto);
            var isCacheExists = _cacheManager.IsAdd(cacheKey);
            if (isCacheExists)
            {
                return new SuccessDataResult<List<CategoryResponseModel>>(_cacheManager.Get<List<CategoryResponseModel>>(cacheKey));
            }

            var categories = await _categoryRepository.GetListAsync();
            #region category filter
            if (dto !=null)
            {
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    categories = categories.Where(t => t.Name == dto.Name);
                }
                if (!string.IsNullOrEmpty(dto.Description))
                {
                    categories = categories.Where(t => t.Description == dto.Description);
                }
            }
     
            #endregion

            _cacheManager.Add(cacheKey, categories, 600);
            return new SuccessDataResult<List<CategoryResponseModel>>(categories.ToList().Select(t=>t.Map<CategoryResponseModel>()).ToList());
        }


        public async Task<IDataResult<CategoryResponseModel>> Update(string id, CategoryForAddUpdateDto dto)
        {
            var isCategoryExists = await _categoryRepository.GetListAsync(t => t.Id == ObjectId.Parse(id));
            if (isCategoryExists.FirstOrDefault() == null)
            {
                return new ErrorDataResult<CategoryResponseModel>(Messages.CategoryNotFound);
            }
            var updatedCategory = dto.Map<Category>();
            updatedCategory.Id = ObjectId.Parse(id);
            await _categoryRepository.UpdateAsync(updatedCategory, t => t.Id == ObjectId.Parse(id));
            _cacheManager.RemoveByPattern("Category*");
            _cacheManager.RemoveByPattern("Product*");
            return new SuccessDataResult<CategoryResponseModel>(updatedCategory.Map<CategoryResponseModel>(), Messages.UpdatedSuccessfully);
        }
    }
}