using UnityEngine;
using DG.Tweening;

public class NetSoftPhysics : MonoBehaviour
{
    [SerializeField] private Transform netTransform;
    [Header("Impact Settings")]
    [SerializeField] private float punchForce = 0.6f;
    [SerializeField] private float duration = 0.7f;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        // Oyun baþladýðýnda filenin o anki güzel konumunu kaydet
        _startPosition = netTransform.localPosition;
        _startRotation = netTransform.localRotation;
    }

    public void Impact(Vector3 direction, Vector3 rotationPower)
    {
        netTransform.DOKill();

        // Zero yerine kaydettiðimiz orijinal konuma döndürüyoruz
        netTransform.localPosition = _startPosition;
        netTransform.localRotation = _startRotation;

        // Punch vuruþunu yap
        netTransform.DOPunchPosition(direction * punchForce, duration, 6, 0.5f);
        netTransform.DOPunchRotation(rotationPower, duration, 5, 0.5f);
    }
}