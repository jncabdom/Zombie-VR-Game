using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  public static float health = 100;
  public static int money = 10000;
  public static float reloadSpeed = 1f;
  public static float damageMultiplier = 1f;
  public static float recoverySpeed = 1f;
  public GameObject weapon;

  private void Start() {
    WeaponManager.OnDecreaseMoney += DecreaseMoney;
    WeaponManager.OnEnoughMoney += EnoughMoney;
    WeaponManager.OnSameWeapon += SameWeapon;
    WeaponManager.OnSetWeapon += SetWeapon;
    BuyDoor.OnDecreaseMoney += DecreaseMoney;
    BuyDoor.OnEnoughMoney += EnoughMoney;
    MoveTowardsPlayer.OnDamagePlayer += Damage;
  }

  private void Update() {
  }

  // If enough money then decrease it and return a success state
  void DecreaseMoney(int price) {
    if (EnoughMoney(price)) {
      money -= price;
    }
  }

  void Damage(int amount) {
    health -= amount;
  }

  bool EnoughMoney(int price) {
    return money >= price;
  }

  bool SameWeapon(string weaponName) {
    return weapon.name == weaponName;
  }

  void SetWeapon(GameObject newWeapon) {
    weapon = newWeapon;
  }

  private void OnDisable() {
    WeaponManager.OnDecreaseMoney -= DecreaseMoney;
    WeaponManager.OnEnoughMoney -= EnoughMoney;
  }

  public void increaseHealth(float amount) {
    health *= amount;
  }

  public void increaseDmg(float amount) {
    damageMultiplier *= amount;
  }

  public void increaseReloadSpeed(float amount) {
    reloadSpeed *= amount;
  }
  
  public void increaseRecovery(float amount) {
    recoverySpeed *= amount;
  }
}