using Business.Abstract;
using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Category;
using Entities.Dtos.Product;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly ICacheManager _cacheManager;
        public ProductManager(IProductRepository productRepository, ICategoryService categoryService, ICacheManager cacheManager)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _cacheManager = cacheManager;
        }


        public async Task<IDataResult<ProductResponseModel>> Add(ProductForAddUpdateDto dto)
        {

            var isProductExists = await _productRepository.GetListAsync(t => t.Name == dto.Name);
            if (isProductExists.FirstOrDefault() != null)
            {
                return new ErrorDataResult<ProductResponseModel>(Messages.ProductAlreadyExist);
            }
            var newProduct = dto.Map<Product>();
            await _productRepository.AddAsync(newProduct);

            _cacheManager.RemoveByPattern("Product*");

            var returnProduct = newProduct.Map<ProductResponseModel>();
            var category = await _categoryService.GetCategory(newProduct.CategoryId);
            returnProduct.Category = category.Data.Map<CategoryResponseModel>();
            return new SuccessDataResult<ProductResponseModel>(returnProduct, Messages.AddedSuccessfully);
        }

        public async Task<IResult> Delete(string id)
        {
            var isProductExists = await _productRepository.GetListAsync(t => t.Id == ObjectId.Parse(id));
            if (isProductExists.FirstOrDefault() == null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            await _productRepository.DeleteAsync(id);
            _cacheManager.RemoveByPattern("Product*");
            return new SuccessResult(Messages.DeletedSuccessfully);
        }
        public async Task<IDataResult<ProductResponseModel>> GetProduct(string id)
        {
            var cacheKey = "Product_" + id;
            var isCacheExists = _cacheManager.IsAdd(cacheKey);
            if (isCacheExists)
            {
                return new SuccessDataResult<ProductResponseModel>(_cacheManager.Get<ProductResponseModel>(cacheKey));
            }
            var product = await _productRepository.GetByIdAsync(id);
            var category = await _categoryService.GetCategory(product.CategoryId);
            if (product != null)
            {
                var returnProduct = product.Map<ProductResponseModel>();
                returnProduct.Category = category.Data.Map<CategoryResponseModel>();
                _cacheManager.Add(cacheKey, returnProduct, 600);
                return new SuccessDataResult<ProductResponseModel>(returnProduct);
            }
            else
            {
                return new ErrorDataResult<ProductResponseModel>(Messages.ProductNotFound);
            }
        }

        public async Task<IDataResult<List<ProductResponseModel>>> GetProductList(ProductForGetListDto dto)
        {
            var cacheKey = "Products_" + JsonConvert.SerializeObject(dto);
            var isCacheExists = _cacheManager.IsAdd(cacheKey);
            if (isCacheExists)
            {
                return new SuccessDataResult<List<ProductResponseModel>>(_cacheManager.Get<List<ProductResponseModel>>(cacheKey));
            }

            var products = await _productRepository.GetListAsync();
            #region product filter
            if (dto != null)
            {
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    products = products.Where(t => t.Name == dto.Name);
                }
                if (!string.IsNullOrEmpty(dto.Description))
                {
                    products = products.Where(t => t.Description == dto.Description);
                }
                if (dto.Price != 0)
                {
                    products = products.Where(t => t.Price == dto.Price);
                }
                if (!string.IsNullOrEmpty(dto.Currency))
                {
                    products = products.Where(t => t.Currency == dto.Currency);
                }
            }
            #endregion

            var categories = await _categoryService.GetCategoryList(null);
            var data = (from p in products.ToList()
                        join c in categories.Data
                        on p.CategoryId equals c.Id.ToString()
                        select new ProductResponseModel
                        {
                            Id = p.Id.ToString(),
                            Category = new CategoryResponseModel
                            {
                                Id = c.Id.ToString(),
                                Name = c.Name,
                                Description = c.Description
                            },
                            Name = p.Name,
                            Currency = p.Currency,
                            Description = p.Description,
                            Price = p.Price
                        }).ToList();

            _cacheManager.Add(cacheKey, data, 600);
            return new SuccessDataResult<List<ProductResponseModel>>(data);
        }

        public async Task<IDataResult<ProductResponseModel>> Update(string id, ProductForAddUpdateDto dto)
        {
            var isProductExists = await _productRepository.GetListAsync(t => t.Id == ObjectId.Parse(id));
            if (isProductExists.FirstOrDefault() == null)
            {
                return new ErrorDataResult<ProductResponseModel>(Messages.ProductNotFound);
            }
            var updatedProduct = dto.Map<Product>();
            updatedProduct.Id = ObjectId.Parse(id);
            await _productRepository.UpdateAsync(updatedProduct, t => t.Id == ObjectId.Parse(id));
            _cacheManager.RemoveByPattern("Product*");

            var category = await _categoryService.GetCategory(updatedProduct.CategoryId);
            var returnProduct = updatedProduct.Map<ProductResponseModel>();
            returnProduct.Category = category.Data.Map<CategoryResponseModel>();
            return new SuccessDataResult<ProductResponseModel>(returnProduct, Messages.UpdatedSuccessfully);
        }
    }
}