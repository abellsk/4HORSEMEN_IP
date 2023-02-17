using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketChecker : MonoBehaviour
{
    public GameObject theToken;
    public GameObject theKey;
    //public GameObject tokenTwo;
    //public GameObject tokenThree;
    //public GameObject tokenFour;
    //public GameObject tokenFive;
    //public GameObject tokenSix;
    //public GameObject tokenSeven;
    //public GameObject tokenEight;
    //public GameObject tokenNine;

    //public List<GameObject> tokens;

    private int numberAnsCorrect = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberAnsCorrect >= 9)
        {
            theKey.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (theToken == collider.gameObject)
        {
            numberAnsCorrect++;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (theToken == collider.gameObject)
        {
            numberAnsCorrect--;
        }
    }
}
