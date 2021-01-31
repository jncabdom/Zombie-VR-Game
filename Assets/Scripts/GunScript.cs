using System.Collections;
using UnityEngine;

public delegate void SetBullets(int magazineBullets, int bullets);
public delegate void SetWeapon(GameObject newWeapon);
public delegate void SetWeaponName(string name);

public class GunScript : MonoBehaviour
{
    public static event SetBullets OnSetBullets;
    public static event SetWeapon OnSetWeapon;
    public static event SetWeaponName OnSetWeaponName;

    public float damage = 10f;
    public float range = 100f;
    public float cadence = 0.3f;

    bool canShoot = true;
    public bool walled = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    AudioSource[] audio;
    Animation anim;
    public GameObject blood;

    // Bullets
    public int magazineBullets = 24;
    public int bullets = 64;
    private int currentBullets;
    private int currentMagazineBullets;

    void Start()
    {
        WeaponBuyLogic.OnBuyBullets += FillBullets;
        audio = GetComponents<AudioSource>();
        anim = GetComponent<Animation>();
        OnSetWeapon(gameObject);
        FillBullets();
    }


    // Update is called once per frame
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

    IEnumerator shootWait() {
        canShoot = false;
        shoot();
        yield return new WaitForSeconds(cadence);
        canShoot = true;
    }

    void FillBullets() {
        currentBullets = bullets;
        currentMagazineBullets = magazineBullets;
        OnSetBullets(currentMagazineBullets, currentBullets);
    }

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
                    hitInfo.collider.gameObject.SendMessage("damageSongui", damage);
                }
            }
       }
    }

    void Reload() {
        if (Input.GetButtonDown("Action") && !MagazineFull()) {
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
    Debug.Log(currentMagazineBullets);
    return currentMagazineBullets == 0;
  }

  bool MagazineFull() {
    return currentMagazineBullets == magazineBullets;
  }

  bool FullOfBullets() {
    return currentBullets == bullets && MagazineFull();
  }
}