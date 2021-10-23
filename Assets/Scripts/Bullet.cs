using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletInitializationData
{
}

public abstract class Bullet : MonoBehaviour
{
    protected SpriteRenderer Render;

    protected void Start()
    {
        Render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public abstract void Update();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public abstract void InitializeBullet();

    public void InitializeBullet(BulletInitializationData bulletInitializationData)
    {
        InitializeBullet();
    }
}