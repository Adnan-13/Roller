using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class LoadHighestScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highestScoreText = null;

    void Update()
    {
        //highestScoreText.text = PlayerPrefs.GetFloat("HighestScore", 0f).ToString("0");
        highestScoreText.text = GameManager.currentUser.score.ToString("0");
    }

    public void resetHighestScore()
    {
        //PlayerPrefs.SetFloat("HighestScore", 0f);

        GameManager.currentUser.score = 0f;
    }
}
