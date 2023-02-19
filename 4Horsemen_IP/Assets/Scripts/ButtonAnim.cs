using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{

    public static ButtonAnim instance;

    public void PlayAnimation()
    {
        GetComponent<Animator>().Play("ButtonPressed");
    }
}
