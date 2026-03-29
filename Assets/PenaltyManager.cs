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
    public enum GameMode { Penalty, FreeKick }
    [Header("Game Mode Settings")]
    public GameMode currentMode;
    [SerializeField] private ShotSettings penaltySettings;
    [SerializeField] private ShotSettings freeKickSettings;

    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    private void OnEnable()
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

      
        _capturedTimingScore = timingRing.Accuracy;

       

        playerAnimator.SetTrigger("Kick");
        inputHandler.enabled = false;
        timingRing.Deactivate(); 
    }

    private void HandleBallHit()
    {
        ShotSettings activeSettings = (currentMode == GameMode.Penalty) ? penaltySettings : freeKickSettings;
   
        _eventBus.Publish(new BallKickedEvent(_finalShot, _capturedTimingScore, activeSettings));
    }

    private void OnDestroy()
    {
        
        inputHandler.OnShootPerformed -= HandleInput;
        broadcaster.OnBallHitFrame -= HandleBallHit;
    }
}