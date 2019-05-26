using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTriggerScript : MonoBehaviour
{
    public bool boatPositioned = false;

    private GameObject triggeringGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!boatPositioned && other.gameObject.layer == LayerMask.NameToLayer("Vehicle"))
        {
            triggeringGameObject = other.gameObject;
            boatPositioned = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggeringGameObject)
        {
            boatPositioned = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
