using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using FMODUnity;

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

    public GameObject OptionsScreen;

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

    public StudioEventEmitter StartMenuMusic;
    public StudioEventEmitter GameMusic;

    public StudioEventEmitter MenuSelectSound;
    public StudioEventEmitter PauseMenuOpenSound;
    public StudioEventEmitter PauseMenuCloseSound;

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
            PauseMenuOpenSound.Play();
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
            PauseMenuCloseSound.Play();
            paused = false;
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OptionsBtnFromPause()
    {
        MenuSelectSound.Play();
        OptionsScreen.SetActive(true);
        PauseMenu.SetActive(false);
    }

    //public void OptionsBackBtnFromPause()
    //{
    //    OptionsScreen.SetActive(false);
    //    PauseMenu.SetActive(true);
    //}

    public void OptionsBtn()
    {
        MenuSelectSound.Play();
        OptionsScreen.SetActive(true);
        StartMenu.SetActive(false);
    }

    public void OptionsBackBtn()
    {
        MenuSelectSound.Play();
        OptionsScreen.SetActive(false);

        if(paused)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            StartMenu.SetActive(true);
        }
    }

    public void AchievementsBtn()
    {
        MenuSelectSound.Play();
        AchievementsScreen.SetActive(true);
        StartMenu.SetActive(false);
    }

    public void AchievementBackBtn()
    {
        MenuSelectSound.Play();
        AchievementsScreen.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void PlayBtn1()
    {
        MenuSelectSound.Play();
        Crosshair.SetActive(true);
        SceneManager.LoadScene("Office1");
        StartMenu.SetActive(false);
        InstructionsScreen1.SetActive(true);
    }

    public void NextBtn()
    {
        MenuSelectSound.Play();
        InstructionsScreen1.SetActive(false);
        InstructionsScreen2.SetActive(true);
    }

    public void StartGame()
    {
        MenuSelectSound.Play();
        //Load the game... how to do tutorial??
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InstructionsScreen2.SetActive(false);
        GameUI.SetActive(true);
        //Crosshair.SetActive(true);
        timer = 300;
        timerStarted = true;

        StartMenuMusic.Stop();
        GameMusic.Play();
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

        StartMenuMusic.Play();
        GameMusic.Stop();
    }

    public void QuitToMenu()
    {
        MenuSelectSound.Play();
        SceneManager.LoadScene("Intro");
        timerStarted = false;
        timer = 300;
        StartMenu.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void playClickSound()
    {
        MenuSelectSound.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
