using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject message;
    Text messageText;
    public GameObject[] spawners;
    public int round = 1;
    public int songuisPerRound = 4;
    public int remainingSonguis;
    public int maxWaitTime = 12;
    public int minWaitTime = 5;
    bool spawn = true;

    void Start() {
        remainingSonguis = 0;
        GameObject reference = GameObject.Find("Rounds");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
    }

    void Update()
    {
        if (remainingSonguis == 0) {
            messageText.text = round.ToString();
            remainingSonguis = songuisPerRound;
            StartCoroutine(playRound());
            songuisPerRound += (Random.Range(4, 6) * (round/2));
        }
        if(PlayerStats.health <= 0) {
            StartCoroutine(backToMenu());
        }
    }
    
    IEnumerator backToMenu() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator playRound() {
        while(remainingSonguis != 1) {
            Debug.Log(remainingSonguis);
            SpawnSongui();
            remainingSonguis--;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
        round++;
        SpawnSongui();
        while(GameObject.Find("Songui Base(Clone)") != null) {
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(10);
        remainingSonguis--;
    }

    void SpawnSongui() {
        spawners[Random.Range(0, spawners.Length - 1)].SendMessage("spawn");
    }
}