using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] spawners;
    public int round = 1;
    public int songuisPerRound = 4;
    public int remainingSonguis;
    public int maxWaitTime = 5;
    public int minWaitTime = 1;
    bool spawn = true;

    void Start()
    {
        remainingSonguis = songuisPerRound;
    }

    void Update()
    {
        if (remainingSonguis == 0) {
            Debug.Log("End of Round");
        }
        if(PlayerStats.health <= 0) {
            Debug.Log("You Died");
        }
        if(spawn) {
            StartCoroutine(SpawnSongui());
        }
    }

    IEnumerator SpawnSongui() {
        spawn = false;
        spawners[Random.Range(0, spawners.Length - 1)].SendMessage("spawn");
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        spawn = true;
    }
}