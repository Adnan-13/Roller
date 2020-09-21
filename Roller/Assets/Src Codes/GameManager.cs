using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static User currentUser;

    public static int currentLevel = 1;

    public PlayerMovement playerMovement;
    public GameObject player;
    public GameObject obstacle;
    public GameObject levelPassedTrigger;

    public GameObject levelWonPanel;
    public GameObject levelLostPanel;
    public GameObject gamePausedPanel;
    public GameObject scorePanel;

    bool levelLost = false;
    bool levelWon = false;
    bool gameIsPaused = false;
    bool levelWonPanelActive = false;
    bool gamePausedPanelActive = false;


    Vector3 spawnPosOffset = new Vector3(0f, 0f, 0f);
    [SerializeField]
    int totalObstaclesToSpawn = 10;
    [SerializeField]
    float spawnDistance = 10f;
    [SerializeField]
    float gapBetweentwoObstacles = 10f;

    [SerializeField]
    Text currentLevelText = null;

    private void Start()
    {
        Debug.Log(currentLevel);

        changeSpeedAccordingToCurrentLevel();

        genarateObstacle(totalObstaclesToSpawn);

        genarateLevelPassedTrigger();

        showCurrentLevel();
    }

    private void showCurrentLevel()
    {
        currentLevelText.text = "Level:" + currentLevel.ToString();
    }

    private void changeSpeedAccordingToCurrentLevel()
    {
        PlayerMovement.forwardForce = 100f;

        for(int i = 1; i < currentLevel; i++)
        {
            increaseSpeed();
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel") && !levelLost)
        {
            if(!gameIsPaused)
            {
                Pause();
            }
            else if(gameIsPaused)
            {
                Resume();
            }
        }

        if(levelLost || levelWon)
        {
            scorePanel.SetActive(false);
        }
    }

    public void increaseSpeed()
    {
        PlayerMovement.forwardForce += 10f;
    }


    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        if (!gamePausedPanelActive)
        {
            gamePausedPanel.SetActive(true);
            gamePausedPanelActive = true;
        }


    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        if(gamePausedPanelActive)
        {
            gamePausedPanel.SetActive(false);
            gamePausedPanelActive = false;
        }

    }

    public void restart()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Debug.Log("Quit!");

        Application.Quit();
    }

    public void loadMainMenu()
    {
        currentLevel = 1;
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void nextLevel()
    {
        currentLevel++;
        //increaseSpeed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void levelPassed()
    {
        if(!levelWon && !levelLost)
        {
            Debug.Log("Level Won");

            FindObjectOfType<PlayerMovement>().enabled = false;
            levelWon = true;
            if (!levelWonPanelActive)
            {
                levelWonPanel.SetActive(true);
            }

        }
    }

    public void levelFailed()
    {
        if(!levelLost && !levelWon)
        {

            levelLost = true;
            playerMovement.enabled = false;

            levelLostPanel.SetActive(true);

        }
    }
    
    void genarateLevelPassedTrigger()
    {
        Instantiate(levelPassedTrigger, new Vector3(0f, 1.5f, spawnDistance += 10), Quaternion.identity );
    }

    void genarateObstacle(int t)
    {
        while(t-- > 0)
        {
            placeObstacle();
        }
    }

    void placeObstacle()
    {
        spawnPosOffset.x = Random.Range(-6.5f, 6.5f);
        spawnPosOffset.y = .5f;
        spawnPosOffset.z = spawnDistance;

        spawnDistance += gapBetweentwoObstacles;
        
        Instantiate(obstacle, player.transform.position.normalized + spawnPosOffset, Quaternion.identity);
    }
}
