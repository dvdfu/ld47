using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCalling : MonoBehaviour {
    [SerializeField] Phone phone = null;
    [SerializeField] Image profilePicture = null;
    [SerializeField] Text callerName = null;
    [SerializeField] GameObject incomingCallScreen = null;
    [SerializeField] GameObject callingScreen = null;
    [SerializeField] AudioClip chatterSound = null;
    [SerializeField] AudioClip hangupSound = null;

    bool isSpam;
    bool isCalling;
    
    public void OnPickup() {
        phone.CompleteTask();
        isCalling = true;
        incomingCallScreen.SetActive(false);
    }

    public void OnHangup() {
        phone.CompleteTask();
        incomingCallScreen.SetActive(false);
    }

    void Start() {
        incomingCallScreen.SetActive(false);
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(3, 5));
            incomingCallScreen.SetActive(true);
            isSpam = Random.value < 0.5f;
            phone.StartDepleting();
            while (incomingCallScreen.activeInHierarchy) {
                yield return null;
            }
            if (isCalling) {
                callingScreen.SetActive(true);
                SoundManager.instance.Play(chatterSound);
                yield return new WaitForSeconds(2.5f);
                isCalling = false;
                callingScreen.SetActive(false);
            }
            SoundManager.instance.Play(hangupSound);
        }
    }
}
