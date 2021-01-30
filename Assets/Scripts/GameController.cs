using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] spawners;
    public int round = 1;
    public int songuisPerRound = 4;
    public int remainingSonguis;
    public int maxWaitTime = 12;
    public int minWaitTime = 5;
    bool spawn = true;

    void Start() {
        remainingSonguis = 0;
    }

    void Update()
    {
        if (remainingSonguis == 0) {
            Debug.Log("End of Round");
            remainingSonguis = songuisPerRound;
            StartCoroutine(playRound());
            songuisPerRound += Random.Range(4, 6);
        }
        if(PlayerStats.health <= 0) {
        }
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