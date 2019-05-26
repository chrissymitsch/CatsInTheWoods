using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemsScript : MonoBehaviour
{
    public GameObject player;

    private InventarScript inventar;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        inventar = GameObject.FindGameObjectWithTag("Player").GetComponent<InventarScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && triggered && Input.GetKeyUp(KeyCode.E))
        {
            for (int i = 0; i < inventar.slots.Length; i++)
            {
                if (!inventar.isFull[i])
                {
                    // collect item
                    inventar.isFull[i] = true;
                    inventar.items[i] = gameObject;
                    gameObject.SetActive(false);
                    break;
                }
            }

            triggered = false;
        }
    }
}
