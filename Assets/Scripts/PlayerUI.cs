using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Se encarga de actualizar los valores de la  UI
public class PlayerUI : MonoBehaviour
{
  void Awake() {
    WeaponBuyLogic.OnSetWeapon += UpdateWeapon;
    GunScript.OnSetBullets += UpdateBullets;
  }
  private string health = "Health: ";           // String donde añadiremos el valor de la vida

    void Update()
    {
      UpdateHealth();
      UpdateMoney();
    }

  // Actualizamos el dinero
  void UpdateMoney() {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Money").GetComponent<Text>().text = PlayerStats.money.ToString();
  }

  // Actualizamos la vida
  void UpdateHealth() {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Health").GetComponent<Text>().text = health + PlayerStats.health;
  }

  // Actualizamos el arma
  void UpdateWeapon(GameObject weapon) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("WeaponName").GetComponent<Text>().text = weapon.name;
  }

  // Actualizamos el arma pasándo un string como argumento
  void UpdateWeapon(string weapon) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("WeaponName").GetComponent<Text>().text = name;
  }

  // Actualizamos las balas
  void UpdateBullets(int magazineBullets, int bullets) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Bullets").GetComponent<Text>().text = magazineBullets + "/" + bullets;
  }
}

