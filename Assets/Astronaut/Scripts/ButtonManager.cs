using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject _instructionsPage; // reference to the instructions page game object
    [SerializeField] private GameObject _closeButton; // reference to the close button game object
    private bool _instructionsVisible = false; // flag to track if instructions page is visible
    [SerializeField] private GameObject IAPManager;
    private bool IAPManagerVisible = false;
    [SerializeField] private GameObject shopPage;
    private bool shopPageVisible = false;
    [SerializeField] private GameObject _mainPage;


    public void IWasClicked()
    {
        _source.PlayOneShot(_compressClip);
        SceneManager.LoadScene(1);
        StartCoroutine(PlayUncompressClipDelayed());
    }


    private IEnumerator PlayUncompressClipDelayed()
    {
        yield return new WaitForSeconds(_compressClip.length);
        _source.PlayOneShot(_uncompressClip);
    }

    public void InstructionsClicked()
    {
        // toggle the visibility of the instructions page
        _instructionsVisible = !_instructionsVisible;
        _instructionsPage.SetActive(_instructionsVisible);
        _source.PlayOneShot(_compressClip);
    }

    public void shopPageClicked()
    {
        // toggle the visibility of the instructions page
        shopPageVisible = !shopPageVisible;
        shopPage.SetActive(shopPage);
        _source.PlayOneShot(_compressClip);
    }

    public void IAPManagerClicked()
    {

        IAPManagerVisible = !IAPManagerVisible;
        IAPManager.SetActive(IAPManagerVisible);
        _source.PlayOneShot(_compressClip);
    }

    public void MainClicked()
    {
        _mainPage.SetActive(false);
    }
    public void CloseClicked()
    {
        // disable the instructions page and reset the flag
        _instructionsVisible = false;
        _instructionsPage.SetActive(false);
        _source.PlayOneShot(_compressClip);
        shopPageVisible = false;
        shopPage.SetActive(false);
        _mainPage.SetActive(true);
        IAPManagerVisible = false;
        IAPManager.SetActive(false);

    }
}
