using Project.Core.EventBus;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    [Header("Sub-Systems")]
    [SerializeField] private ShotParameterProvider parameterProvider;
    [SerializeField] private PenaltyInputHandler inputHandler;
    [SerializeField] private BallKickController ballKicker;

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
       
        if (parameterProvider.AdvanceSelection())
        {
            StartShootSequence();
        }
    }

    private void StartShootSequence()
    {

        _finalShot = parameterProvider.GetShotData();

       

        playerAnimator.SetTrigger("Kick");
        inputHandler.enabled = false; 
    }

    private void HandleBallHit()
    {
        //ballKicker.Kick(_finalShot);
        _eventBus.Publish(new BallKickedEvent(_finalShot));

    }

    private void OnDestroy()
    {
        
        inputHandler.OnShootPerformed -= HandleInput;
        broadcaster.OnBallHitFrame -= HandleBallHit;
    }
}