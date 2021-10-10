using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct ParsedLevelData
{
    public GameObject[] Enemies;
    public Vector2[] EnemyPositions;
}

public static class JsonParser
{
    private struct ParsedJsonData
    {
        public int[] EnemyIndices;
        public float[] EnemyXPositions;
        public float[] EnemyYPositions;
    }

    public static ParsedLevelData ParseJsonFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        ParsedJsonData parsedJsonData = JsonUtility.FromJson<ParsedJsonData>(jsonString);
        ParsedLevelData levelData = new ParsedLevelData();

        List<GameObject> enemyList = new List<GameObject>();
        List<Vector2> positionsList = new List<Vector2>();

        for (int i = 0; i < parsedJsonData.EnemyIndices.Length; i++)
        {
            GameObject enemyObject = EnemyDictionary.GetEnemyPrefab(parsedJsonData.EnemyIndices[i]);
            enemyList.Add(enemyObject);
            positionsList.Add(new Vector2(parsedJsonData.EnemyXPositions[i], parsedJsonData.EnemyYPositions[i]));
        }

        levelData.Enemies = enemyList.ToArray();
        levelData.EnemyPositions = positionsList.ToArray();
        return levelData;
    }
}