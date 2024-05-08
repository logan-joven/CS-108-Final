using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    private void Start()
    {
    }

    public void StartNewGameClicked()
    {
        StaticData.Health = 3;
        SceneManager.LoadScene("Level 1"); // load Level One when button is pressed
    }

    public void StartTutorialClicked()
    {
        StaticData.Health = 3;
        SceneManager.LoadScene("Draft Scene"); // load Tutorial when button is pressed
    }

    public void RetryLevelClicked()
    {
        StaticData.Health = 3;
        SceneManager.LoadScene(StaticData.currentLevel);
    }

    public void TitleLevelClicked()
    {
        SceneManager.LoadScene("Title");
    }
}
