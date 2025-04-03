using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI,diffMenuUI;

    [SerializeField]
    BallScript ball;

    [SerializeField]
    GameObject resumeBT;

    [SerializeField]
    InstructionsScript instructions;

    [SerializeField] TMP_Text hardBTtxt, MedBTtxt, EasyBTtxt;

    [SerializeField] string CurrentDiff = "Easy", SelectedDiff = "Easy";

    bool isLost = false;
    bool isInDiffMenu = false;

    private void Start()
    {
        CurrentDiff = "Easy";
        SelectedDiff = "Easy";
        int bestScore = PlayerPrefs.GetInt("HighScore");
        ball.setBestScore(bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (isLost)
                {
                    return;
                }
                else if (isInDiffMenu)
                {
                    return;
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(GameIsPaused)
            {
                RestartMenu();
            }
        }

        if(Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }

        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        resumeBT.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;    
    }

    public void LoseScreen()
    {
        pauseMenuUI.SetActive(true);
        resumeBT.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        isLost = true;
    }

    public void RestartMenu()
    {
        ball.Restart();
        resumeBT.SetActive(true);
        instructions.StartInstructionsTimer();
        CurrentDiff = SelectedDiff;
    }

    public void LoadDiffMenu()
    {
        diffMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        isInDiffMenu = true;
    }

    public void HardBT()
    {
        hardBTtxt.color = Color.red;
        MedBTtxt.color = Color.white;
        EasyBTtxt.color = Color.white;
        SelectedDiff = "Hard";
    }

    public void MediumBT()
    {
        MedBTtxt.color = Color.yellow;
        hardBTtxt.color = Color.white;
        EasyBTtxt.color = Color.white;
        SelectedDiff = "Medium";
    }

    public void EasyBT()
    {
        EasyBTtxt.color = Color.green;
        hardBTtxt.color = Color.white;
        MedBTtxt.color = Color.white;
        SelectedDiff = "Easy";
    }

    public void BackBT()
    {
        diffMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        if (!(CurrentDiff.Equals(SelectedDiff)))
        {
            resumeBT.SetActive(false);
        }
        else
        {
            resumeBT.SetActive(true);
        }    
    }

    public string getDiff()
    {
        return CurrentDiff.ToString();
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("HighScore",ball.getBestScore());
        PlayerPrefs.Save();
        Application.Quit();
    }



}
