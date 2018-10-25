using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {
    public Text moneyText = null;
    public Text ElectricityText = null;
    public int money = 250;

    private float _electricity = 20;
	void Start () {
        UpdateMoney();
        UpdateElectricity();
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
    public void ChangeElectricity(int amount)
    {
        _electricity += amount;
        UpdateElectricity();
    }
    private void UpdateElectricity()
    {
        ElectricityText.text = _electricity.ToString();

    }
}
