using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int mode = 0;
    public GameObject player;
    private float buffer = 0.6f;
    private Vector3 pos;
    private Vector3 campos;
    private float height;
    private float moveBuff = 1;
    private Vector3 moveStore = new Vector3 (0,0,0);

    void Start(){
        height = (float)Camera.main.pixelHeight;
    }
    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            return;
        }
        pos = player.transform.position;
        campos = Camera.main.WorldToScreenPoint(pos);
        switch(mode){
            default:
                //Camera Does Not Move
                break;
            case 1:
                //Camera Moves Centered on Player
                transform.position = new Vector3 (transform.position.x, pos.y, transform.position.z);
                break;
            case 2:
                //Camera Moves Following Player w/ Padding
                if(campos.y >= buffer * height){
                        transform.position = new Vector3 (transform.position.x, transform.position.y + pos.y - Camera.main.ScreenToWorldPoint(new Vector3 (0, buffer * height, 0)).y, transform.position.z);
                }
                if(campos.y <= (1-buffer) * height){
                        transform.position = new Vector3 (transform.position.x, transform.position.y + pos.y - Camera.main.ScreenToWorldPoint(new Vector3 (0, (1-buffer) * height, 0)).y, transform.position.z);
                }
                break;
            case 3:
                //Camera Autoscrolls
                transform.position += new Vector3 (0, moveBuff, 0) * Time.deltaTime;
                break;
            case 4:
                //Funky Mode
                float dif = pos.y - transform.position.y;
                if(campos.y >= buffer * height | campos.y <= (1 - buffer) * height){
                    moveStore += Mathf.Sign(dif) * new Vector3(0, Mathf.Pow(Mathf.Abs(dif), 2.25f), 0) * Time.deltaTime;
                } else {
                    moveStore = moveStore * 0.95f;
                }
                Vector3 moveDelta = moveStore * Time.deltaTime * moveBuff;
                transform.position += moveDelta;
                moveStore -= moveDelta;
                break;


        }
    }
}
