using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Something that happens every iteration. For things that need a trigger, but they just always are hitting that trigger.
public class Always : MonoBehaviour {
        public Actor action;
        public void  SetAlways(Actor doWhat)
        {//could put these out into functions if desired.
            action = doWhat;
        }
    private void Update()
    {
        action();
    }
}
