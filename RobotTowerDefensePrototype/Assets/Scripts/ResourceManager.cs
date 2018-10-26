using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {
    public Text moneyText = null, ElectricityText = null;
    public int money = 250;
    public List<Turret> turrets = null;

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

        if (_electricity <= -50)
        {
            Turret.noElectricity = true;
            UpdateElectricity();
            BalanceElectricity();
            return;
        }
        else if (_electricity < 0)
        {
            BalanceElectricity();
        }
        else if (_electricity >= 0)
        {
            Turret.modifier = 1;
            Turret.noElectricity = !Turret.noElectricity;
        }

        UpdateElectricity();
    }
    private void UpdateElectricity()
    {
        ElectricityText.text = _electricity.ToString();
    }
    private void BalanceElectricity()
    {
        float tempElectrcity = Mathf.Abs(_electricity);
        tempElectrcity /= 10;
        Turret.modifier = tempElectrcity;
    }
}
