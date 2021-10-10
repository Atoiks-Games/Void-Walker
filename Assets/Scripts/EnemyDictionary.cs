using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyDictionary : MonoBehaviour
{
    [SerializeField]
    private GameObject[] editorEnemyPrefabArray;
    private static GameObject[] _enemyPrefabArray;
    
    private void Awake()
    {
        _enemyPrefabArray = editorEnemyPrefabArray;
    }

    public static GameObject GetEnemyPrefab(int index)
    {
        Assert.IsTrue(index < _enemyPrefabArray.Length);
        return _enemyPrefabArray[index];
    }
}