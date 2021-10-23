using UnityEngine;

public class ShootAtPlayerBullet : Bullet
{
    private float spd = 10f;
    private Vector3 _direction = Vector2.left;

    protected new void Start()
    {
        base.Start();
    }

    public override void Update()
    {
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

        SetDirection(PlayerControl.Instance.transform.position - transform.position);
    }

    public void SetDirection(Vector2 newDirection)
    {
        _direction = Vector3.Normalize(newDirection);
    }
}