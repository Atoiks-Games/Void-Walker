using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	//Movement
	private float spd = 8;
	private float fspd = 3;
	private float moveBuff = 5;
	private Vector3 moveStore;
	//Rendering
	public Sprite defSprite;
	public Sprite focSprite;
	public Color defCol;
	public Color focCol;
	private SpriteRenderer spriteRender;
	//Shooting
	//Defense
	//Pause/Menu

	// Use this for initialization
	void Start () {
		spriteRender = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		//Movment and Focus Rendering
		if(Input.GetAxisRaw("Focus") > 0){
			moveStore = new Vector3(0,0,0);;
			spriteRender.sprite = focSprite;
			spriteRender.color = focCol;
			transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * fspd, Input.GetAxisRaw("Vertical") * Time.deltaTime * fspd, 0);
		}else{
			spriteRender.sprite = defSprite;
			spriteRender.color = defCol;
			moveStore += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * spd, Input.GetAxisRaw("Vertical") * Time.deltaTime * spd, 0);
			Vector3 moveDelta = moveStore * Time.deltaTime * moveBuff;
			transform.position += moveDelta;
			moveStore -= moveDelta;
		}

		//Shooting
		//Defense
		//Pause/Menu

	}
}
