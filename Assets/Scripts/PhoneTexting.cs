using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTexting : MonoBehaviour {
    [SerializeField] Phone phone = null;
    [SerializeField] RectTransform conversation = null;
    [SerializeField] Text typingMessageUI = null;
    [SerializeField] GameObject friendMessagePrefab = null;
    [SerializeField] GameObject yourMessagePrefab = null;
    [SerializeField] AudioClip typeSound = null;
    [SerializeField] AudioClip sendSound = null;
    [SerializeField] AudioClip receiveSound = null;

    string typingMessage;
    int clicks;
    bool canType;

    public void OnPress() {
        if (canType) {
            if (clicks < typingMessage.Length) {
                clicks++;
                SoundManager.instance.Play(typeSound);
            } else {
                clicks = 0;
                SoundManager.instance.Play(sendSound);
                phone.CompleteTask();
                StartCoroutine(SendMessageRoutine());
            }
        }
    }

    public void OnTimeout() {
        ReceiveMessage(GetRandomWaitMessage());
    }

    void Start() {
        ReceiveMessage("hey");
    }

    void Update() {
        if (conversation.transform.childCount > 5) {
            Destroy(conversation.transform.GetChild(0).gameObject);
        }
    }

    void LateUpdate() {
        typingMessageUI.text = typingMessage.Substring(0, Mathf.Min(clicks, typingMessage.Length));
    }

    void ReceiveMessage(string message) {
        Instantiate(friendMessagePrefab, conversation.transform).GetComponent<Message>().Init(message);
        phone.StartDepleting();
        canType = true;
        typingMessage = GetRandomSendMessage();
        SoundManager.instance.Play(receiveSound);
    }

    string GetRandomMessage() {
        switch (Random.Range(0, 4)) {
            case 0: return "let's hang";
            case 1: return "what's up";
            case 2: return "really??";
            case 3: return "how bout u";
        }
        return "huh";
    }

    string GetRandomSendMessage() {
        switch (Random.Range(0, 4)) {
            case 0: return "of course";
            case 1: return "doing gr8";
            case 2: return "u know it";
            case 3: return "hahahahah";
        }
        return "huh";
    }

    string GetRandomWaitMessage() {
        switch (Random.Range(0, 4)) {
            case 0: return "hello??";
            case 1: return "you there?";
            case 2: return "umm...?";
            case 3: return "...?";
        }
        return "huh";
    }

    IEnumerator SendMessageRoutine() {
        canType = false;
        Instantiate(yourMessagePrefab, conversation.transform).GetComponent<Message>().Init(typingMessage);
        yield return new WaitForSeconds(Random.Range(3, 5));
        ReceiveMessage(GetRandomMessage());
    }
}
