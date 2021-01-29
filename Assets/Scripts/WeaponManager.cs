using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool EnoughMoney(int price);
public delegate void DecreaseMoney(int price);
public delegate bool SameWeapon(string weaponName);
public delegate void SetWeapon(GameObject newWeapon);
public delegate void SetBullets(int magazineBullets, int bullets);

public class WeaponManager : MonoBehaviour
{

  public Transform CameraTransform;

  // Detection of player
  public float radius = 2f;
  public GameObject player;
  private bool insideBuyingArea = false;

  // Checking for cash and weapon attached to the player
  public GameObject weapon;
  public int weaponPrice = 100;
  public int bulletsPrice = 25;

  // Events to control PlayerStats
  public static event DecreaseMoney OnDecreaseMoney;
  public static event EnoughMoney OnEnoughMoney;
  public static event SameWeapon OnSameWeapon;
  public static event SetWeapon OnSetWeapon;
  public static event SetBullets OnSetBullets;

  // Bullets
  public int magazineBullets = 24;
  public int bullets = 64;
  private int currentBullets;
  private int currentMagazineBullets;

  // Gun position and rotation
  public Vector3 pos = new Vector3(0.3f, 4.3f, 0.6f);
  // public Vector3 rot = new Vector3(1.54f, -12.47f, -2.37f);

  private void Start() {
    currentBullets = bullets;
    currentMagazineBullets = magazineBullets;
  }

  // Update is called once per frame
  void Update() {
    BuyGun();
    Shoot();
    Reload();
  }

  void BuyGun() {
    float distance = Vector3.Distance(transform.position, player.transform.position);
    if (distance <= radius) {
      insideBuyingArea = true;
      if (!OnSameWeapon(weapon.name) && OnEnoughMoney(weaponPrice)) {
        if (Input.GetButtonDown("Action")) {
          OnDecreaseMoney(weaponPrice);
          AttachWeapon();
          OnSetWeapon(weapon);
        }
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyWeapon").gameObject.SetActive(true);
      } else if (OnSameWeapon(weapon.name) && OnEnoughMoney(bulletsPrice)) {
        if (Input.GetButtonDown("Action") && !FullOfBullets()) {
          OnDecreaseMoney(bulletsPrice);
          currentBullets = bullets;
          currentMagazineBullets = magazineBullets;
          OnSetBullets(currentMagazineBullets, currentBullets);
        }
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyBullets").gameObject.SetActive(true);
      } else {
        RemoveGUITexts();
        GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("NoMoney").gameObject.SetActive(true);
      }
    } else {
      insideBuyingArea = false;
      RemoveGUITexts();
    }
  }

  void RemoveGUITexts() {
    for (int child = 0; child < GameObject.Find("Canvas").GetComponent<Canvas>().transform.childCount; child++) {
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.GetChild(child).gameObject.SetActive(false);
    }
  }

  void AttachWeapon() {
    Debug.Log(player.transform.position);
    GameObject gun = Instantiate(weapon, new Vector3(player.transform.position.x,
                                                     player.transform.position.y + 1f,
                                                     player.transform.position.z), Quaternion.identity);
    gun.transform.SetParent(CameraTransform);
    gun.transform.rotation = CameraTransform.rotation;
    gun.transform.localPosition = new Vector3(0.15f, -0.1f, 0.3f);
    Debug.Log(gun.transform.localPosition.y);
    OnSetBullets(currentMagazineBullets, currentBullets);
  }

  void Shoot() {
    if (Input.GetButtonDown("Fire") && !MagazineEmpty()) {
      currentMagazineBullets--;
      OnSetBullets(currentMagazineBullets, currentBullets);
    }
  }

  void Reload() {
    if (Input.GetButtonDown("Action") && !insideBuyingArea && !MagazineFull()) {
      if (EnoughForMagazine()) {                                        // If there are enough bullets to fill the magazine
        currentBullets -= magazineBullets - currentMagazineBullets;
        currentMagazineBullets = magazineBullets;
      } else {                                                          // Otherwise  we calculate the total of bullets we still have
        int totalBullets = currentMagazineBullets + currentBullets;     // If the totalbullets are greater than a magazine stock
        int leftBullets = totalBullets - magazineBullets;               
        if (leftBullets > 0) {
          currentBullets = leftBullets;
          currentMagazineBullets = magazineBullets;
        } else {
          currentBullets = 0;
          currentMagazineBullets = totalBullets;
        }
      }
      OnSetBullets(currentMagazineBullets, currentBullets);
    }
  }

  bool EnoughForMagazine() {
    return currentBullets >= magazineBullets;
  }

  bool MagazineEmpty() {
    return currentMagazineBullets == 0;
  }

  bool MagazineFull() {
    return currentMagazineBullets == magazineBullets;
  }

  bool FullOfBullets() {
    return currentBullets == bullets && MagazineFull();
  }
}
