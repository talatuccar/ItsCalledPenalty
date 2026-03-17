using Project.Core.EventBus;
using UnityEngine;

public class BallKickController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    [Header("Force Settings")]
    [SerializeField] private float horizontalMultiplier = 10f;
    [SerializeField] private float verticalMultiplier = 15f;
    [SerializeField] private float forwardForce = 25f;

    [SerializeField] private BoxCollider goalTrigger; 
    [SerializeField] private float flightTime = 1.5f; 
    [SerializeField] private float spinIntensity = 25f;

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
        Kick(e.ShotData);
    }
}