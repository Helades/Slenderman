using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAScript : MonoBehaviour
{

    NavMeshAgent _navMeshAgent;
    public Transform[] points;
    public Transform player, slenderman;
    public GameObject playergo;
    public Renderer slender;
    public Camera ca;

    public float[] distance;
    public float tpSpeed, dmgDistance, counter;
    private bool isClose;


    void Start()
    {
        distance = new float[10];

        counter = 60f;
        tpSpeed = 60f;
        isClose = false;

        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        counter += Time.deltaTime;

        if (!isClose)
        {
            if (counter > tpSpeed)
            {
                // Get player position
                // Get closer point
                // Spawn Slenderman on the point

                SetDestination();
                counter = 0;
            }

            _navMeshAgent.SetDestination(slenderman.position);
        }
        else 
        {
            
            if (slender.isVisible)
            {
                // Stop Slenderman when being seen by the player

                _navMeshAgent.SetDestination(transform.position);
            }
            else if (!slender.isVisible)
            {
                // Move Slenderman slowly when far enough and when not looked at

                _navMeshAgent.SetDestination(player.position);
            }

            // If Slenderman is close to the player count damage depending on the distance
            // When damage gets to 100 the player dies   

            dmgDistance = Vector3.Distance(player.position, slenderman.position);

            if (dmgDistance > 20f || playergo.GetComponent<Torchlight>().hp < 0)
            {
                playergo.GetComponent<Torchlight>().hp -= Time.deltaTime * 50f;

                if (playergo.GetComponent<Torchlight>().hp < 0)
                {
                    playergo.GetComponent<Torchlight>().hp = 0;
                }
            }
            else if (dmgDistance < 5f)
            {
                playergo.GetComponent<Torchlight>().hp += Time.deltaTime * 36f;
            }
            else if (dmgDistance < 10f)
            {
                playergo.GetComponent<Torchlight>().hp += Time.deltaTime * 18f;
            }
            else if (dmgDistance < 15f)
            {
                playergo.GetComponent<Torchlight>().hp += Time.deltaTime * 12f;
            }
            else if (dmgDistance < 20f)
            {
                playergo.GetComponent<Torchlight>().hp += Time.deltaTime * 6f;
            }
        }      
    }

    void SetDestination ()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            distance[i] = Vector3.Distance(player.position, points[i].position);
        }

        Array.Sort(distance);

        float closerDistance = distance[0];

        for (int i = 0; i < points.Length - 1; i++)
        {
            if (closerDistance == Vector3.Distance(player.position, points[i].position))
            {
                transform.position = points[i].position;
            }
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isClose = true;
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            isClose = false;
        }
    }
}
