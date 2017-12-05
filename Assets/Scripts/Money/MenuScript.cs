using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//This is a class that simply controls menu toggling.
public class MenuScript : MonoBehaviour {
    public GameObject Menu;
    public GameObject Player;
    public NetworkManagerHUD NetworkHUD;
    public Interact playerInteract;
    public GameObject interactionGrid;
    public GameObject playerRotation;
  
    public bool haveHud = false;

    // Use this for initialization
    public void Start()
    {
        playerInteract = Player.GetComponent<Interact>();
    NetworkHUD = GameObject.Find("Networking").GetComponent<NetworkManagerHUD>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    //BUTTONS ARE IN PURCHASES
    public void Toggle() {
           
        if (Menu.activeInHierarchy)
        {
            AudioListener.pause = false;
            NetworkHUD.enabled = false;
            Time.timeScale = 1;
            Menu.SetActive(false);
            Player.GetComponent<PlayerController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else{
            if (haveHud)
            {
                NetworkHUD.enabled = true;
            }
            /*
            AudioListener.pause = true;
            Time.timeScale = 0;
            */
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Player.GetComponent<PlayerController>().enabled = false;
            Menu.SetActive(true);
            Interactable.CloseInteractions();
            playerInteract.RefreshInteractables();
        }
    }


}
