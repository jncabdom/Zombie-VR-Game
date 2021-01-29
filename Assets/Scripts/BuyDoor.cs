using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDoor : MonoBehaviour
{
    public static event DecreaseMoney OnDecreaseMoney;
    public static event EnoughMoney OnEnoughMoney;
    public GameObject player;
    public float radius = 2f;
    public int cost = 1000;
    public int y = -1;
    public float speed = 200f;
    private bool bought = false;
    AudioSource audioOpen;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Info").GetComponent<Canvas>().transform.Find("BuyDoor").GetComponent<Text>().text += "[" + cost.ToString() + "]"; 
        audioOpen = GetComponent<AudioSource>();
    }

    bool InRange() {
        return Vector3.Distance(player.transform.position, transform.position) <= radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange() && !bought) {
            transform.Find("Info").GetComponent<Canvas>().transform.Find("BuyDoor").gameObject.SetActive(true);
            if (Input.GetButtonDown("Action") && OnEnoughMoney(cost) && !bought) {
                bought = true;
                OnDecreaseMoney(cost);
                transform.Find("Info").GetComponent<Canvas>().transform.Find("BuyDoor").gameObject.SetActive(false);
                transform.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(Disappear());
            }
        } else {
            transform.Find("Info").GetComponent<Canvas>().transform.Find("BuyDoor").gameObject.SetActive(false);
        }
    }

    IEnumerator Disappear() {
        audioOpen.Play();
        Vector3 target = new Vector3(transform.position.x, transform.position.y * y, transform.position.z);
        while (Vector3.Distance(transform.position, target) > radius) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForSeconds(.5f);
        }
        gameObject.SetActive(false);
    }
}
