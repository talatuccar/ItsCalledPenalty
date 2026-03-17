using UnityEngine;
using Project.Core.EventBus;

public class SceneInstaller : MonoBehaviour
{
    [SerializeField] private BallKickController ballKick;
    //[SerializeField] private KickSoundPlayer soundPlayer;
    [SerializeField]
    private PenaltyManager penaltyManager;

    private void Start()
    {
        if (ProjectBootstrap.Container == null)
        {
            Debug.LogWarning("Bootstrap yok, oluþturuluyor...");

            var bootstrapGO = new GameObject("ProjectBootstrap");
            bootstrapGO.AddComponent<ProjectBootstrap>();
        }

        var container = ProjectBootstrap.Container;
        var eventBus = container.Get<IEventBus>();

        ballKick.Construct(eventBus);
        penaltyManager.Construct(eventBus);
    }
}