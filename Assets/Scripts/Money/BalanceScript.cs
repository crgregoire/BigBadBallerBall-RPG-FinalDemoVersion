using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceScript : MonoBehaviour {
    public int balance;
    private Text bal;
	// Use this for initialization
    void Start()
    {
        bal = GetComponent<Text>();
        bal.text = "BALANCE: " + balance.ToString();
    }
	public void AddMoney (int amount) {
        balance += amount;
        bal.text = "BALANCE: " + balance.ToString();
	}

}
