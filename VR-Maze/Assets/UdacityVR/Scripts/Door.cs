using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked()
    // Create a boolean value called "opening" that can be checked in Update()
    public bool locked;
    public bool opening;
    public AudioSource doorSound;
    public AudioClip openGate;
    public AudioClip closeGate;
    public GameObject leftDoor;
    public GameObject rightDoor;

    void Update()
    {
        // If the door is opening and it is not fully raised
        // Animate the door raising up
        if (opening == true) {
            //leftDoor.transform.Rotate(0f, 0f, 45f);
            //rightDoor.transform.Rotate(0f, 0f, -45f);
            if (transform.position.y <= 20.5f) {
                transform.Translate(0, 30 * Time.deltaTime, 0, Space.World);
            }
        }
    }

    public void OnDoorClicked()
    {
        // If the door is clicked and unlocked
        // Set the "opening" boolean to true
        if (Key.gotKey == true)
        {
            //leftDoor.transform.Rotate(0f, 0f, 80f);
            //rightDoor.transform.Rotate(0f, 0f, -80f);
            opening = true;
            doorSound.PlayOneShot(openGate);

            //disabling collider after opening door
            GetComponent<Collider>().enabled = false;
        }
        // (optionally) Else
        // Play a sound to indicate the door is locked
    }

    public void Unlock()
    {
        // You'll need to set "locked" to false here
        locked = true;
    }
}
