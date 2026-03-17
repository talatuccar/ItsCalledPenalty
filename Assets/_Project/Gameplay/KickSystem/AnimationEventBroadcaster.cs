using UnityEngine;
using System;

public class AnimationEventBroadcaster : MonoBehaviour
{
    // Bu event'i PenaltyManager dinleyecek
    public event Action OnBallHitFrame;

    // Animation Window'dan seçeceðin metod
    public void OnBallHit()
    {
        OnBallHitFrame?.Invoke();
    }
}