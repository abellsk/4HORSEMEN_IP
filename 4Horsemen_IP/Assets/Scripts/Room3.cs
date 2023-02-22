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

    public Animator chestOpen;


    public void TokenPlaced()
    {
        tokenTracker++;
        if (tokenTracker >= 9)
        {
            Fire.SetActive(false);
            chestKey.SetActive(true);
        }
    }

    public void KeyPlaced()
    {
        chestOpen.Play("ChestOpen");
    }

}
