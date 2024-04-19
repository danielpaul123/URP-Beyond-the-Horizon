using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            objective1.text = "1. Key Acquired";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "1. Find key to open the gate";
            objective1.color = Color.white;
        }

        if(obj2 == true)
        {
            objective2.text = "2. Computer offline";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "2. Shutdown the computer system";
            objective2.color = Color.white;
        }

        if(obj3 == true)
        {
            objective3.text = "3. Generators offline";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "3. Shutdown both of the generators";
            objective3.color = Color.white;
        }

        if(obj4 == true)
        {
            objective4.text = "4. Mission Completed";
            objective4.color = Color.green;
        }
        else
        {
            objective4.text = "4. Find vehicle and escape from the facility";
            objective4.color = Color.white;
        }
    }
}
