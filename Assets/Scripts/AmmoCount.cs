using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammunitationtext;
    public Text magtext;

    public static AmmoCount occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void UpdateAmmoText(int presentammunation)
    {
        ammunitationtext.text = "Ammo-" + presentammunation;
    }

    public void updatemagtext(int mag)
    {
        magtext.text = "Magazines-" + mag;
    }
}
