using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCalling : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] Phone phone = null;
    [SerializeField] Image profilePicture = null;
    [SerializeField] Image callPicture = null;
    [SerializeField] Text callerName = null;
    [SerializeField] GameObject incomingCallScreen = null;
    [SerializeField] GameObject callingScreen = null;
    [SerializeField] Sprite[] profileSprites = null;
    [SerializeField] Sprite spamSprite = null;
    [SerializeField] AudioSource ringSource = null;
    [SerializeField] AudioClip chatterSound = null;
    [SerializeField] AudioClip spamSound = null;
    [SerializeField] AudioClip hangupSound = null;

    bool isSpam;
    bool isCalling;
    
    public void OnPickup() {
        if (isSpam) {
            phone.FailTask();
        } else {
            phone.CompleteTask();
        }
        ringSource.Stop();
        isCalling = true;
        incomingCallScreen.SetActive(false);
    }

    public void OnHangup() {
        if (!isSpam) {
            phone.FailTask();
        }
        ringSource.Stop();
        incomingCallScreen.SetActive(false);
    }

    public void OnTimeout() {
        OnHangup();
    }

    void Start() {
        incomingCallScreen.SetActive(false);
        StartCoroutine(MainRoutine());
    }

    void ReceiveCall() {
        incomingCallScreen.SetActive(true);
        isSpam = Random.value < 0.33f;
        if (isSpam) {
            profilePicture.sprite = spamSprite;
            callPicture.sprite = spamSprite;
            callerName.text = "Spam!!";
        } else {
            int i = Random.Range(0, profileSprites.Length);
            profilePicture.sprite = profileSprites[i];
            callPicture.sprite = profileSprites[i];
            callerName.text = GetCallerName(i);
        }
        phone.ResetCountdown();
        ringSource.Play();
    }

    string GetCallerName(int i) {
        switch (i) {
            case 0: return "Boop";
            case 1: return "Crabb";
            case 2: return "Isa";
            case 3: return "Zerg";
            case 4: return "Blurp";
            case 5: return "Chungy";
            case 6: return "Moo";
            case 7: return "Peep";
            default: return "SDF";
        }
    }

    IEnumerator MainRoutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(7, 10));
            if (gameData.onFire) {
                yield break;
            }
            ReceiveCall();
            while (incomingCallScreen.activeInHierarchy) {
                yield return null;
            }
            if (isCalling) {
                callingScreen.SetActive(true);
                if (isSpam) {
                    SoundManager.instance.Play(spamSound);
                } else {
                    SoundManager.instance.Play(chatterSound);
                }
                yield return new WaitForSeconds(2.5f);
                isCalling = false;
                callingScreen.SetActive(false);
            }
            phone.ResetCountdown(false);
            SoundManager.instance.Play(hangupSound);
        }
    }
}
