using UnityEngine;
using UnityEngine.UI;

public class SliderOscillator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Slider targetSlider;
    [SerializeField] private float speed = 2f;

    private bool _isStopped = false;

    private void Update()
    {
        if (_isStopped) return;


        //float value = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        //targetSlider.value = value;
        targetSlider.value = Mathf.PingPong(Time.time * speed, 1f);
    }

    public void Stop() => _isStopped = true;
    public void StartMoving() => _isStopped = false;

    public float GetValue() => targetSlider.value;
}