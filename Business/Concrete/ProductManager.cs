using AutoMapper;
using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models.Requests;
using Entities.Models.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;
    private readonly IValidator<Product> _productValidator;

    public ProductManager(IProductDal poductDal, IMapper mapper, ICategoryDal categoryDal, IValidator<Product> productValidator)
    {
        _productDal = poductDal;
        _mapper = mapper;
        _categoryDal = categoryDal;
        _productValidator = productValidator;
    }

    public IResult Add(ProductRequest productRequest)
    {
        var category = _categoryDal.Get(c => c.Id == productRequest.CategoryId);
        if (category == null)
            return new ErrorResult(ProductMessages.InvalidCategoryId);

        var mappedProduct = _mapper.Map<ProductRequest, Product>(productRequest);
        ValidationResult result = _productValidator.Validate(mappedProduct);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);
        
        mappedProduct.CreatedAt = DateTime.Now;
        _productDal.Add(mappedProduct);
        return new SuccessResult(ProductMessages.ProductAdded);
    }

    public IResult Delete(int id)
    {
        var product = _productDal.Get(p => p.Id == id);
        if (product == null)
            return new ErrorResult(ProductMessages.ProductNotFound);

        _productDal.Delete(product);
        return new SuccessResult(ProductMessages.ProductDeleted);
    }

    public IDataResult<List<ProductResponse>> GetAll()
    {
        var productList = _productDal.GetAll();
        if (productList.Count == 0)
            return new ErrorDataResult<List<ProductResponse>>(ProductMessages.NoProductInStock);

        return new SuccessDataResult<List<ProductResponse>>(productList, ProductMessages.ProductsListed);
    }

    public IDataResult<ProductResponse> GetById(int id)
    {
        var product = _productDal.GetAll(p => p.Id == id).FirstOrDefault();
        if (product == null)
            return new ErrorDataResult<ProductResponse>(ProductMessages.ProductNotFound);

        return new SuccessDataResult<ProductResponse>(product, ProductMessages.ProductListed);
    }
    public IDataResult<List<ProductResponse>> GetByCategoryId(int categoryId)
    {
        var category = _categoryDal.Get(c => c.Id == categoryId);
        if (category == null)
            return new ErrorDataResult<List<ProductResponse>>(ProductMessages.InvalidCategoryId);

        var productList = _productDal.GetAll(p => p.CategoryId == categoryId);
        var mappedProductList = _mapper.Map<List<Product>, List<ProductResponse>>(productList);
        return new SuccessDataResult<List<ProductResponse>>(mappedProductList, ProductMessages.ProductListedSelectedCategory);
    }

    public IResult Update(ProductRequest productRequest, int productId)
    {
        var mappedProduct = _mapper.Map<ProductRequest, Product>(productRequest);
        ValidationResult result = _productValidator.Validate(mappedProduct);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);
        
        var product = _productDal.Get(p => p.Id == productId);

        if (product == null)
            return new ErrorResult(ProductMessages.ProductNotFound);

        mappedProduct.Id = productId;
        mappedProduct.CreatedAt = product.CreatedAt;
        _productDal.Update(mappedProduct);
        return new SuccessResult(ProductMessages.ProductUpdated);
    }

    public IResult UpdateStock(int id, int quantity)
    {
        var product = _productDal.Get(p => p.Id == id);
        if (product == null)
            return new ErrorResult(ProductMessages.ProductNotFound);

        product.Stock += quantity;

        if (quantity < 0 && product.Stock < Math.Abs(quantity))
            return new ErrorResult(ProductMessages.InsufficientStock);

        _productDal.Update(product);

        var message = quantity < 0 ? ProductMessages.StockDecreasedSuccess : ProductMessages.StockIncreasedSuccess;
        return new SuccessResult(message);
    }

    public IDataResult<List<ProductResponse>> GetProductsInStock()
    {
        var products = _productDal.GetAll(p => p.Stock > 0);

        if (products.Count == 0)
            return new ErrorDataResult<List<ProductResponse>>(ProductMessages.NoProductInStock);

        return new SuccessDataResult<List<ProductResponse>>(products, ProductMessages.ProductsInStockListed);
    }

    public IDataResult<List<ProductResponse>> GetProductsAbovePrice(decimal price)
    {
        var products = _productDal.GetAll(p => p.Price >= price);

        if (products.Count == 0)
            return new ErrorDataResult<List<ProductResponse>>(ProductMessages.NoProductAbovePrice(price));

        return new SuccessDataResult<List<ProductResponse>>(products, ProductMessages.ProductsAbovePriceListed(price));
    }
}
