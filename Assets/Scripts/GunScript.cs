using System.Collections;
using UnityEngine;

public delegate void SetBullets(int magazineBullets, int bullets);
public delegate void EarnMoney(int amount);


// Se encarga de que el arma del jugador pueda disparar, comprueba si ha disparado a un zombie y de recargar 
public class GunScript : MonoBehaviour
{
    public static event SetBullets OnSetBullets;                // Actualiza las balas en el UI
    public static event SetWeapon OnSetWeapon;                  // Actualiza el arma en el UI
    public static event EarnMoney OnEarnMoney;                  // Al golpear un zombie aumenta el dinero del jugador

    public float damage = 10f;                                  // Daño del arma
    public float range = 100f;                                  // Alcance
    public float cadence = 0.3f;                                // Cadencia

    bool canShoot = true;                                       // Se utiliza para controlar la cadencia del arma
    public bool walled = false;                                 // Indica si el arma se encuentra en la pared o no

    public Camera fpsCam;                                       // Cámara del jugador
    public ParticleSystem muzzleFlash;                          // Particulas al disparar

    AudioSource[] audio;                                        // Audios de recarga y disparo
    Animation anim;                                             // Animación de disparo
    public GameObject blood;                                    // Partícula de sangre que se crea al golpear al zombie
    public int amount = 10;                                     // Cantidad de dinero que se le aumenta al jugador por disparo acertado

    // Bullets
    public int magazineBullets = 24;                            // Tamaño del cargador
    public int bullets = 64;                                    // Total de balas que posee el arma
    private int currentBullets;                                 // Total de balas actual del arma
    private int currentMagazineBullets;                         // Total de balas en el cargador actualmente

    // Una vez se compra un arma se le llena de balas  y obtenemos los audios y la animación
    void Start()
    {
        WeaponBuyLogic.OnBuyBullets += FillBullets;
        PlayerStats.OnIncreaseMagazine += IncreaseMagazine;
        audio = GetComponents<AudioSource>();
        anim = GetComponent<Animation>();
        FillBullets();
    }


    // Si el arma no se encuentra en la pared y se pulsa el botón de disparar, se llama a la corrutina "shootWait", además comprobamos si 
    // el jugador quiere recargar
    void Update()
    {
        if(!walled) {
            if ((Input.GetButton("Fire")) || (Input.GetAxisRaw("FireController") == 1)) {
                if(canShoot) {
                    StartCoroutine(shootWait());
                }
            }
            Reload();
        }
    }

    void unwall() {
        walled = false;
    }

    // llama a shoot y estable una cadencia una cadencia
    IEnumerator shootWait() {
        canShoot = false;
        shoot();
        yield return new WaitForSeconds(cadence);
        canShoot = true;
    }

    // Llena de balas el arma y actualiza los valores en el UI
    void FillBullets() {
        currentBullets = bullets;
        currentMagazineBullets = magazineBullets;
        OnSetBullets(currentMagazineBullets, currentBullets);
    }

    // Si el cargador no está vacio, quitamos una bala y actualizamos los valores, reproducimos las animaciones y el sonido, por último comprobamos que si golpeamos a un 
    // zombie le hagamos daño y que el jugador gane dinero.
    void shoot() {
        if (!MagazineEmpty()) {
            currentMagazineBullets--;
            OnSetBullets(currentMagazineBullets, currentBullets);
            anim.Play("Shoot");
            muzzleFlash.Play();
            audio[0].Play();

            RaycastHit hitInfo;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hitInfo, range)) {
                if (hitInfo.transform.tag == "Songui") {
                    GameObject newObj = Instantiate(blood, hitInfo.point, Quaternion.Euler(-fpsCam.transform.forward), hitInfo.transform);
                    Destroy(newObj, 0.5f);
                    hitInfo.collider.gameObject.SendMessage("damageSongui", damage * PlayerStats.damageMultiplier);
                    OnEarnMoney(amount);
                }
            }
       }
    }

    // Cuando el jugador consume la ventaja "speedCola" el tamaño del cargador se duplicará y además obtendrá un cargado lleno de balas
    void IncreaseMagazine(float amount) {
        magazineBullets *= (int)amount;
        currentMagazineBullets = magazineBullets;
        OnSetBullets(currentMagazineBullets, currentBullets);
    }

    // Si el jugador pulsa el botón de "Action" recargará el arma mientras tenga balas, además se reproducirá el audio correspondiente
    // al final actualizamos las balas en el UI
    void Reload() {
        if (Input.GetButtonDown("Action") && !MagazineFull()) {
        audio[1].Play();
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

   // Devuelve verdadero si el número de balas restantes es mayor o igual al tamaño del cargador
   bool EnoughForMagazine() {
    return currentBullets >= magazineBullets;
  }

  // Devuelve verdadero si el cargador está vacio
  bool MagazineEmpty() {
    return currentMagazineBullets == 0;
  }

  // Devuelve verdadero si el cargador está lleno
  bool MagazineFull() {
    return currentMagazineBullets == magazineBullets;
  }

  // devuelve verdadero si el jugador tiene las balas tanto del cargador como el total lleno
  bool FullOfBullets() {
    return currentBullets == bullets && MagazineFull();
  }
}