using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  public float maxHealth = 100f;
  public static float health = 100f;
  public static int money = 10000;
  public static float reloadSpeed = 1f;
  public static float damageMultiplier = 1f;
  public static float recoverySpeed = 1f;
  public float recoverAmount = 100f;
  public float recoverCooldown = 2f;
  private bool isHealing = false;
  public GameObject weapon;

  private void Start() {
    BuyDoor.OnDecreaseMoney += DecreaseMoney;
    BuyDoor.OnEnoughMoney += EnoughMoney;
    MoveTowardsPlayer.OnDamagePlayer += Damage;
    GunScript.OnEarnMoney += IncreaseMoney;
    SonguiStats.OnEarnMoney += IncreaseMoney;
  }

  private void Update() {
    if(!isHealing)
      StartCoroutine(RecoverHealth());
  }

  IEnumerator RecoverHealth() {
      isHealing = true;
      while (health < maxHealth) {
        health += recoverAmount * recoverySpeed;
        yield return new WaitForSeconds(recoverCooldown);
      } 
      isHealing = false;
  }

  void IncreaseMoney(int amount) {
    money += amount;
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
    BuyDoor.OnDecreaseMoney -= DecreaseMoney;
    BuyDoor.OnEnoughMoney -= EnoughMoney;
    MoveTowardsPlayer.OnDamagePlayer -= Damage;
    GunScript.OnEarnMoney -= IncreaseMoney;
    SonguiStats.OnEarnMoney -= IncreaseMoney;
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

  public void destroyWeapon() {
    Debug.Log(GameObject.Find(GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(2).name));
    GameObject.Destroy(GameObject.Find(GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(2).name));
  }
}