﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    //Create a reference to the CoinPoofPrefab
    public GameObject CoinPoof;

    public void OnCoinClicked() {
        // Instantiate the CoinPoof Prefab where this coin is located
        // Make sure the poof animates vertically
        Instantiate(CoinPoof, transform.position, Quaternion.Euler(-90,0,0));
        // Destroy this coin. Check the Unity documentation on how to use Destroy
        Destroy(gameObject); //at some point I change to 'CoinPoof' instead of 'gameObject too'
    }

}