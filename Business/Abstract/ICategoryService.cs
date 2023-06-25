using Base.Utilities.Results;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Abstract;

public interface ICategoryService
{
    IDataResult<List<CategoryResponse>> GetAll();
    IDataResult<List<CategoryResponse>> GetAllWithInclude();
    IResult Add(CategoryRequest category);
    IResult Update(CategoryRequest category, int categoryId);
    IResult Delete(int id);
}
