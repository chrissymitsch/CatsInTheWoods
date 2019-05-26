using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public bool allowMotion = true;
    public float speed = 5.0f;
    public float mouseSensibilitaet = 10;
    public GameObject player;
    public CharacterController playerController;
    public GameObject neededObject;

    private InventarScript inventar;
    private bool neededObjectInItems = false;
    private bool triggered = false;
    private bool steuerung = false;
    private float verticalVelocity;
    private CharacterController vehicleController;
    private CatAnimatorScript catAnimatorScript;

    private void Start()
    {
        inventar = GameObject.FindGameObjectWithTag("Player").GetComponent<InventarScript>();
        vehicleController = GetComponent<CharacterController>();
        catAnimatorScript = playerController.transform.GetComponent<CatAnimatorScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Vehicle triggered");
            steuerung = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            catAnimatorScript.vehicleActive = false;
            steuerung = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            for (int i = 0; i < inventar.items.Length; i++)
            {
                if (inventar.items[i].name == "VehiclePaddel")
                {
                    neededObjectInItems = true;
                    break;
                }
            }

            if (allowMotion && neededObjectInItems && !steuerung && Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Enter Steuerung");
                playerController.transform.parent = transform;
                playerController.enabled = false;
                catAnimatorScript.vehicleActive = true;
                steuerung = true;
            }
        }
    }

    void Update()
    {
        if (allowMotion && steuerung)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

            Vector3 movement = new Vector3(deltaX, 0, deltaZ);

            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = 0;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            vehicleController.Move(movement);

            // Rotation
            if (Input.GetMouseButton(1))
            {
                float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensibilitaet;
                transform.Rotate(0, horizontalRotation, 0);
            }

            Vector3 offset = new Vector3(0, verticalVelocity, 0);
            vehicleController.Move(offset * Time.deltaTime);

            if (Input.GetKeyUp(KeyCode.Q))
            {
                quitSteuerung();
            }
        }
        if (!allowMotion && steuerung)
        {
            quitSteuerung();
        }
    }

    void quitSteuerung()
    {
        Debug.Log("Quit Steuerung");
        playerController.transform.parent = null;
        playerController.enabled = true;
        catAnimatorScript.vehicleActive = false;
        steuerung = false;
    }
}
