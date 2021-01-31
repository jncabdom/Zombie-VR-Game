﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
  private string health = "Health: ";
    // Start is called before the first frame update
    void Start()
    {
    GunScript.OnSetWeapon += UpdateWeapon;
    GunScript.OnSetBullets += UpdateBullets;
    GunScript.OnSetWeaponName += UpdateWeapon;
    }

    // Update is called once per frame
    void Update()
    {
      UpdateHealth();
      UpdateMoney();
    }

  void UpdateMoney() {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Money").GetComponent<Text>().text = PlayerStats.money.ToString();
  }

  void UpdateHealth() {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Health").GetComponent<Text>().text = health + PlayerStats.health;
  }

  void UpdateWeapon(GameObject weapon) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("WeaponName").GetComponent<Text>().text = weapon.name;
  }

  void UpdateWeapon(string weapon) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("WeaponName").GetComponent<Text>().text = name;
  }

  void UpdateBullets(int magazineBullets, int bullets) {
    GameObject.Find("PlayerUI").GetComponent<Canvas>().transform.Find("Bullets").GetComponent<Text>().text = magazineBullets + "/" + bullets;
  }
}

