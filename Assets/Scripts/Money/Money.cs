using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Coins check if player is colliding with them. If he is, they disapear and add to his balance.
//This is the script to handle that behaviour.
public class Money : MonoBehaviour {

    public int worth = 1;
    public BalanceScript balanceScript;
    void Start()
    {
        if (!balanceScript)
        {
            balanceScript = GameObject.Find("PlayerBalance").GetComponent<BalanceScript>();
        }
    }

void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            balanceScript.AddMoney(worth);
            Destroy(gameObject);
        }
    }
}
