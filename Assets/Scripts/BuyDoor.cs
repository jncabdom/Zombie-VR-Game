using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDoor : MonoBehaviour
{
    GameObject message;
    Text messageText;
    public static event DecreaseMoney OnDecreaseMoney;
    public static event EnoughMoney OnEnoughMoney;
    public GameObject player;
    public float radius = 2f;
    public int cost = 1000;
    public int y = -1;
    public float speed = 200f;
    AudioSource audioOpen;

    // Start is called before the first frame update
    void Start()
    {
        GameObject reference = GameObject.Find("InteractionText");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
        audioOpen = GetComponent<AudioSource>();
    }

    bool InRange() {
        return Vector3.Distance(player.transform.position, transform.position) <= radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange()) {
            messageText.text = "Press Action to clear debris (" + cost + ")";
            if (Input.GetButtonDown("Action") && OnEnoughMoney(cost)) {
                OnDecreaseMoney(cost);
                transform.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(Disappear());
                messageText.text = "";
            }
        }
        else messageText.text = "";
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