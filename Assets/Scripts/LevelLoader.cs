using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParsedLevelData levelData =
            JsonParser.ParseJsonFile(Application.dataPath + "/Resources/EnemySpawnTest.json");
        for (int i = 0; i < levelData.Enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(levelData.Enemies[i]);
            newEnemy.transform.position = levelData.EnemyPositions[i];
        }
    }
}