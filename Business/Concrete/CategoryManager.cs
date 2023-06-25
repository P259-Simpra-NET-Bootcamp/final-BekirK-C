using AutoMapper;
using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models.Requests;
using Entities.Models.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;
    private readonly IValidator<Category> _categoryValidator;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper, IValidator<Category> categoryValidator)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
        _categoryValidator = categoryValidator;
    }

    public IResult Add(CategoryRequest categoryRequest)
    {
        var mappedCategory = _mapper.Map<CategoryRequest, Category>(categoryRequest);
        ValidationResult result = _categoryValidator.Validate(mappedCategory);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);
        
        mappedCategory.CreatedAt = DateTime.Now;
        _categoryDal.Add(mappedCategory);
        return new SuccessResult(CategoryMessages.CategoryAdded);
    }

    public IResult Delete(int id)
    {
        var entity = _categoryDal.Get(p => p.Id == id);
        if (entity == null)
            return new ErrorResult(CategoryMessages.CategoryNotFound);

        _categoryDal.Delete(entity);
        return new SuccessResult(CategoryMessages.CategoryDeleted);
    }

    public IDataResult<List<CategoryResponse>> GetAll()
    {
        var categoryList = _categoryDal.GetAll();
        return GetMappedCategoryList(categoryList);
    }

    public IDataResult<List<CategoryResponse>> GetAllWithInclude()
    {
        var categoryList = _categoryDal.GetAllWithInclude("Products");
        return GetMappedCategoryList(categoryList);
    }

    public IResult Update(CategoryRequest categoryRequest, int categoryId)
    {
        var mappedCategory = _mapper.Map<CategoryRequest, Category>(categoryRequest);
        ValidationResult result = _categoryValidator.Validate(mappedCategory);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);
        
        var category = _categoryDal.Get(p => p.Id == categoryId);
        if (category == null)
            return new ErrorResult(CategoryMessages.CategoryNotFound);

        mappedCategory.Id = categoryId;
        mappedCategory.CreatedAt = category.CreatedAt;
        _categoryDal.Update(mappedCategory);
        return new SuccessResult(CategoryMessages.CategoryUpdated);
    }

    private IDataResult<List<CategoryResponse>> GetMappedCategoryList(List<Category> categoryList)
    {
        var mappedCategoryList = _mapper.Map<List<Category>, List<CategoryResponse>>(categoryList);

        if (mappedCategoryList.Count == 0)
            return new ErrorDataResult<List<CategoryResponse>>(CategoryMessages.NoProductInStock);

        return new SuccessDataResult<List<CategoryResponse>>(mappedCategoryList, CategoryMessages.CategoriesListed);
    }
}
