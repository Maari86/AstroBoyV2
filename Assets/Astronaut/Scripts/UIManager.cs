using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Joystick joystick;
    public GameObject gameOverMenu;
    public GameObject pauseScreen;
    public GameObject resumeButton;
    public GameObject closeButton;
    [SerializeField] GameObject _rewardedAdButton;
    public GameObject[] backGround;
    public GameObject selectedBackGround;
    public GameObject[] player;
    public GameObject selectedPlayer;
    int playerIndex;
    int bgIndex;
    public static Vector2 lastCheckPointPos = new Vector2(0, 0);


    //public TextMeshProUGUI scoreText;
    public static int scoreValue = 0;


    private void Awake()
    {
        bgIndex = PlayerPrefs.GetInt("SelectedBackGround",0);
        selectedBackGround= Instantiate(backGround[bgIndex], lastCheckPointPos,Quaternion.identity);
        playerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        selectedPlayer = Instantiate(player[playerIndex], lastCheckPointPos, Quaternion.identity);
        Time.timeScale = 1;
        resumeButton = GameObject.Find("ResumeButton");
    }

    public GameObject GetSelectedPlayer()
    {
        return selectedPlayer;
    }
    private void OnEnable()
    {
      //  Health.OnPlayerDeath += EnableGameOverMenu;
        Health.OnPlayerDeath += EnableRewardedAdButton;
    }

    private void OnDisable()
    {
        //Health.OnPlayerDeath -= EnableGameOverMenu;
        Health.OnPlayerDeath -= EnableRewardedAdButton;

    }


    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void EnableRewardedAdButton()
    {
        _rewardedAdButton.SetActive(true);
    }

    public void DisableRewardedAdButton()
    {
        _rewardedAdButton.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }

        //  score = (int)Time.time;
        //  scoreText.text = "Score : " + score.ToString() + "s";
    }
    #region Game Over
 
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        ScoreCounter.scoreValue = 0;

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        ScoreCounter.scoreValue = 0;

    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

           if (status)
        {
            Time.timeScale = 0;
            resumeButton.SetActive(true);
            closeButton.SetActive(true);
        }

        else
        {
            Time.timeScale = 1;
            resumeButton.SetActive(false);
            closeButton.SetActive(false);
        }
    }
             
    #endregion
   public void ClosePauseScreen()
    {
        PauseGame(false);
    }
  
}
