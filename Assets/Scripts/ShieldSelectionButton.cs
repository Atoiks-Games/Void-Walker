using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShieldSelectionButton : MonoBehaviour
{
    public ShieldType shieldType;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        PlayerData.ShieldType = shieldType;
        SceneManager.LoadScene("SampleScene");
    }
}