using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketChecker : MonoBehaviour
{
    public GameObject theToken;
    public GameObject theKey;
    private bool isTriggered = false;
    //public GameObject tokenTwo;
    //public GameObject tokenThree;
    //public GameObject tokenFour;
    //public GameObject tokenFive;
    //public GameObject tokenSix;
    //public GameObject tokenSeven;
    //public GameObject tokenEight;
    //public GameObject tokenNine;

    //public List<GameObject> tokens;

    public int numberAnsCorrect = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreAdder();
        if (numberAnsCorrect < 9)
        {
            theKey.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider collider)
    {  
        if (theToken == theToken)
        {
            isTriggered = true;
            Debug.Log("Token is at the correct place");
        }
        else
        {
            Debug.Log("Token salah");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        //hrllo
    }

    void scoreAdder()
    {
        if (isTriggered)
        {
            numberAnsCorrect++;
            isTriggered = false;
            Debug.Log(numberAnsCorrect);
        }
    }

}
