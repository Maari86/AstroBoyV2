using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedPlayer;
    public Backgrounds[] Player;
    public Button unlockButton;
    public TextMeshProUGUI EnergyBalls;

    private void Awake()
    {

        selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[selectedPlayer].SetActive(true);

        foreach (Backgrounds b in Player)
        {
            if (b.price == 0)
                b.isUnlocked = true;
            else
            {
                b.isUnlocked = PlayerPrefs.GetInt(b.name, 0) == 0 ? false : true;
            }
        }
    }

    public void ChangeNext()
    {
        skins[selectedPlayer].SetActive(false);
        selectedPlayer++;
        if (selectedPlayer == skins.Length)
            selectedPlayer = 0;

        skins[selectedPlayer].SetActive(true);
        if (Player[selectedPlayer].isUnlocked)
            PlayerPrefs.SetInt("SelectedPlayer", selectedPlayer);

        UpdateUI();
    }

    public void ChangePrevious()
    {
        skins[selectedPlayer].SetActive(false);
        selectedPlayer--;
        if (selectedPlayer == -1)
            selectedPlayer = skins.Length - 1;

        skins[selectedPlayer].SetActive(true);
        if (Player[selectedPlayer].isUnlocked)
            PlayerPrefs.SetInt("SelectedPlayer", selectedPlayer);
        UpdateUI();
    }

    public void UpdateUI()
    {
        EnergyBalls.text = " " + PlayerPrefs.GetInt("Collected", 0);
        if (Player[selectedPlayer].isUnlocked == true)
            unlockButton.gameObject.SetActive(false);
        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = " " + Player[selectedPlayer].price;
            if (PlayerPrefs.GetInt("Collected", 0) < Player[selectedPlayer].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false;
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
            }
        }
    }
    public void Unlock()
    {
        int energyBalls = PlayerPrefs.GetInt("Collected", 0);
        int price = Player[selectedPlayer].price;
        PlayerPrefs.SetInt("Collected", energyBalls - price);
        PlayerPrefs.SetInt(Player[selectedPlayer].name, 1);
        PlayerPrefs.SetInt("SelectedPlayer", selectedPlayer);
        Player[selectedPlayer].isUnlocked = true;

        // Update the displayed energy value
        EnergyBalls.text = " " + PlayerPrefs.GetInt("Collected", 0);

        UpdateUI();
    }
}