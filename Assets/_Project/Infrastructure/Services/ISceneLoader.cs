using Cysharp.Threading.Tasks;
using Project.Infrastructure.DependencyInjection;

namespace Project.Infrastructure.Services
{
    public interface ISceneLoader : IService
    {
        UniTask LoadScene(string sceneName);
    }
}