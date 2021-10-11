using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ParsedLevelData levelData =
            JsonParser.ParseJsonFile(Application.dataPath + "/Resources/EnemySpawnTest.json");
            GameObject newPlayer = Instantiate(player);
            newPlayer.transform.position = new Vector3(0,0,0);
        for (int i = 0; i < levelData.Enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(levelData.Enemies[i]);
            newEnemy.transform.position = levelData.EnemyPositions[i];
            EnemyBehavior enemyComponent = newEnemy.GetComponent<EnemyBehavior>();
            enemyComponent.player = newPlayer;
        }
    }
}
