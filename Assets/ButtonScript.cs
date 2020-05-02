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

    public float timer;
    bool timerStarted = false;
    public TextMeshProUGUI timerText;

    bool paused = false;

    private static ButtonScript scriptInstance;

    public TextMeshProUGUI scoreText;

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

    //https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

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
        timer = 120;
        timerStarted = true;
    }

    public void EndGame()
    {
        //write score text?
        var g = GameObject.Find("PointsArea");
        if(g.GetComponent<pointTally>())
        {
            scoreText.text = "SCORE $" + g.GetComponent<pointTally>().pointsTotal;
        }

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
        timer = 120;
        StartMenu.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
