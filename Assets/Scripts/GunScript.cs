using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float cadence = 3.0f;

    bool canShoot = true;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    AudioSource[] audioAk;

    void Start()
    {
        audioAk = GetComponents<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButton("Fire")) || (Input.GetAxisRaw("FireController") == 1)) {
            if(canShoot) {
                StartCoroutine(shootWait());
            }
        }
    }

    IEnumerator shootWait() {
        canShoot = false;
        shoot();
        yield return new WaitForSeconds(cadence);
        canShoot = true;
    }

    void shoot() {
        muzzleFlash.Play();
        audioAk[0].Play();

        RaycastHit hitInfo;
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hitInfo, range)) {
            Debug.Log(hitInfo.transform.name);
           if (hitInfo.transform.tag == "Songui") {
                hitInfo.collider.gameObject.SendMessage("damageSongui", damage);
           }
       }
    }
}