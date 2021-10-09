using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	//Movement
	private float spd = 10;
	private float fspd = 2;
	private float moveBuff = 5;
	private Vector3 moveStore;
	//Rendering
	public Sprite defSprite;
	public Sprite focused;
	private SpriteRenderer spritRender;
	//Shooting
	//Defense
	//Pause/Menu

	// Use this for initialization
	void Start () {
		spritRender = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		//Movment and Focus Rendering
		if(Input.GetAxisRaw("Focus") > 0){
			spritRender.sprite = focused;
			transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * fspd, Input.GetAxisRaw("Vertical") * Time.deltaTime * fspd, 0);
			moveStore = new Vector3(0,0,0);
		}else{
			spritRender.sprite = defSprite;
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
