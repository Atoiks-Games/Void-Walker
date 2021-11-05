using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

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
    public static bool IsPaused = false;
    private GameState _currentGameState = GameState.None;
    public GameObject PauseMenu;


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
        PauseMenu.transform.Find("Quit").gameObject.GetComponent<Button>().onClick.AddListener(Quit);
        PauseMenu.transform.Find("Continue").gameObject.GetComponent<Button>().onClick.AddListener(Resume);
        PauseMenu.transform.Find("Restart").gameObject.GetComponent<Button>().onClick.AddListener(Resume);
        PauseMenu.transform.Find("Restart").gameObject.GetComponent<Button>().onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused){
                Resume();
            } else {
                Pause();
            }
            IsPaused = !IsPaused;
        }
    }

    private void OnPlayerDeath(GameObject player)
    {
        _currentGameState = GameState.GameOver;
    }

    private void Resume(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Quit(){
        Debug.Log("If we weren't in the editor, I would have quit.");
        Application.Quit();
    }
}
