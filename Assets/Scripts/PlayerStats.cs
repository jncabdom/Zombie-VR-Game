using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void IncreaseMagazine(float amount);

// Estadísticas asociadas al jugador
public class PlayerStats : MonoBehaviour
{
  public static event IncreaseMagazine OnIncreaseMagazine;

  public float maxHealth = 100f;              // Máximo de vida del jugador
  public static float health = 100f;          // Vida actual del jugador
  public static int money = 10000;            // Dinero del jugador
  public static float magazineSize = 1f;      // Multiplicador del tamaño del cargador
  public static float damageMultiplier = 1f;  // Multiplicador del daño del arma
  public static float recoverySpeed = 1f;     // Velocidad a la que se recupera la vida
  public float recoverAmount = 100f;          // Cantidad de vida que se recupera cada recoverCooldown
  public float recoverCooldown = 2f;          // Enfriamiento para que vuelva a recuperar vida
  private bool isHealing = false;             // Controla si el jugador se está curando
  public GameObject weapon;                   // Arma que porta el jugador

  // Suscribimos los eventos a los siguientes métodos
  private void Start() {
    BuyDoor.OnDecreaseMoney += DecreaseMoney;
    BuyDoor.OnEnoughMoney += EnoughMoney;
    MoveTowardsPlayer.OnDamagePlayer += Damage;
    GunScript.OnEarnMoney += IncreaseMoney;
    SonguiStats.OnEarnMoney += IncreaseMoney;
  }

  // Si el jugador no se está curando llama a la corrutina
  private void Update() {
    if(!isHealing)
      StartCoroutine(RecoverHealth());
  }

  // Si la vida es inferior a la máxima cura al jugador y espera durante un enfriamiento para comprobar si es necesario volver a curarlo así
  // sucesivamente
  IEnumerator RecoverHealth() {
      isHealing = true;
      while (health < maxHealth) {
        health += recoverAmount * recoverySpeed;
        yield return new WaitForSeconds(recoverCooldown);
      } 
      isHealing = false;
  }

  // Aumenta el dinero una cantidad
  void IncreaseMoney(int amount) {
    money += amount;
  }

  // Disminuye el dinero en una cantidad si hay dinero suficiente
  void DecreaseMoney(int price) {
    if (EnoughMoney(price)) {
      money -= price;
    }
  }

  // Disminuye la vida del jugador
  void Damage(int amount) {
    health -= amount;
  }

  // Devuelve verdadero si el dinero es mayor que el precio
  bool EnoughMoney(int price) {
    return money >= price;
  }

  // Devuelve verdadero si el arma equipada es la misma que el arma nueva
  bool SameWeapon(string weaponName) {
    return weapon.name == weaponName;
  }

  // Establece una nueva arma para el jugador
  void SetWeapon(GameObject newWeapon) {
    weapon = newWeapon;
  }

  // Desuscribimos los métodos de los eventos
  private void OnDisable() {
    BuyDoor.OnDecreaseMoney -= DecreaseMoney;
    BuyDoor.OnEnoughMoney -= EnoughMoney;
    MoveTowardsPlayer.OnDamagePlayer -= Damage;
    GunScript.OnEarnMoney -= IncreaseMoney;
    SonguiStats.OnEarnMoney -= IncreaseMoney;
  }

  // Aumentamos la vida un valor
  public void increaseHealth(float amount) {
    maxHealth *= amount;
  }

  // Aumentamos el daño del arma
  public void increaseDmg(float amount) {
    damageMultiplier *= amount;
  }

  // Aumentamos el tamaño del cargador
  public void increaseMagazineSize(float amount) {
    magazineSize *= amount;
    OnIncreaseMagazine(magazineSize);
  }
  
  // Aumentamos la velocidad a la que se recupera vida
  public void increaseRecovery(float amount) {
    recoverySpeed *= amount;
  }

  // Eliminamos el arma anterior
  public void destroyWeapon() {
    GameObject.Destroy(GameObject.Find(GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(2).name));
  }
}