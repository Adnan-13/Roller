using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float highestScore = -1;

    [SerializeField]
    GameObject player = null;
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    float scoreController = 10.0f;

    PlayerMovement playerMovement;

    public float score = 0f;
    private void Start()
    {
        highestScore = GameManager.currentUser.score;
    }
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        float scoreMultiplier = PlayerMovement.forwardForce / scoreController;

        score = player.transform.position.z * scoreMultiplier;

        scoreText.text = score.ToString("0");

        checkHighestScore();
    }

    private void checkHighestScore()
    {
        if (score > highestScore)
        {
            highestScore = score;

            GameManager.currentUser.score = score;
        }
    }
}
