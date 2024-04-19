using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class objdmg : MonoBehaviour
{

    public float objhealth = 100f;

    public void objhitdmg(float amount)
    {
        objhealth -= amount;
        if(objhealth <= 0f )
        {
            dead();
        }
    }
    void dead()
    {
        Destroy(gameObject);
    }
}
