using UnityEngine;
using UnityEngine.UI;


public class LevelWonPanelScore : MonoBehaviour
{
    Score scoreScript;

    [SerializeField]
    Text wonPanelScoreText = null;


    private void Awake()
    {
        scoreScript = FindObjectOfType<Score>();
    }

    void Update()
    {
        wonPanelScoreText.text = "Score: " + scoreScript.score.ToString("0");
    }
}
