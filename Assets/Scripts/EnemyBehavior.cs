using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform _playerPos;
    private float _time = 0.0f;

    private const float _bulletCooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.position);
        _time += Time.deltaTime;
        if (_time > _bulletCooldown)
        {
            _time = 0;
            GameObject newBullet = Instantiate(bulletPrefab);
            Vector3 position = transform.position;
            newBullet.transform.position = position;
            Bullet bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.SetDirection(_playerPos.position - position);
        }
    }
}
