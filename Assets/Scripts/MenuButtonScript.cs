using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    private void Start()
    {
    }

    public void Clicked()
    {
        SceneManager.LoadScene("Draft Scene"); // load Level One when button is pressed
    }

}
