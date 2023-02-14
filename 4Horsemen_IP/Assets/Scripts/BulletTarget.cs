using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public GameObject targetShot;
    int targetDown = 0;
    bool allDead = false;

    void onCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Target1")
        {
            Debug.Log("Target has been hit");
            targetDown++;
        }
        if (collision.gameObject.tag == "Target2")
        {
            Debug.Log("Target has been hit");
            targetDown++;
        }
        if (collision.gameObject.tag == "Target3")
        {
            Debug.Log("Target has been hit");
            targetDown++;
        }
    }

    void checkTargets()
    {
        if (targetDown >= 3)
        {
            allDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
