using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {
    public Text moneyText = null;
    public int money = 250;

	void Start () {
        UpdateMoney();
    }
	
	void Update () {
		
	}

    public void ChangeMoney(int amount)
    {
        money += amount;
        UpdateMoney();
    }
    private void UpdateMoney()
    {
        moneyText.text = "$" + money.ToString();
    }
}
