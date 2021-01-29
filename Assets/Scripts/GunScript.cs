using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire")) {
            shoot();
        }
        
    }

    void shoot() {
        muzzleFlash.Play();

        RaycastHit hitInfo;
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hitInfo, range)) {
            Debug.Log(hitInfo.transform.name);
           if (hitInfo.transform.tag == "Songui") {
                hitInfo.collider.gameObject.SendMessage("damgeSongui", damage);
           }
       }
    }
}
