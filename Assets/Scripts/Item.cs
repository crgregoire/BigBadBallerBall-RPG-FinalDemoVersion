using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

        public int count = 0;
        public string name;
    public Item(string name, int count = 1)
    {
        this.name = name;
        this.count = count;
    }
    
    
}
