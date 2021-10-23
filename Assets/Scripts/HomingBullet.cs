using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    private float spd = 5f;
    private Vector3 _direction = Vector3.left;
    private float _time = 0.0f;
    private float _maxTrackingTime = 3.0f;

    protected new void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    public override void Update()
    {
        _time += Time.deltaTime;

        if (PlayerControl.Instance != null)
        {
            Vector3 correctDirection = PlayerControl.Instance.transform.position - transform.position;

            float correctDirectionWeight = 0.1f * Math.Max((_maxTrackingTime - _time) / _maxTrackingTime, 0);
            float currentDirectionWeight = 1 - correctDirectionWeight;
            _direction = currentDirectionWeight * _direction + correctDirectionWeight * correctDirection;
            _direction.Normalize();
        }

        transform.position += spd * Time.deltaTime * _direction;

        // To save on memory, let's check if the bullet is off-screen and destroy it
        if (!base.Render.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    public override void InitializeBullet()
    {
        if (!PlayerControl.Instance) return;

        _direction = PlayerControl.Instance.transform.position - transform.position;
        _direction.Normalize();
    }
}