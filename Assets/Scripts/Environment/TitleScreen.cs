using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject controls;
    public void Start()
    {
        FindObjectOfType<AudioManager>().plyAudio("titlebgm");
    }
    public void startGame()
    {
        FindObjectOfType<AudioManager>().plyAudio("confirm");
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }


    public void options()
    {
        FindObjectOfType<AudioManager>().plyAudio("confirm");
        mainScreen.SetActive(false);
        controls.SetActive(true);
    }
    public void backToMain()
    {
        mainScreen.SetActive(true);
        controls.SetActive(false);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
