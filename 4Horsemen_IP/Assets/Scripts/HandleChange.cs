using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleChange : MonoBehaviour
{

    public GameObject Handle;

    private void OnTriggerEnter(Collider other)
    {
        if(Handle)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
