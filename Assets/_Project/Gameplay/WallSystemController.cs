using UnityEngine;
using System.Threading.Tasks; // async/await için gerekli
using System;
using Project.Core.EventBus; // Senin yeni event namespace'in
using System.Collections.Generic;

public class WallSystemController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxRandomDelayMs = 200f; // Milisaniye cinsinden

    //private List<Animator> _childAnimators = new List<Animator>();
    private IEventBus _eventBus;
    //private bool _isDestroyed;
    public Animator anim;

    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        //// Parent altýndaki tüm Animator'larý tek seferde önbelleðe alalým (Cache)
        //// Bu sayede her vuruþta GetComponent aramak zorunda kalmayýz.
        //var animators = GetComponentsInChildren<Animator>();
        //_childAnimators.AddRange(animators);
    }


    private void Start()
    {
        _eventBus.Subscribe<BallKickedEvent>(OnBallKicked);
    }

    // async void: Event handlerlar için kabul edilebilir bir kullanýmdýr
    private void OnBallKicked(BallKickedEvent e)
    {
        anim.SetTrigger("Jump");
    }

   

   
    private void OnDestroy()
    {
        //_isDestroyed = true;
        _eventBus?.Unsubscribe<BallKickedEvent>(OnBallKicked);
    }
}