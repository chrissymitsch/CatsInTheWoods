using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPointsScript : MonoBehaviour
{
    public GameObject point;
    public GameObject effect;
    public int points = 1;
    [SerializeField] public Text punkteText;

    private bool triggered = false;

    private void Start()
    {
        effect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggered)
        {
            // Sammeln
            point.SetActive(false);
            GameObject splash = GameObject.Find("Splash");
            effect.SetActive(true);

            // take value from UI text, parse it to int, 
            // increase it and set it back to the UI text
            punkteText.text = (int.Parse(punkteText.text) + points).ToString();

            triggered = false;
        }
    }

}
