using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//All the options for buing content.
//This generates the buttons you see at the bottom when you hit ESC in game.
//It handles what the buttons do, how much it costs to upgrade them, what upgrading effects, etc etc.
public struct Inf
{
    public int price;
    public int upgrade;
    public int level { get; set; }
    public Producer produce;
}
public class Purchases : MonoBehaviour
{
    public BalanceScript balance;
    public int Cheap = 1;
    private Health health;
    private PlayerController playerScript;
    public GameObject menu;
    private GameObject playerBody;
    public Dictionary<string, Inf> items;
    public Inventory invs;
    public Interact ints;
    public Interactable interactableScript;
    public Modifiers ms;

    /*
     * GameObject gun = Create.Gun( new List<string>(new string[]{"Enemy" }),autoFire: true, level: level);
        gun.transform.parent = playerBody.transform.parent;

        Interactable interactable = gun.AddComponent<Interactable>();
        interactable.moveable = true;

        Create.AttachSpawner(gun, playerBody,displacement: new Vector3(1, 0, 1));*/
    private void ZeroPrice(string name)
    {
        Inf item = items[name];
        item.price = 0;
        items[name] = item;
    }
    private void Start()
    {
        playerBody = GameObject.Find("PlayerBody");
        ints = playerBody.GetComponent<Interact>();
        health = gameObject.GetComponentInParent<Health>();
        playerScript = gameObject.GetComponent<PlayerController>();
        invs = GetComponent<Inventory>();
        ms = GetComponentInParent<Modifiers>();
        items = new Dictionary<string, Inf>()
        {
                   {"Gun", new Inf{price = 10, level = 1, upgrade = 1,
                produce = (level, from) => {
                     Create.AddLoadout("Gunny",playerBody,true);

                    ZeroPrice("Gun");
                }
    } },
                   {"Sniper", new Inf{price = 20, level = 1, upgrade = 1,
                produce = (level, from) => {
                     Create.AddLoadout("Snippy",playerBody,true);
                    ZeroPrice("Sniper");
                }
    } },
                                      {"SwordyPlus", new Inf{price = 5, level = 1, upgrade = 1,
                produce = (level, from) => {
                     Create.AddLoadout("SwordyPlus",playerBody,true);
                    ZeroPrice("SwordyPlus");
                }
    } },
                {"Charger", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => Create.Unit(from.transform.position + new Vector3(0f, 2f, 0f), "ChargerBody", "Player",level: level)
    } },
                {"Turret", new Inf{price = 5, level = 1, upgrade = 1,
                produce = (level,from) => { GameObject gunner = Create.Unit(gameObject.transform.position+ (gameObject.transform.forward * 6), "TurretBody",  "Player", "Gunny",level: 10);
                gunner.GetComponent<Modifiers>().reload = 5f;}
    } },
                {"Sniper Turret", new Inf{price = 5, level = 1, upgrade = 1,
                produce = (level,from) => { GameObject gunner = Create.Unit(gameObject.transform.position+ (gameObject.transform.forward * 6), "TurretBody",  "Player", "Snippy",level: 10);
                gunner.GetComponent<Modifiers>().reload = 1f;}
    } },
                {"DimLight", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => Create.ALight(gameObject.transform.position+ (gameObject.transform.forward * 6) + new Vector3(0f, 5f, 0f), color: Color.red, range: 10+(level*2), intensity:1+level,indirect:level/50)
    } },
                {"Light", new Inf{price = 5, level = 1, upgrade = 1,
                produce = (level,from) => Create.ALight(gameObject.transform.position+ (gameObject.transform.forward * 6) + new Vector3(0f, 8f, 0f), color: Color.white, range: 20+(level*2), intensity:3+level,indirect:level/50)
    } },
                {"Heal", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) =>  health.TakeDamage(-25 - (level *10))
    } },
                {"Repair", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) =>  from.GetComponentInParent<Health>().TakeDamage(-25 - (level *10))
    } },
                {"Backwards", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => playerScript.noBackwards = false
    } },

                {"Townhall", new Inf{price = 5, level = 1, upgrade = 1,
                produce = (level,from) => { Create.Unit(gameObject.transform.position + (gameObject.transform.forward * 6), "TownHall", "Player", level:15); }
    } } ,
                {"Wall", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => { GameObject wall = Create.Unit(gameObject.transform.position + (gameObject.transform.forward * 3), "Wall", "Player", level:15);
                                                        wall.transform.rotation = playerBody.transform.rotation; }
    } } ,
                        {"Contact Damage Upgrade", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => { ms.HitDamagePlus += 10; }
    } } ,
                          {"Scroll Range Upgrade", new Inf{price = 1, level = 1, upgrade = 1,
                produce = (level,from) => { playerScript.maxCamera += 2; }
    } } ,
            {"Sword", new Inf{price = 0, level = 1, upgrade = 1,
            produce = (level,from) =>{Create.AddLoadout("Swordy",playerBody,true);
                                 ZeroPrice("Sword");
            } }

        } };
        /*
        int count = 0;
        foreach (KeyValuePair<string, Inf> pair in items)
        {
            GameObject buttonObj = Instantiate(Create.GetPrefab("Button"), menu.transform);
            ClickableObject cs = buttonObj.AddComponent<ClickableObject>();
            Button button = buttonObj.GetComponent<Button>();
            Text txt = buttonObj.GetComponentInChildren<Text>();
            txt.text = "(" + pair.Value.upgrade + ")" + "lvl" + pair.Value.level + " " + pair.Key + "(" + pair.Value.price + ")";
            buttonObj.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rect = buttonObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(count * 160, 0);

            cs.leftClick = delegate { Buy(pair.Key); };
            cs.rightClick = delegate { Upgrade(pair.Key, txt); };
            count++;

        }*/
    }
    public void Upgrade(string name, Text txt) //level
    {

        Inf item = items[name];

        if (balance.balance >= item.upgrade) //&& cheap contains name
        {
            balance.AddMoney(-item.upgrade);
            item.level++;
            item.upgrade += item.upgrade;
            items[name] = item;
            print("upgraded to level " + item.level);
            txt.text = "(" + item.upgrade + ")" + "lvl" + item.level + " " + name + "(" + item.price + ")";
        }


    }
    public void Buy(string name, GameObject from, bool asItem = false, bool free = false) //level
    {
        Inf item = items[name];
        int price = item.price;
        if (free)
        {
            price = 0;
        }
        if (balance.balance >= price) //&& cheap contains name
        {
            balance.AddMoney(-price);
            if (asItem)
            {
                invs.Add(name);
            }
            else { 
                item.produce(item.level, from);
            }
        }
        ints.RefreshInteractables();
        from.GetComponent<Interactable>().RefreshInteractions();
    }

}
