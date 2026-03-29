using UnityEngine;

[CreateAssetMenu(fileName = "ShotSettings", menuName = "Scriptable Objects/ShotSettings")]
public class ShotSettings : ScriptableObject
{
    [Header("Multiplier Settings")]
    public float horizontalErrorPower = 10f;
    public float verticalErrorPower = 5f;
    public float autoLiftAmount = 1.5f;

    [Header("Physics Settings")]
    public float flightTime = 1.2f;
}
