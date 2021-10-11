using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    private float cam_z = -10f;
    // Start is called before the first frame update
    void Start()
    {
        ParsedLevelData levelData =
            JsonParser.ParseJsonFile(Application.dataPath + "/Resources/EnemySpawnTest.json");
        GameObject newPlayer = Instantiate(player);
        newPlayer.transform.position = new Vector3(0,0,0);
        GameObject m_cam = Instantiate(camera);
        m_cam.transform.position = new Vector3(0,0,cam_z);
        CameraMovement camComponent = m_cam.GetComponent<CameraMovement>();
        camComponent.player = newPlayer;
        for (int i = 0; i < levelData.Enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(levelData.Enemies[i]);
            newEnemy.transform.position = levelData.EnemyPositions[i];
            EnemyBehavior enemyComponent = newEnemy.GetComponent<EnemyBehavior>();
            enemyComponent.player = newPlayer;
        }
    }
}
