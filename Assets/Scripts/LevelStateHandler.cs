using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStateHandler : MonoBehaviour
{
    private enum GameState
    {
        Alive,
        GameOver,
        None
    }

    public GameObject levelLoaderObject;
    private LevelLoader _levelLoader;
    private static bool _isPaused;
    private GameState _currentGameState = GameState.None;
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        _levelLoader = levelLoaderObject.GetComponent<LevelLoader>();
        StartLevel();
    }

    private void StartLevel()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        GameObject playerObject =
            _levelLoader.LoadLevel("/Resources/EnemySpawnTest.json", transform, PlayerData.ShieldType);

        playerObject.GetComponent<PlayerControl>().AddDeathCallback(OnPlayerDeath);
        _currentGameState = GameState.Alive;
        pauseMenu.transform.Find("Quit").gameObject.GetComponent<Button>().onClick.AddListener(Quit);
        pauseMenu.transform.Find("Continue").gameObject.GetComponent<Button>().onClick.AddListener(Resume);
        pauseMenu.transform.Find("Restart").gameObject.GetComponent<Button>().onClick.AddListener(OnRestartButton);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            _isPaused = !_isPaused;
        }
    }

    private void OnPlayerDeath(GameObject player)
    {
        _currentGameState = GameState.GameOver;
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Quit()
    {
        Debug.Log("If we weren't in the editor, I would have quit.");
//        Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRestartButton()
    {
        pauseMenu.SetActive(false);
        RestartLevel();
    }
}