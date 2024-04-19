using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngineInternal;

public class enemy : MonoBehaviour
{
    [Header("Enemy health and damage")]
    private float enemyhealth = 120f;
    private float presenthealth;
    public float givedmg = 5;
    public healthbar healthBar;

    [Header("Enemy things")]
    public NavMeshAgent enemyagent;
    public Transform lookpoint;
    public Camera shootingraycastarea;
    public Transform playerbody;
    public LayerMask playerlayer;

    [Header("Enemy guarding var")]
    public GameObject[] walkpoints;
    int currentenemyposition = 0;
    public float enemyspeed;
    float walkingpointradius = 2;

    [Header("Sounds and UI")]
    public AudioClip shootingSound;
    public AudioSource audioSource;

    [Header("Enemy shooting var")]
    public float timebtwshoot;
    bool prerviouslyshoot;

    [Header("Enemy animation and muzzle")]
    public Animator anim;
    public ParticleSystem muzzlespark;

    [Header("Enemy situation")]
    public float visionradius;
    public float shootingradius;
    public bool playerinvisionradius;
    public bool playerinshootingradius;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        presenthealth = enemyhealth;
        healthBar.givefullhealth(enemyhealth);
        playerbody = GameObject.Find("Player").transform;
        enemyagent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerinvisionradius = Physics.CheckSphere(transform.position,visionradius, playerlayer);
        playerinshootingradius= Physics.CheckSphere(transform.position,shootingradius, playerlayer);

        if (!playerinvisionradius && !playerinshootingradius) guard();
        if (playerinvisionradius && !playerinshootingradius) pursueplayer();
        if (playerinvisionradius && playerinshootingradius) shootplayer();

    }
    private void guard()
    {
        if (Vector3.Distance(walkpoints[currentenemyposition].transform.position, transform.position) < walkingpointradius)
        {
            currentenemyposition= Random.Range(0,walkpoints.Length);
            if(currentenemyposition >= walkpoints.Length)
            {
                currentenemyposition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkpoints[currentenemyposition].transform.position,Time.deltaTime*enemyspeed);

        transform.LookAt(walkpoints[currentenemyposition].transform.position);
    }
    private void pursueplayer()
    {
       if(enemyagent.SetDestination(playerbody.position))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", true);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", false);


            visionradius = 30f;
            shootingradius = 16f;
        }
       else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", true);
        }
    }
    private void shootplayer()
    {
        enemyagent.SetDestination(transform.position);

        transform.LookAt(lookpoint);

        if(!prerviouslyshoot)
        {
            muzzlespark.Play();
            audioSource.PlayOneShot(shootingSound);

            RaycastHit hit;
            if(Physics.Raycast(shootingraycastarea.transform.position,shootingraycastarea.transform.forward,out hit,shootingradius))
            {
                Debug.Log("Shooting" + hit.transform.name);

                playerscript playerbody = hit.transform.GetComponent<playerscript>();

                if (playerbody != null)
                {
                    playerbody.playerhitdmg(givedmg);
                }
                anim.SetBool("Shoot", true);
                anim.SetBool("Walk", false);
                anim.SetBool("AimRun", false);
                anim.SetBool("Die", false);
            }
            prerviouslyshoot = true;
            Invoke(nameof(activeshooting), timebtwshoot);
        }
    }
    private void activeshooting()
    {
        prerviouslyshoot = false;
    }
    public void enemyhitdmg(float takedmg)
    {
        presenthealth-=takedmg;
        healthBar.sethealth(presenthealth);

        visionradius = 40f;
        shootingradius = 19f;

        if (presenthealth <= 0)
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Die", true); ;
            enemydie();
        }
    }
    private void enemydie()
    {
        enemyagent.SetDestination(transform.position);
        enemyspeed = 0f;
        shootingradius = 0f;
        visionradius = 0f;
        playerinshootingradius = false;
        playerinshootingradius = false; 
        Object.Destroy(gameObject,0f);
    }
}
