using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
    public GameObject KeyPoof;
    public Door doorOpen;
    public static bool gotKey;

    private void Start()
    {
        gotKey = false;
    }

    void Update()
	{
		//Not required, but for fun why not try adding a Key Floating Animation here :)
	}

	public void OnKeyClicked()
	{
        // Instatiate the KeyPoof Prefab where this key is located
        // Make sure the poof animates vertically
        Instantiate(KeyPoof, transform.position, Quaternion.Euler(-150, 0, 0));
        // Call the Unlock() method on the Door
        // Set the Key Collected Variable to true
        gotKey = true;
        doorOpen.Unlock();
        // Destroy the key. Check the Unity documentation on how to use Destroy
        Destroy(gameObject);
    }

}
