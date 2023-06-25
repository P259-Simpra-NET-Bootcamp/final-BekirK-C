using Base.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Base.Entities.Concrete;

public class RequestProfilerModel : IEntity
{
    public DateTimeOffset RequestTime { get; set; }
    public HttpContext Context { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }
    public DateTimeOffset ResponseTime { get; set; }
}
