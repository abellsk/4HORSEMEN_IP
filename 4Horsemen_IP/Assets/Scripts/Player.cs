using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// movement variables
    /// </summary>
    Vector3 vectorToMove = new Vector3(2.5f, 1f, 2f);
    Vector3 movementInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    Vector3 headRotationInput;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f; 
    float interactionDistance = 10f;

    /// <summary>
    /// Default values
    /// </summary>
    bool interact = false;
    bool GunEquip = false;

    /// <summary>
    /// Camera for player.
    /// </summary>
    public GameObject playerCamera;


    // Update is called once per frame
    void Update()
    {
        // Create a new Vector3
        Vector3 movementVector = Vector3.zero;
      
        // Add the forward direction of the player multiplied by the user's up/down input.
        movementVector += transform.forward * movementInput.y;

        // Add the right direction of the player multiplied by the user's right/left input.
        movementVector += transform.right * movementInput.x;
        
        // Apply the movement vector multiplied by movement speed to the player's position.
        transform.position += movementVector * moveSpeed * Time.deltaTime;

            // Apply the rotation multiplied by the rotation speed.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed * Time.deltaTime);
            playerCamera.transform.rotation = Quaternion.Euler(playerCamera.transform.rotation.eulerAngles + headRotationInput * rotationSpeed * Time.deltaTime);

            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (playerCamera.transform.forward * interactionDistance));
            RaycastHit hitInfo;
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionDistance))
            {
                if(hitInfo.transform.tag == "Gun")
                {
                    if(interact)
                    {
                        GunEquip = true;
                        Debug.Log(GunEquip);
                        Destroy(hitInfo.transform.gameObject);
                    }
                }
            }
        
        
    }

    /// <summary>
    ///To Move
    /// </summary>
    void OnMove(InputValue value)   
    {
        movementInput = value.Get<Vector2>();
    }

    /// <summary>
    /// For player to be able to look around
    /// </summary>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        headRotationInput.x = value.Get<Vector2>().y * -1;
    }

    /// <summary>
    /// For Left Click Function
    /// </summary>
    void OnFire()
    {
        interact = true;
    }
}
