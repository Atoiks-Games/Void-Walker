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

    private int sparks = 8;
	public GameObject sparkPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _bulletCooldown)
        {
            _time = 0;
            GameObject newBullet = Instantiate(bulletPrefab, transform.parent);
            Vector3 position = transform.position;
            newBullet.transform.position = position;
            Bullet bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.SetDirection(_playerPos.position - position);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
    	if (other.gameObject.tag == "Player"){
        	this.SelfDestruct();
    	}
	}

    public void SelfDestruct(){
		Vector3 dir = new Vector3(1, 0, 0);
		for(int i = 0; i < sparks; i++)
        {
			GameObject newSpark = Instantiate(sparkPrefab);
			newSpark.transform.position = transform.position;
			float angleDir = Mathf.Deg2Rad * i * 360/sparks;
			dir = new Vector3(dir.x * Mathf.Cos(angleDir) - dir.y * Mathf.Sin(angleDir), dir.x * Mathf.Sin(angleDir) + dir.y * Mathf.Cos(angleDir), 0);
			DeathSpark sparkComponent = newSpark.GetComponent<DeathSpark>();
			sparkComponent.SetDirection(dir);
        }
		Destroy(this.gameObject);
	}
}
