using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletOrigin;
    public GameObject bulletPrefab;
    public GameObject targetShot;


    public void Fire()
    {
        Debug.Log("hello");
        // Spawn a new bullet at the position of bulletOrgin
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);

        newBullet.GetComponent<Rigidbody>().AddForce(bulletOrigin.forward * 1500f);
    } 

}
