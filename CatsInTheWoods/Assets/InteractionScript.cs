using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public GameObject NPC;
    private bool triggered = false;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Boom");
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggered && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("E pressed");
            // Reden
            if (NPC.tag == "NPC")
            {
                Debug.Log("Blablabla");
            }

            triggered = false;
        }
    }

}
