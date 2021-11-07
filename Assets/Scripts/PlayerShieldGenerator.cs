using UnityEngine;

public enum ShieldType
{
    Stationary,
    Moving,
    None
}

public abstract class PlayerShieldGenerator : MonoBehaviour
{
    public abstract bool TriggerShield(GameObject shieldObject);
}