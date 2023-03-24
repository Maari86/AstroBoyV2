using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectEnergyballs : MonoBehaviour
{
    public static int Collected = 0;
    private TextMeshProUGUI EnergyBalls;

    void Start()
    {
        EnergyBalls = GetComponent<TextMeshProUGUI>();

        // Load the saved value of Collected from PlayerPrefs
        int savedCollected = PlayerPrefs.GetInt("Collected", 0);
        Collected = savedCollected;
    }

    void Update()
    {
        // Add the number of newly collected energy balls to Collected
        Collected += GetNewlyCollectedEnergyBalls();

        // Update the UI text with the new value of Collected
        EnergyBalls.text = " " + Collected;

        // Save the new value of Collected to PlayerPrefs
        PlayerPrefs.SetInt("Collected", Collected);
    }

    int GetNewlyCollectedEnergyBalls()
    {
        // This function should return the number of newly collected energy balls in the game.
     
        return 0;
    }
}
