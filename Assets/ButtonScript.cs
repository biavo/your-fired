using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject StartMenu;
    public GameObject PauseMenu;
    public GameObject GameUI;
    public GameObject Crosshair;
    public GameObject EndScreen;

    public GameObject InstructionsScreen1;
    public GameObject InstructionsScreen2;

    public GameObject AchievementsScreen;

    public float timer;
    bool timerStarted = false;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI ScoreTextGameUIt;
    public TextMeshProUGUI ItemsStolenText;
    public TextMeshProUGUI GoldenItemsText;

    bool paused = false;

    private static ButtonScript scriptInstance;

    public TextMeshProUGUI scoreText;

    public int currentScore;

    //Achievements section
    int HighScore;
    string Ach5gold;
    string Ach5gold1min;
    string AchScore3000;
    string AchScore5000;
    string Ach50Pens;

    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI Ach5goldText;
    public TextMeshProUGUI Ach5gold1minText;
    public TextMeshProUGUI AchScore3000Text;
    public TextMeshProUGUI AchScore5000Text;
    public TextMeshProUGUI Ach50PensText;

    void Save()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.SetString("Ach5gold", Ach5gold);
        PlayerPrefs.SetString("Ach5gold1min", Ach5gold1min);
        PlayerPrefs.SetString("AchScore3000", AchScore3000);
        PlayerPrefs.SetString("AchScore5000", AchScore5000);
        PlayerPrefs.SetString("Ach50Pens", Ach50Pens);
    }

    void Load()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if (PlayerPrefs.HasKey("Ach5gold"))
        {
            Ach5gold = PlayerPrefs.GetString("Ach5gold");
        }
        else
        {
            PlayerPrefs.SetString("Ach5gold", "false");
        }

        if (PlayerPrefs.HasKey("Ach5gold1min"))
        {
            Ach5gold1min = PlayerPrefs.GetString("Ach5gold1min");
        }
        else
        {
            PlayerPrefs.SetString("Ach5gold1min", "false");
        }

        if (PlayerPrefs.HasKey("AchScore3000"))
        {
            AchScore3000 = PlayerPrefs.GetString("AchScore3000");
        }
        else
        {
            PlayerPrefs.SetString("AchScore3000", "false");
        }

        if (PlayerPrefs.HasKey("AchScore5000"))
        {
            AchScore5000 = PlayerPrefs.GetString("AchScore5000");
        }
        else
        {
            PlayerPrefs.SetString("AchScore5000", "false");
        }


        if (PlayerPrefs.HasKey("Ach50Pens"))
        {
            Ach50Pens = PlayerPrefs.GetString("Ach50Pens");
        }
        else
        {
            PlayerPrefs.SetString("Ach50Pens", "false");
        }

        Save();
        prepareAchievementsText();
    }

    public void SetHighScore(int num)
    {
        if(HighScore < num)
        {
            HighScore = num;
        }

        if(HighScore >= 3000)
        {
            AchScore3000 = "true";
        }

        if (HighScore >= 5000)
        {
            AchScore5000 = "true";
        }

    }

    public void SetGoldItemsAch()
    {
        Ach5gold = "true";
        //if achieved in under 1 minute:
        if(timer >= 240f)
        {
            Ach5gold1min = "true";
        }
    }

    public void SetAch50Pens()
    {
        Ach50Pens = "true";
        //Save();
    }

    void prepareAchievementsText()
    {
        HighScoreText.text = "High Score: " + HighScore;

        if(Ach5gold == "true")
        {
            Ach5goldText.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            Ach5goldText.fontStyle = FontStyles.Normal;
        }

        if (Ach5gold1min == "true")
        {
            Ach5gold1minText.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            Ach5gold1minText.fontStyle = FontStyles.Normal;
        }

        if (AchScore3000 == "true")
        {
            AchScore3000Text.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            AchScore3000Text.fontStyle = FontStyles.Normal;
        }

        if (AchScore5000 == "true")
        {
            AchScore5000Text.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            AchScore5000Text.fontStyle = FontStyles.Normal;
        }

        if (Ach50Pens == "true")
        {
            Ach50PensText.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            Ach50PensText.fontStyle = FontStyles.Normal;
        }
    }


    void Awake()
    {
        //https://answers.unity.com/questions/982403/how-to-not-duplicate-game-objects-on-dontdestroyon.html
        DontDestroyOnLoad(gameObject);
        if (scriptInstance == null)
        {
            scriptInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted && !paused)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                EndGame();
            }
        }

        if(!paused && timerStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            //pause?
            paused = true;
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(paused && timerStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            UnPause();
        }


    }

    public void setScoreGameUI(int num)
    {
        ScoreTextGameUIt.text = "Score: $" + num;
    }

    public void setItemsStolenGameUI(int num)
    {
        ItemsStolenText.text = "Items Stolen: " + num;
    }

    public void setGoldenItemsGameUI(int num)
    {
        if(num == 5)
        {
            GoldenItemsText.text = "Golden Items: " + num + "/5 +$1000";
        }
        else
        {
            GoldenItemsText.text = "Golden Items: " + num + "/5";
        }
    }

    //https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("Time Remaining: {0:0}:{1:00}", minutes, seconds);

        timerText.text = niceTime;
        //GUI.Label(new Rect(10, 10, 250, 100), niceTime);
    }

    public void UnPause()
    {
        if (paused && timerStarted)
        {
            paused = false;
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



    public void AchievementsBtn()
    {
        AchievementsScreen.SetActive(true);
        StartMenu.SetActive(false);
    }

    public void AchievementBackBtn()
    {
        AchievementsScreen.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void PlayBtn1()
    {
        StartMenu.SetActive(false);
        InstructionsScreen1.SetActive(true);
    }

    public void NextBtn()
    {
        InstructionsScreen1.SetActive(false);
        InstructionsScreen2.SetActive(true);
    }

    public void StartGame()
    {
        //Load the game... how to do tutorial??
        Time.timeScale = 1f;
        SceneManager.LoadScene("Office1");
        InstructionsScreen2.SetActive(false);
        GameUI.SetActive(true);
        Crosshair.SetActive(true);
        timer = 300;
        timerStarted = true;
    }

    public void EndGame()
    {
        scoreText.text = "SCORE $" + currentScore;
        SetHighScore(currentScore);
        prepareAchievementsText();
        Save();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameUI.SetActive(false);
        Crosshair.SetActive(false);
        PauseMenu.SetActive(false);
        EndScreen.SetActive(true);
        paused = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Intro");
        timerStarted = false;
        timer = 300;
        StartMenu.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
