using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    private float cam_z = -10f;

    private ParsedLevelData _levelData;

    private string _levelFilePath;

    private void LoadLevelData(string levelFilePath)
    {
        if (levelFilePath == _levelFilePath)
        {
            return;
        }

        _levelData =
            JsonParser.ParseJsonFile(Application.dataPath + levelFilePath);
        _levelFilePath = levelFilePath;
    }

    public GameObject LoadLevel(string levelFilePath, Transform parentTransform)
    {
        LoadLevelData(levelFilePath);
        GameObject newPlayer = Instantiate(player, parentTransform);
        newPlayer.transform.position = new Vector3(0, 0, 0);
        GameObject m_cam = Instantiate(camera, parentTransform);
        m_cam.transform.position = new Vector3(0, 0, cam_z);
        CameraMovement camComponent = m_cam.GetComponent<CameraMovement>();
        camComponent.player = newPlayer;
        for (int i = 0; i < _levelData.Enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(_levelData.Enemies[i], parentTransform);
            newEnemy.transform.position = _levelData.EnemyPositions[i];
            EnemyBehavior enemyComponent = newEnemy.GetComponent<EnemyBehavior>();
            enemyComponent.player = newPlayer;
        }

        return newPlayer;
    }
}