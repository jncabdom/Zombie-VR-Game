using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
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

    void Start()
    {
        audio = GetComponents<AudioSource>();
        anim = GetComponent<Animation>();
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

    void shoot() {
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