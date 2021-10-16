using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelStateHandler : MonoBehaviour
{
    private enum GameState
    {
        Alive,
        GameOver,
        None
    }

    public GameObject LevelLoaderObject;
    private LevelLoader _levelLoader;

    private GameState _currentGameState = GameState.None;


    // Start is called before the first frame update
    void Start()
    {
        _levelLoader = LevelLoaderObject.GetComponent<LevelLoader>();
        StartLevel();
    }

    private void StartLevel()
    {
        GameObject playerObject = _levelLoader.LoadLevel("/Resources/EnemySpawnTest.json", this.transform);
        playerObject.GetComponent<PlayerControl>().AddDeathCallback(OnPlayerDeath);
        _currentGameState = GameState.Alive;
    }

    private void RestartLevel()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGameState == GameState.GameOver)
        {
            if (Input.GetAxisRaw("Continue") > 0)
            {
                RestartLevel();
            }
        }
    }

    private void OnPlayerDeath(GameObject player)
    {
        _currentGameState = GameState.GameOver;
    }
}