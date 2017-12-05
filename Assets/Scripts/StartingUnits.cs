using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//This class contains any content that is made from the Create class at the start of the game.
public class StartingUnits : MonoBehaviour {
    public Text playerText;
	// Use this for initialization
	void Start () {
        //Create nonStatic = GameObject.Find("GlobalScripts").GetComponent<Create>();
        //Create.Gunner(new Vector3(0f, 0.5f, -14f));

        Create.ALight(gameObject.transform.position + new Vector3(0f, 5f, 3f), color: Color.white, intensity: 3, range: 15);
        //nonStatic.Sound("boop", altLocation: light, times: 5, volume: 0.1f);
        StartCoroutine(PlayerMessage(" ",10f));
        /*
        Create.Gunner(new Vector3(0f, .5f, -140f), "Player", new string[] { "Enemy" }, 30, targetingRange: 1000, ai: "kite");
        Create.Gunner(new Vector3(0f, .5f, -130f), "Player", new string[] { "Enemy" }, 30, targetingRange: 1000, ai: "kite");
        Create.Gunner(new Vector3(0f, .5f, -120f), "Player", new string[] { "Enemy" }, 30, targetingRange: 1000, ai: "kite");
        Create.Gunner(new Vector3(0f, .5f, -110f), "Player", new string[] { "Enemy" }, 30, targetingRange: 1000, ai: "kite");
        Create.Gunner(new Vector3(0f, .5f, -101f), "Player", new string[] { "Enemy" }, 30, targetingRange: 1000, ai: "kite" );
        */
    }
    


    IEnumerator PlayerMessage(string message, float delayTime )
    {

        yield return new WaitForSeconds(delayTime);
        playerText.text = message;


    }

}
