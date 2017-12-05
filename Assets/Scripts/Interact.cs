using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This class gets objects within a radius that are interactable. It is run when you open the menu. Or any time refresh interactables is called.
public class Interact : MonoBehaviour {
    public int  radius = 100000;
    public bool open = false;
    public GameObject grid;

    public void RefreshInteractables()
    {
        CloseInteractables();
        GetInteractables();
    }
    public void CloseInteractables()
    {
        var children = new List<GameObject>();
        foreach (Transform child in grid.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

    }
    public void GetInteractables()
    {
        open = true;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Interactable item = hit.GetComponent<Interactable>();
            if (item)
            {
                GameObject buttonObj = Instantiate(Create.GetPrefab("Grid Button"), grid.transform);
                ClickableObject cs = buttonObj.AddComponent<ClickableObject>();
                Button button = buttonObj.GetComponent<Button>();
                Text txt = buttonObj.GetComponentInChildren<Text>();
                if (hit.name == "PlayerBody")
                {
                    txt.text = "Inventory";
                }
                else
                {
                    txt.text = hit.name;
                }
                
                cs.leftClick = () => { Interactable.CloseInteractions(); item.GetInteractions(); };
                //cs.rightClick = delegate { Upgrade(pair.Key, txt); };
            }
        }
    }

}
