using Project.Core.EventBus;
using UnityEngine;
using static ShotParameterProvider;

public class PenaltyManager : MonoBehaviour
{
    [Header("Sub-Systems")]
    [SerializeField] private ShotParameterProvider parameterProvider;
    [SerializeField] private PenaltyInputHandler inputHandler;
    [SerializeField] private BallKickController ballKicker;
    [SerializeField] private ShotTimingRing timingRing; 
    private float _capturedTimingScore; 

    [Header("Visuals")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AnimationEventBroadcaster broadcaster;

    //private Vector2 _finalParameters;
    ShotData _finalShot;
    private IEventBus _eventBus;

    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    private void Start()
    {
        parameterProvider.Initialize();

       
        inputHandler.OnShootPerformed += HandleInput;
        broadcaster.OnBallHitFrame += HandleBallHit;
    }

    private void HandleInput()
    {
        // Önce seçimi ilerlet
        bool isFinished = parameterProvider.AdvanceSelection();

        // Þu anki durumu kontrol et
        var currentState = parameterProvider.GetCurrentState();

        if (currentState == SelectionState.Timing)
        {
            // Sadece ve sadece iki slider da durduðunda buraya girer
            timingRing.Activate();
            Debug.Log("Sliderlar bitti, halka baþladý!");
        }
        else if (isFinished)
        {
            // Halka aþamasýndayken basýldý, þut baþlasýn
            timingRing.Deactivate();
            StartShootSequence();
            Debug.Log("Zamanlama yakalandý, þut çekiliyor!");
        }
    }
    private void StartShootSequence()
    {
        _finalShot = parameterProvider.GetShotData();

        // Halkanýn o anki deðerini al (Örn: 0.85f)
        _capturedTimingScore = timingRing.Accuracy;

        playerAnimator.SetTrigger("Kick");
        inputHandler.enabled = false;
        timingRing.Deactivate(); // Vuruþ kararý verildiði an halkayý kapatabilirsin
    }

    private void HandleBallHit()
    {
        // Artýk event fýrlatýrken yakaladýðýmýz timing deðerini de gönderiyoruz
        _eventBus.Publish(new BallKickedEvent(_finalShot, _capturedTimingScore));
    }

    private void OnDestroy()
    {
        
        inputHandler.OnShootPerformed -= HandleInput;
        broadcaster.OnBallHitFrame -= HandleBallHit;
    }
}