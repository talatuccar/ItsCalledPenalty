using UnityEngine;
using Project.Infrastructure.DependencyInjection;

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
        Debug.Log("Services initialized");
    }
}