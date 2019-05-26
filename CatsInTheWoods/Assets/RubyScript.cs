using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyScript : MonoBehaviour
{
    public Transform trigger1;
    public Transform trigger2;
    public Transform boot1;
    public Transform boot2;
    public int points = 1;
    [SerializeField] public Text PunkteText;

    private Animator animator;
    private bool jumped = false;
    private AnimatorStateInfo animatorStateInfo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!jumped && trigger1.transform.GetComponent<BoatTriggerScript>().boatPositioned && trigger2.transform.GetComponent<BoatTriggerScript>().boatPositioned)
        {
            boot1.transform.GetComponent<VehicleController>().allowMotion = false;
            boot2.transform.GetComponent<VehicleController>().allowMotion = false;
            animator.applyRootMotion = false;
            animator.Play("RubyJump");
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            PunkteText.text = (int.Parse(PunkteText.text) + points).ToString();
            jumped = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RubyJump"))
        {
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.normalizedTime > 0.99f)
            {
                animator.applyRootMotion = true;
                animator.Play("Idle");
            }
        }
    }
}
