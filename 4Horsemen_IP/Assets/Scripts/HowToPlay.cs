using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public GameObject Main;
    public GameObject htp;
    public GameObject Op;

    public void Trigger()
    {
        if(Main.activeInHierarchy == true)
            Main.SetActive(false);
            htp.SetActive(true);
    }

    public void OptionUI()
    {
        if(Main.activeInHierarchy == true)
            Main.SetActive(false);
            Op.SetActive(true);
    }
}
