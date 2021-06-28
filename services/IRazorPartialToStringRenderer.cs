using System.Threading.Tasks;
namespace Automated_Wedding_Application.Services
{
    public interface IRazorPartialToStringRenderer
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}
