using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Movement
    private float spd = 8;
    private float fspd = 3;
    private float moveBuff = 5;

    private Vector3 moveStore;

    //Rendering
    public Sprite defSprite;
    public Sprite focSprite;
    public Color defCol;
    public Color transCol;
    public Color focCol;
    private SpriteRenderer spriteRender;
    private int sparks = 16;
    public GameObject sparkPrefab;

    public static PlayerControl Instance { get; private set; }

    private List<Action<GameObject>> _deathCallbacks = new List<Action<GameObject>>();
    //Shooting
    //Defense
    //Pause/Menu

    // Use this for initialization
    void Start()
    {
        Instance = this;
        spriteRender = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movment and Focus Rendering
        if (Input.GetAxisRaw("Focus") > 0)
        {
            moveStore = new Vector3(0, 0, 0);
            ;
            spriteRender.sprite = focSprite;
            spriteRender.color = focCol;
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * fspd,
                                              Input.GetAxisRaw("Vertical") * Time.deltaTime * fspd, 0);
        }
        else
        {
            spriteRender.sprite = defSprite;
            spriteRender.color = defCol;
            moveStore += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * spd,
                                     Input.GetAxisRaw("Vertical") * Time.deltaTime * spd, 0);
            Vector3 moveDelta = moveBuff * Time.deltaTime * moveStore;
            transform.position += moveDelta;
            moveStore -= moveDelta;
        }

        Camera m_cam = Camera.main;
        int scrw = m_cam.pixelWidth;
        int scrh = m_cam.pixelHeight;
        Vector3 screenPos = m_cam.WorldToScreenPoint(transform.position);
        if (screenPos.x >= scrw | screenPos.x <= 0 | screenPos.y >= scrh | screenPos.y <= 0)
        {
            this.SelfDestruct();
        }

        //Shooting
        //Defense
        //Pause/Menu
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" | other.gameObject.tag == "Enemy")
        {
            this.SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Vector3 dir = new Vector3(1, 0, 0);
        for (int i = 0; i < sparks; i++)
        {
            GameObject newSpark = Instantiate(sparkPrefab, transform.parent);
            newSpark.transform.position = transform.position;
            float angleDir = Mathf.Deg2Rad * i * 360 / sparks;
            dir = new Vector3(dir.x * Mathf.Cos(angleDir) - dir.y * Mathf.Sin(angleDir),
                              dir.x * Mathf.Sin(angleDir) + dir.y * Mathf.Cos(angleDir), 0);
            DeathSpark sparkComponent = newSpark.GetComponent<DeathSpark>();
            sparkComponent.col1 = defCol;
            sparkComponent.col2 = transCol;
            sparkComponent.col3 = focCol;
            sparkComponent.SetDirection(dir);
        }

        foreach (Action<GameObject> callback in _deathCallbacks)
        {
            callback(this.gameObject);
        }

        Destroy(this.gameObject);
    }

    public void AddDeathCallback(Action<GameObject> callback)
    {
        _deathCallbacks.Add(callback);
    }
}