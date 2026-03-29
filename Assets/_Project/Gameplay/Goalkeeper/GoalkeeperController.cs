//using UnityEngine;
//using Project.Core.EventBus;

//public class GoalkeeperController : MonoBehaviour
//{
//    [SerializeField] private Animator animator;
//    [SerializeField] private string diveTriggerName = "Dive";

//    [Header("Sync Settings")]
//    [SerializeField] private float ballFlightTime = 1.0f; // BallKickController'daki ile ayný olmalý
//    [SerializeField] private float animationImpactPoint = 0.85f; // Animasyonun yüzde kaçýnda topa deðer? (0-1 arasý)

//    private IEventBus _eventBus;

//    public void Construct(IEventBus eventBus)
//    {
//        _eventBus = eventBus;
//        _eventBus.Subscribe<BallKickedEvent>(OnBallKicked);
//    }

//    private void OnBallKicked(BallKickedEvent e)
//    {
//        // Profesyonel Hile: Animasyon hýzýný topun hýzýna eþitleme
//        // Formül: (Animasyonun vuruþ aný yüzdesi / Topun varýþ süresi)
//        // Eðer animasyon 2 saniye ama top 1 saniyede gidiyorsa, hýzý 2 katýna çýkarýr.
//        float requiredSpeed = animationImpactPoint / ballFlightTime;
//        animator.speed = requiredSpeed;

//        // Atlayýþý baþlat
//        animator.SetTrigger(diveTriggerName);

//        Debug.Log($"Kaleci atlýyor! Ayarlanan Animasyon Hýzý: {requiredSpeed}");
//    }

//    private void OnDestroy()
//    {
//        _eventBus?.Unsubscribe<BallKickedEvent>(OnBallKicked);
//    }
//}
using UnityEngine;
using Project.Core.EventBus;


public class GoalkeeperController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string diveTriggerName = "Dive";

    [Header("Sync Settings")]
    [SerializeField] private float ballFlightTime = 1.0f;
    [SerializeField] private float animationImpactPoint = 0.85f;

    private IEventBus _eventBus;
    
    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe<BallKickedEvent>(OnBallKicked);
    }

    private void OnBallKicked(BallKickedEvent e)
    {
        // 1. Rastgele Karar Mekanizmasý
        // X: -1 (Sol), 1 (Sað) | Y: 0 (Yer), 1 (Hava)
        //float randomX = Random.value > 0.5f ? 1f : -1f;
        //float randomY = Random.value > 0.5f ? 1f : 0f;

         
        

       
        

        // 3. Hýz Ayarý
        float requiredSpeed = animationImpactPoint / ballFlightTime;
        animator.speed = requiredSpeed;

        // 4. Tetikle
        animator.SetTrigger(diveTriggerName);

        
    }

    private void OnDestroy()
    {
        _eventBus?.Unsubscribe<BallKickedEvent>(OnBallKicked);
    }
}
