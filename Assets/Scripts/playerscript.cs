using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerspeed = 1.9f;
    public float sprint = 3f;

    [Header("Player health")]
    private float playerhealth = 120f;
    private float presentheath;
    public healthbar healthBar;
 //   public AudioClip playerHurtSound;
    public AudioSource audioSource;

    [Header("Palyer scripts cameras")]
    public Transform playercamera;
    public GameObject deathcamera;
    public GameObject EndGameMenuUI;

    [Header("Player Animator and Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player jump and velocity")]
    public float jumpheight = 1f;
    Vector3 velocity;
    public float turncaltime = 0.1f;
    float turncalvelocity;
    public Transform groundcheck;
    bool onsurface;
    public float groundistance =0.4f;
    public LayerMask groundmask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentheath = playerhealth;
        healthBar.givefullhealth(playerhealth);
    }

    // Update is called once per frame
    void Update()
    {
        onsurface = Physics.CheckSphere(groundcheck.position,groundistance,groundmask);

        if(onsurface && velocity.y<0 )
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity*Time.deltaTime);

        playermove();
        jump();
        Sprint();
    }
    void playermove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction =new Vector3 (horizontal_axis,0f, vertical_axis).normalized;

        if(direction.magnitude>=0.1f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            animator.SetBool("AimWalk", false);
            animator.SetBool("IdleAim", false);

            float targetangel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playercamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangel, ref turncalvelocity, turncaltime);
            transform.rotation = Quaternion.Euler(0f, targetangel, 0f);

            Vector3 movedirection = Quaternion.Euler(0f, targetangel, 0f) * Vector3.forward;
            cC.Move (movedirection.normalized*playerspeed*Time.deltaTime);

        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetTrigger("Jump");
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("AimWalk", false);
        }
    }
    void jump()
    {
        if(Input.GetButtonDown("Jump") && onsurface)
        {
            animator.SetBool("Walk", false);
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpheight * -2 * gravity);
        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }

    void Sprint()
    {
        if (Input.GetButton("Sprint")&& Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)&& onsurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("IdleAim", false);
                float targetangel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playercamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangel, ref turncalvelocity, turncaltime);
                transform.rotation = Quaternion.Euler(1f, targetangel, 1f);

                Vector3 movedirection = Quaternion.Euler(0f, targetangel, 0f) * Vector3.forward;
                cC.Move(movedirection.normalized * sprint * Time.deltaTime);

            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
            }
        }
    }
    public void playerhitdmg(float takedmg)
    {
        presentheath -= takedmg;
        healthBar.sethealth(presentheath);
       // audioSource.PlayOneShot(playerHurtSound);
       
        if(presentheath <= 0)
        {
            playerdie();
        }
    }
    private void playerdie()
    {
        EndGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        deathcamera.SetActive(true);
        Object.Destroy(gameObject,1.0f);
    }
}
