using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BulletTarget : MonoBehaviour
{
    public GameObject targetShot;
    public GameObject targetShot2;
    public static GameManager instance;
    //bool allDead = false;


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Target has been hit");
            Destroy(collision.gameObject);
            GameManager.instance.shootScore();
        }
        if (collision.gameObject.tag == "Target1")
        {
            Destroy(collision.gameObject);
            Debug.Log("Target 1 has been hit");
            GameManager.instance.shootScore();
        }
        if (collision.gameObject.tag == "Target2")
        {
            Destroy(collision.gameObject);
            Debug.Log("Target 2 has been hit");
            GameManager.instance.shootScore();
        }
    }
}
