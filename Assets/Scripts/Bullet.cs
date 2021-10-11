using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float spd = 10f;
    private Vector3 _direction = Vector2.left;
    private SpriteRenderer render;

//    private float _timeOnScreen = 0.0f;
//    private const float DESTROY_AFTER = 5.0f;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * Time.deltaTime * spd;

//        _timeOnScreen += Time.deltaTime;
//        if (_timeOnScreen > DESTROY_AFTER)
//        {
//            Destroy(this.gameObject);
//        }

        // To save on memory, let's check if the bullet is off-screen and destroy it
        if (!render.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        _direction = Vector3.Normalize(newDirection);
    }
}
