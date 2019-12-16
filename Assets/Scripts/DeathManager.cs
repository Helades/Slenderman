using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private float timing = 0;
    public CanvasGroup cg;

    void Update()
    {
        timing += Time.deltaTime;

        if (timing < 1f)
        {
            cg.alpha += Time.deltaTime;
        }
        else if (timing > 2f)
        {
            cg.alpha -= Time.deltaTime;
        }

        if (timing > 3f)
        {
            timing = 0;
            SceneManager.LoadScene("StartMenu");
        }
    }
}