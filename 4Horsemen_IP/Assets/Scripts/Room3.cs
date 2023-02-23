using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Room3 : MonoBehaviour
{

    private int tokenTracker;
    public GameObject chestKey;
    public GameObject Fire;
    public GameObject dimLight;

    public Animator chestOpen;


    public void TokenPlaced()
    {
        tokenTracker++;
        if (tokenTracker >= 9)
        {
            Fire.SetActive(false);
            dimLight.SetActive(true);
            chestKey.SetActive(true);
        }
    }

    public void KeyPlaced()
    {
        chestOpen.Play("ChestOpen");
    }


}
