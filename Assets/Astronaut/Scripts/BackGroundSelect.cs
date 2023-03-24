using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackGroundSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedBackGround;
    public Backgrounds[] Background;
    public Button unlockButton;
    public TextMeshProUGUI EnergyBalls;

    private void Awake()
    {
        
        selectedBackGround = PlayerPrefs.GetInt("SelectedBackGround", 0);
        foreach(GameObject Bg in skins)
            Bg.SetActive(false);

        skins[selectedBackGround].SetActive(true);

        foreach (Backgrounds b in Background)
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
        skins[selectedBackGround].SetActive(false);
        selectedBackGround++;
        if (selectedBackGround == skins.Length)
            selectedBackGround = 0;

        skins[selectedBackGround].SetActive(true);
        if (Background[selectedBackGround].isUnlocked)
          PlayerPrefs.SetInt("SelectedBackGround", selectedBackGround);

        UpdateUI();
    }

    public void ChangePrevious()
    {
        skins[selectedBackGround].SetActive(false);
        selectedBackGround--;
        if (selectedBackGround == -1)
            selectedBackGround =  skins.Length -1;

        skins[selectedBackGround].SetActive(true);
        if (Background[selectedBackGround].isUnlocked)
            PlayerPrefs.SetInt("SelectedBackGround", selectedBackGround);
        UpdateUI();
    }

    public void UpdateUI()
    {
        EnergyBalls.text = " " + PlayerPrefs.GetInt("Collected", 0);
        if (Background[selectedBackGround].isUnlocked == true)
            unlockButton.gameObject.SetActive(false);
        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text= " " + Background[selectedBackGround].price;
            if (PlayerPrefs.GetInt("Collected", 0) < Background[selectedBackGround].price)
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
        int price = Background[selectedBackGround].price;
        PlayerPrefs.SetInt("Collected", energyBalls - price);
        PlayerPrefs.SetInt(Background[selectedBackGround].name, 1);
        PlayerPrefs.SetInt("SelectedBackGround", selectedBackGround);
        Background[selectedBackGround].isUnlocked = true;

        // Update the displayed energy value
        EnergyBalls.text = " " + PlayerPrefs.GetInt("Collected", 0);

        UpdateUI();
    }
}