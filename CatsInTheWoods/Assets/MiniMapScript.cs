using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapScript : MonoBehaviour
{
    public Camera miniMapCamera;
    public Button plusButton;
    public Button minusButton;
    public SpriteRenderer playerSprite;
    private float max = 45f;
    private float min = 10f;

    // Start is called before the first frame update
    void Start()
    {
        plusButton.onClick.AddListener(ZoomIn);
        minusButton.onClick.AddListener(ZoomOut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ZoomIn()
    {
        Debug.Log("Zoom in");
        if (miniMapCamera.orthographicSize > min)
        {
            miniMapCamera.orthographicSize--;
        }
    }

    private void ZoomOut()
    {
        Debug.Log("Zoom out");
        if (miniMapCamera.orthographicSize < max)
        {
            miniMapCamera.orthographicSize++;
        }
    }
}
