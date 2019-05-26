using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Hello");
        }
    }
}
