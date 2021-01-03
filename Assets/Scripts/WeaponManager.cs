using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool EnoughMoney(int price);
public delegate void DecreaseMoney(int price);
public delegate bool SameWeapon(string weaponName);
public delegate void SetWeapon(GameObject newWeapon);

public class WeaponManager : MonoBehaviour
{
  public float radius = 2f;
  public GameObject player;
  public GameObject weapon;
  public int weaponPrice = 100;
  public int bulletsPrice = 25;
  public bool insideBuyingArea = false;
  public static event DecreaseMoney OnDecreaseMoney;
  public static event EnoughMoney OnEnoughMoney;
  public static event SameWeapon OnSameWeapon;
  public static event SetWeapon OnSetWeapon;

  // Update is called once per frame
  void Update() {
    float distance = Vector3.Distance(transform.position, player.transform.position);
    if (distance <= radius) {
      if (!OnSameWeapon(weapon.name) && OnEnoughMoney(weaponPrice)) {
        if (Input.GetButtonDown("Action")) {
          OnDecreaseMoney(weaponPrice);
          AttachWeapon();
          OnSetWeapon(weapon);
        }
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyWeapon").gameObject.SetActive(true);
      } else if (OnSameWeapon(weapon.name) && OnEnoughMoney(bulletsPrice)) {
        if (Input.GetButtonDown("Action")) {
          OnDecreaseMoney(bulletsPrice);
          Debug.Log("+100 balas");
          // Meterle balas a un arma
        }
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyBullets").gameObject.SetActive(true);
      } else {
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("NoMoney").gameObject.SetActive(true);
      }
    } else {
      RemoveGUITexts();
      Debug.Log(PlayerStats.money);
    }
  }

  void RemoveGUITexts() {
    for (int child = 0; child < GameObject.Find("Canvas").GetComponent<Canvas>().transform.childCount; child++) {
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.GetChild(child).gameObject.SetActive(false);
    }
  }

  void AttachWeapon() {
    GameObject gun = Instantiate(weapon, new Vector3(player.transform.position.x,
                                                     player.transform.position.y + 1,
                                                     player.transform.position.z + 1), Quaternion.identity);
    gun.transform.SetParent(player.transform);
  }
}
