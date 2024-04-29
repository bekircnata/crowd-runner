using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header( "Elements" )]
    [SerializeField] private Button thisButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;
    [SerializeField] private Transform runnersParent;
    [SerializeField] private Transform runnersPrefab;


    private bool unlocked;

    public void Configure(Material skinSprite, bool unlocked) 
    {
        skinImage.material = skinSprite;
        this.unlocked = unlocked;

        if(unlocked) {
            Unlock();
        } else {
            Lock();
        }
    }

    public void Unlock()
    {
        thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        unlocked = true;
    }

    private void Lock()
    {
        thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void Select()
    {
        selector.SetActive(true);
        
        for (int i = 0; i < runnersParent.childCount; i++) {
            runnersParent.GetChild(i).GetChild(0).gameObject.GetComponent<Renderer>().material = skinImage.material;
            runnersPrefab.GetChild(0).gameObject.GetComponent<Renderer>().material = skinImage.material;
        }
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public Button GetButton()
    {
        return thisButton;
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }
    
}
