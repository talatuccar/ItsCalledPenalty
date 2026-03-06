using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Project.Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadScene(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            await operation.ToUniTask();
        }
    }
}