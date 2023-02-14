using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public GameObject targetShot;
    int targetDown = 0;
    bool allDead = false;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Target has been hit");
            targetDown++;
        }
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
        if (collision.gameObject.tag == "playarea")
        {
            Debug.Log("Target has hit the floor");
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
        //Debug.Log("Target has hit the floor");
    }
}
