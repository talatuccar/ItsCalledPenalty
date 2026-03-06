using UnityEngine;
using Project.Infrastructure.DependencyInjection;
using Project.Core.EventBus;

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

        Debug.Log("Services initialized");
    }
}