using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    TMP_InputField level = null;

    int maxLevel = 50;
    public void StartGameWithCurrentLevel()
    {
        Debug.Log(level.text);

        String n = level.text;

        int selectedLevel = int.Parse(n);

        if (selectedLevel < 1)
        {
            selectedLevel = 1;
        }
        else if (selectedLevel > maxLevel)
        {
            selectedLevel = maxLevel;
        }

        GameManager.currentLevel = selectedLevel;

        SceneManager.LoadScene("NewGame");

    }
}
