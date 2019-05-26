using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float minPauseTime = 1;
    public float maxPauseTime = 10;
    public float maxRotationChange = 30;
    public float directionChangeInterval = 1;

    CharacterController characterController;
    private Animator animator;
    private Vector3 targetRotation;
    private Vector3 movement;

    private float randomRotation;
    private bool pause = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        randomRotation = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, randomRotation, 0);

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        // Rotiert zufällig
        if (pause)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }
        else
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
            var forward = transform.TransformDirection(Vector3.forward);
            forward = Vector3.ClampMagnitude(forward, speed);
            forward *= Time.deltaTime;
            forward.y = gravity;
            characterController.Move(forward);
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(RandomPauseTime());
            Pausing();
            yield return new WaitForSeconds(RandomPauseTime());
        }
    }
    void NewHeadingRoutine()
    {
        pause = false;
        var floor = Mathf.Clamp(randomRotation - maxRotationChange, 0, 360);
        var ceil = Mathf.Clamp(randomRotation + maxRotationChange, 0, 360);
        randomRotation = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, randomRotation, 0);
    }

    void Pausing()
    {
        pause = true;
    }

    float RandomPauseTime()
    {
        return Random.Range(minPauseTime, maxPauseTime);
    }
}
