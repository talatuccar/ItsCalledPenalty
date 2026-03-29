using Project.Core.EventBus;
using UnityEngine;

public class BallKickController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    [Header("Force Settings")]
    //[SerializeField] private float horizontalMultiplier = 10f;
    ////[SerializeField] private float verticalMultiplier = 15f;
    //[SerializeField] private float forwardForce = 25f;

    [SerializeField] private BoxCollider goalTrigger;
    [SerializeField] private float flightTime = 1.5f;
    [SerializeField] private float spinIntensity = 25f;
    //public float maxDeviation = 4.0f;
    //public float horizontalErrorMultiplier = 10f;
    //public float verticalErrorMultiplier = 5f;

    [Header("Hata Hassasiyeti")]
    [SerializeField] private float horizontalErrorPower = -10f; // Ters geliyorsa eksi kalsýn
    [SerializeField] private float verticalErrorPower = 5f;
    [SerializeField] private float autoLiftAmount = 1.5f; // Kötü vuruþta ekstra yükselme
    private IEventBus _eventBus;
    public void Kick(ShotData shotData)
    {
        // 1. Hedef Belirleme (Kutunun sýnýrlarýný kullanýyoruz)
        Bounds b = goalTrigger.bounds;

        // Senin eksenlerine göre (Z yan, Y yukarý, X kale derinliði)
        float targetZ = Mathf.Lerp(b.max.z, b.min.z, shotData.Horizontal);
        float targetY = Mathf.Lerp(b.min.y, b.max.y, shotData.Vertical);

        Vector3 targetPoint = new Vector3(b.center.x, targetY, targetZ);

        // 2. Hýz Hesaplama
        ballRigidbody.isKinematic = false;
        Vector3 launchVelocity = CalculateVelocity(targetPoint, flightTime);
        ballRigidbody.linearVelocity = launchVelocity;

        // 3. Dönme (Spin) Efekti
        // Topun hem kendi ekseninde dönmesini hem de falso almasýný saðlar
        float hEffect = (0.5f - shotData.Horizontal) * 2f;
        ballRigidbody.angularVelocity = new Vector3(
            launchVelocity.z * 0.5f, // Ýleri gidiþ rotasyonu
            hEffect * spinIntensity, // Yanal falso
            0
        );
    }

    private Vector3 CalculateVelocity(Vector3 target, float time)
    {
        Vector3 distance = target - ballRigidbody.position;

        float vX = distance.x / time;
        float vZ = distance.z / time;
        // Yerçekimi telafisi (Fizik formülü)
        float vY = (distance.y - 0.5f * Physics.gravity.y * Mathf.Pow(time, 2)) / time;

        return new Vector3(vX, vY, vZ);
    }
    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;

    }

    private void Start()
    {
        _eventBus.Subscribe<BallKickedEvent>(OnBallKicked);
    }

    private void OnDestroy()
    {
        _eventBus?.Unsubscribe<BallKickedEvent>(OnBallKicked);
    }

   
    private void OnBallKicked(BallKickedEvent e)
    {
        Bounds b = goalTrigger.bounds;

        // 1. Hedef Belirleme
        float targetZ = Mathf.Lerp(b.max.z, b.min.z, e.ShotData.Horizontal);
        float targetY = Mathf.Lerp(b.min.y, b.max.y, e.ShotData.Vertical);
        Vector3 baseTarget = new Vector3(b.center.x, targetY, targetZ);

        float errorFactor = 1f - e.TimingScore;

        // 2. Dinamik Hata Hesaplama
        // Yatay: Orta noktadan (0.5) ne kadar uzaktaysa o yöne saptýr
        float horizontalDir = e.ShotData.Horizontal - 0.5f;
        float horizontalOffset = horizontalDir * e.Settings.horizontalErrorPower * errorFactor;

        float verticalOffset = (e.ShotData.Vertical * e.Settings.verticalErrorPower * errorFactor)
                               + (e.Settings.autoLiftAmount * errorFactor);

        // 3. Final Noktasý
        Vector3 finalTarget = baseTarget + new Vector3(0, verticalOffset, horizontalOffset);

        // 4. Fizik Uygulama
        ballRigidbody.isKinematic = false;
        Vector3 velocity = CalculateVelocity(finalTarget, flightTime);
        ballRigidbody.linearVelocity = velocity;
    }
}