using UnityEngine;

public class MovingShieldGenerator : PlayerShieldGenerator
{
    private float _maxCooldownTimer = 5.0f;
    private float _shieldDuration = 2.0f;
    private float _time = 5.0f;

    private GameObject _shieldGameObject;

    public override bool TriggerShield(GameObject shieldObject)
    {
        if (_time < _maxCooldownTimer)
        {
            return false;
        }

        _shieldGameObject = Instantiate(shieldObject, transform);
        _shieldGameObject.transform.position = transform.position;
        _time = 0.0f;
        return true;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_shieldGameObject && _time > _shieldDuration)
        {
            Destroy(_shieldGameObject);
            _shieldGameObject = null;
        }
    }

    private void OnDestroy()
    {
        Destroy(_shieldGameObject);
    }
}