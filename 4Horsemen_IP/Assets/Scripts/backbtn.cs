using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backbtn : MonoBehaviour
{
    public GameObject Main;
    public GameObject htp;
    public GameObject Op;

    public void Trigger()
    {
        if(htp.activeInHierarchy == true)
            htp.SetActive(false);
            Main.SetActive(true);
    }

    public void Option()
    {
        if(Op.activeInHierarchy == true)
            Op.SetActive(false);
            Main.SetActive(true);
    }
}
