using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//This class is currently under developement.
//Once you select an object to interact with this generates its possible interactions.

public class Interactable : MonoBehaviour {

    private GameObject interactionGrid;
    private GameObject interactable;
    public List<string> buyables;
    private Purchases ps;
    public bool moveable = false;
    private Inventory invs;
    private GameObject gs;
    private Interact ints;
    public ScrollRect list;
    void Start()
    {
        interactable = gameObject;
        gs = GameObject.Find("GlobalScripts");
        interactionGrid = gs.GetComponent<MenuScript>().interactionGrid;
        GameObject player = gs.GetComponent<MenuScript>().Player;
        ints = player.GetComponent<Interact>();
        ps =player.GetComponent<Purchases>();
        invs = gameObject.GetComponent<Inventory>();

    }
    public void ReInteractables() {

        ints.RefreshInteractables();
    }

    public void OnDeath()
    {
        Invoke("ReInteractables", 0.1f);
        CloseInteractions();
    }
    public void RefreshInteractions()
    {
        CloseInteractions();
        GetInteractions();
    }
    public static void CloseInteractions()
    {
        var children = new List<GameObject>();
        foreach (Transform child in GameObject.Find("GlobalScripts").GetComponent<MenuScript>().interactionGrid.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

    }
    public void GetInteractions()
    {

        foreach (string it in buyables)
        {
            string item;
            bool asItem = false;
            if (it.Length > 4 && it.Substring(it.Length - 4) == "Item")
            {
                item =it.Substring(0, it.Length-4);
                asItem = true;
            }
            else
            {
                item = it;
            }
            Inf itemInfo = ps.items[item];
            GameObject buttonObj = Instantiate(Create.GetPrefab("Grid Button"), interactionGrid.transform);
            ClickableObject cs = buttonObj.AddComponent<ClickableObject>();
            Button button = buttonObj.GetComponent<Button>();
            Text txt = buttonObj.GetComponentInChildren<Text>();
            txt.text = item + "(" + itemInfo.price + ")";
            cs.leftClick = delegate { ps.Buy(item,gameObject,asItem:asItem); };
        }
        
        ImpactDamage ids = interactable.GetComponent<ImpactDamage>();
        if (ids)
        {
            GameObject buttonObj = Instantiate(Create.GetPrefab("Grid Button"), interactionGrid.transform);
            ClickableObject cs = buttonObj.AddComponent<ClickableObject>();
            Button button = buttonObj.GetComponent<Button>();
            Text txt = buttonObj.GetComponentInChildren<Text>();
            txt.text = "Increase Damage";
            cs.leftClick = () => ids.impactDamage += 1;
            //cs.rightClick = delegate { Upgrade(pair.Key, txt); };
        }
        if(interactable.name == "townhall")
        {

        }
        if (invs)
        {
            foreach (KeyValuePair<string, int> item in invs.items)
            {
                GameObject buttonObj = Instantiate(Create.GetPrefab("Grid Button"), interactionGrid.transform);
                ClickableObject cs = buttonObj.AddComponent<ClickableObject>();
                Button button = buttonObj.GetComponent<Button>();
                Text txt = buttonObj.GetComponentInChildren<Text>();
                txt.text = "(" + item.Value + ")" + item.Key;
                cs.leftClick = delegate { invs.Use(item.Key); RefreshInteractions(); };
            }
        }
    }
}
    



