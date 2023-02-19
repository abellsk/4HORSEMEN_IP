using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChSceneR1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.NextLevel();
    }
        
}
