using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool EnoughMoney(int price);
public delegate void DecreaseMoney(int price);

public class WeaponManager : MonoBehaviour
{
  public float radius = 2f;
  public GameObject player;
  public GameObject weapon;
  public int price = 100;
  public bool insideBuyingArea = false;
  public static event DecreaseMoney OnDecreaseMoney;
  public static event EnoughMoney OnEnoughMoney;

  // Start is called before the first frame update
  void Start() {
        
  }

  // Update is called once per frame
  void Update() {
    float distance = Vector3.Distance(transform.position, player.transform.position);
    if (distance < radius && OnEnoughMoney(price)) {
      if (Input.GetButtonDown("Action")) {
        OnDecreaseMoney(price);
        AttachWeapon();
      }
          GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyWeapon").gameObject.SetActive(true);
        } else {
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyWeapon").gameObject.SetActive(false);
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("NoMoney").gameObject.SetActive(true);
        }
      insideBuyingArea = true;
    if (distance > radius) {
      insideBuyingArea = false;
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyWeapon").gameObject.SetActive(false);
      GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("NoMoney").gameObject.SetActive(false);
      Debug.Log(PlayerStats.money);
    }
  }
  void AttachWeapon() {
    Instantiate(weapon, new Vector3(0, 2, 0), Quaternion.identity);
  }
}
