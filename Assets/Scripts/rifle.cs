using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rifle : MonoBehaviour
{
    [Header("Rifle stuff")]
    public Camera cam;
    public float damage=10f;
    public float shootrange = 100f;
    public float firerate = 15f;
    public Animator animator;
    public playerscript player;

    [Header("rifle ammo and shooting")]
    private int maxammo = 20;
    private int mag = 15;
    private int presentammo;
    public float reloadtime = 1.3f;
    private bool setreloading=false;
    private float nexttimetoshoot = 0f;

    [Header("rifle effects")]
    public ParticleSystem muzzleflash;
    public GameObject impactefx;
    public GameObject gorefx;

    [Header("Sounds and UI")]
    [SerializeField] private GameObject ammooutui;
    [SerializeField] private int timetoshowui = 1;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        presentammo = maxammo;
    }
    // Update is called once per frame
    void Update()
    {
        if (setreloading)
            return;

        if(presentammo<=0)
        {
            StartCoroutine(reload());
            return;
        }
        if (Input.GetButton("Fire1")&& Time.time>=nexttimetoshoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nexttimetoshoot = Time.time + 1f/firerate;
            shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reload", false);
        }
        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reload", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
            animator.SetBool("Reload", false);
        }
    }

     void shoot()
    {
        if(mag ==0)
        {
            StartCoroutine(showammoout());
            return;
        }

        presentammo--;

        if(presentammo ==0)
        {
            mag--;
        }

        AmmoCount.occurrence.UpdateAmmoText(presentammo);
        AmmoCount.occurrence.updatemagtext(mag);

        muzzleflash.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hinfo, shootrange))
        {
            Debug.Log(hinfo.transform.name);

            objdmg objdmg = hinfo.transform.GetComponent<objdmg>();
            enemy enemy = hinfo.transform.GetComponent<enemy>();

            if (objdmg != null)
            {
                objdmg.objhitdmg(damage);

                GameObject impactgo = Instantiate(impactefx, hinfo.point, Quaternion.LookRotation(hinfo.normal));
                Destroy(impactgo, 5f);
            }

            else if (enemy != null)
            {
                enemy.enemyhitdmg(damage);
                GameObject impactgo = Instantiate(gorefx, hinfo.point, Quaternion.LookRotation(hinfo.normal));
                Destroy(impactgo, 2f);
            }
        }
        
    }

    IEnumerator reload()
    {
       // player.playerspeed = 0f;
       // player.sprint = 0f;
        setreloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reload", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadtime);
        animator.SetBool("Reload", false);
        presentammo = maxammo;
        player.playerspeed = 1.9f;
        player.sprint = 3;
        setreloading = false;
    }
    IEnumerator showammoout()
    {
        ammooutui.SetActive(true);
        yield return new WaitForSeconds(timetoshowui);
        ammooutui.SetActive(false);
    }
}
