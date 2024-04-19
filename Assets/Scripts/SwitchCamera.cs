using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera assign")]
    public GameObject aimcam;
    public GameObject aimcanvas;
    public GameObject tpscam;
    public GameObject tpscanvas;

    [Header("camera animator")]
    public Animator animator;
    void Update()
    {
        
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", true);
            animator.SetBool("Walk", true);

            tpscam.SetActive(false);
            tpscanvas.SetActive(false);
            aimcam.SetActive(true);
            aimcanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", false);
            animator.SetBool("Walk", false);

            tpscam.SetActive(false);
            tpscanvas.SetActive(false);
            aimcam.SetActive(true);
            aimcanvas.SetActive(true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("AimWalk", false);

            tpscam.SetActive(true);
            tpscanvas.SetActive(true);
            aimcam.SetActive(false);
            aimcanvas.SetActive(false);
        }
    }
}