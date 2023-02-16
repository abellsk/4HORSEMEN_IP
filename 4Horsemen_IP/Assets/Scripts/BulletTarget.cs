using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletTarget : MonoBehaviour
{
    public GameObject targetShot;
    public GameObject targetShot2;
    int targetDown = 0;
    bool allDead = false;
    public TextMeshProUGUI playerScoreText1; //show current score in screen 1
    public TextMeshProUGUI playerScoreText2; //show current score in screen 2
    public TextMeshProUGUI playerScoreText3; //show current score in screen 3
    public TextMeshProUGUI playerScoreText4; //show current score in screen 4

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Target has been hit");
            Destroy(GameObject.FindWithTag("Target"));
            targetDown++;
            playerScoreText1.text = targetDown.ToString();
            playerScoreText2.text = targetDown.ToString();
            playerScoreText3.text = targetDown.ToString();
            playerScoreText4.text = targetDown.ToString();
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
