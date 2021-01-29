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

    AudioSource[] audioAk;
    Animator anim;
    public GameObject blood;

    void Start()
    {
        audioAk = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
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
        anim.SetBool("Shoot", true);
        muzzleFlash.Play();
        audioAk[0].Play();

        RaycastHit hitInfo;
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hitInfo, range)) {
            Debug.Log(hitInfo.transform.name);
           if (hitInfo.transform.tag == "Songui") {
               GameObject newObj = Instantiate(blood, hitInfo.point, Quaternion.Euler(-fpsCam.transform.forward), hitInfo.transform);
               // GameObject newObj = Instantiate(blood, hitInfo.point, Quaternion.SetFromToRotation(hitInfo.transform.position, fpsCam.transform.position), hitInfo.transform);
               Destroy(newObj, 0.5f);
                hitInfo.collider.gameObject.SendMessage("damageSongui", damage);
           }
       }
        anim.SetBool("Shoot", false);
    }
}