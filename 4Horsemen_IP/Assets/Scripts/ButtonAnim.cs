using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{

    public void PlayAnimation()
    {
        GetComponent<Animator>().Play("ButtonPressed");
    }
}
