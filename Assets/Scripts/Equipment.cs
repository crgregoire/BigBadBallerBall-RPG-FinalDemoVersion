using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Equipment : MonoBehaviour {
    public GameObject current;
    public int availableCount = 0;
    public int currentNum = 0;
    //This is my hacky dynamic array... Very sad.
    public Dictionary<int, string> availableOrder = new Dictionary<int, string>();
    public Dictionary<string,GameObject> available = new Dictionary<string,GameObject>();
    public void NewAvailable(string name, GameObject go)
    {
        if (!available.ContainsKey(name)) {
            availableOrder[availableCount] = name;
            availableCount++;
            available.Add(name, go);
        }
        else
        {
            print("already have");
        }
    }
    public void Next()
    {
        if (availableCount > 0)
        {
            ++currentNum;
            currentNum %= availableCount;
            Create.EquipLoadout(availableOrder[currentNum], gameObject);
        }
    }
}
