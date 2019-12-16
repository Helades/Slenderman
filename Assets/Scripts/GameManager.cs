using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    public GameObject player, slenderman;
    public AudioSource audiosource;
    public AudioClip scary;
    public PlayableDirector cinematic;

    public void AddNotes ()
    {
        player.GetComponent<Torchlight>().npage++;

        // Page 1: Crow Sound

        if (player.GetComponent<Torchlight>().npage == 1)
        {
            audiosource.clip = scary;
            audiosource.Play();
        }

        // Page 2: Cinematic

        else if (player.GetComponent<Torchlight>().npage == 2)
        {
            cinematic.Play();
        }

        // Page 3: Slenderman Stalks

        else if (player.GetComponent<Torchlight>().npage == 3)
        {
            slenderman.SetActive(true);
        }

        // Page 4: Slenderman Stalks Faster

        else if (player.GetComponent<Torchlight>().npage == 4)
        {
            slenderman.GetComponent<IAScript>().tpSpeed = 30f;
            slenderman.GetComponent<NavMeshAgent>().speed = 4;
        }

        // Page 5: Win

        else if (player.GetComponent<Torchlight>().npage == 5)
        {
            // Goes to Win Scene
            SceneManager.LoadScene("WinMenu");
        }
    }
}