using Cysharp.Threading.Tasks;
using Project.Core.EventBus;
using Project.Infrastructure.DependencyInjection;
using Project.Infrastructure.Services;
using UnityEngine;

public class ProjectBootstrap : MonoBehaviour
{
    public static ServiceContainer Container { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Container = new ServiceContainer();

        InitializeServices();
    }

    private void InitializeServices()
    {
        var eventBus = new EventBus();
        Container.Register<IEventBus>(eventBus);

        var sceneLoader = new SceneLoader();
        Container.Register<ISceneLoader>(sceneLoader);

        Debug.Log("Services initialized");
    }

    private async void Start()
    {

        var sceneLoader = Container.Get<ISceneLoader>();
       
        await UniTask.Delay(500);
        await sceneLoader.LoadScene(SceneNames.Menu);
    }
}