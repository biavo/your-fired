using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Load the game... how to do tutorial??
        SceneManager.LoadScene("Office1");
        gameObject.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
