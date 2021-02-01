using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Windows.Speech;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{/*
    private KeywordRecognizer recognizer;
    private string[] keywords = new string[]{"ábrete"};
    public float detectionRadius = 2f;
    public GameObject player;
    private void Start()
    {
        recognizer = new KeywordRecognizer(keywords);
        recognizer.OnPhraseRecognized += MyPhraseRecognized;
    }

    private void Update()
    {
        CheckDoor();
    }

    void CheckDoor() {
        player = GameObject.Find("Player");
        if (onRange(player)) {
            recognizer.Start();
        }
    }

    bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) < detectionRadius);
    }

    private void MyPhraseRecognized(PhraseRecognizedEventArgs data) {
        if (data.text == "ábrete") {;
            gameObject.SetActive(false);
            stopRecognition();
        } else {
            Debug.Log("A problem has occurred and no words were recognized.\n");
            stopRecognition();
        }
    }
    
    public void stopRecognition() {
        recognizer.Stop();
        recognizer.Dispose();
    }
*/}