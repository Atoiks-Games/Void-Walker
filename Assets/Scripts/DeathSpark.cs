using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpark : MonoBehaviour
{
    private SpriteRenderer render;
    private int mode = 0;
    private float tm = 0f;
    private float chngTm = 0.5f;
    public Sprite spr1;
    public Sprite spr2;
    public Sprite spr3;
    public Color col1;
    public Color col2;
    public Color col3;
    private Sprite[] sprs;
    private Color[] cols;
    private Color alph = new Color (0, 0, 0, 1);

    private float spd = 10f;
    private Vector3 _direction = Vector2.left;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        sprs = new Sprite[] {spr1, spr2, spr3, spr2};
        cols = new Color[] {col1, col2, col3, col2};
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * Time.deltaTime * spd;
        render.color = cols[mode];
        col1 = col1 - alph * Time.deltaTime;
        col2 = col2 - alph * Time.deltaTime;
        col3 = col3 - alph * Time.deltaTime;
        cols = new Color[] {col1, col2, col3, col2};
        render.sprite = sprs[mode];
        tm += Time.deltaTime;
        if (tm > chngTm)
        {
            tm = 0f;
            mode = (mode + 1) % 4;
        }
        if (col1.a <= 0){
            Destroy(this.gameObject);
        }

    }

    public void SetDirection(Vector2 newDirection)
    {
        _direction = Vector3.Normalize(newDirection);
    }
}
