using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here I'm defining health, cash...
public class PlayerStats : MonoBehaviour
{
  public static float health = 100;
  public static int money = 400;

  private void Start() {
    WeaponManager.OnDecreaseMoney += DecreaseMoney;
    WeaponManager.OnEnoughMoney += EnoughMoney;
  }

  // If enough money then decrease it and return a success state
  void DecreaseMoney(int price) {
    money -= price;
  }

  bool EnoughMoney(int price) {
    return (money >= price)? true : false;
  }

  private void OnDisable() {
    WeaponManager.OnDecreaseMoney -= DecreaseMoney;
    WeaponManager.OnEnoughMoney -= EnoughMoney;
  }

  // Update is called once per frame
  void Update()
    {
        
    }
}
