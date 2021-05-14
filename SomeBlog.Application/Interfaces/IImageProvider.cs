using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces
{
    public interface IImageProvider
    {
        Task<string> SaveAsync(IFormFile formFile);
    }
}
