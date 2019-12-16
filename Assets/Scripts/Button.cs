using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string type;

    public void NextScene ()
    {
        if (type == "Play")
        {
            SceneManager.LoadScene("LoadingScene");
        }
        else if (type == "Quit")
        {
            Application.Quit();
        }
    }
}