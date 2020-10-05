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
        ReceiveMessage("hello??");
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
        typingMessageUI.text = typingMessage.Substring(0, clicks);
    }

    void ReceiveMessage(string message) {
        Instantiate(friendMessagePrefab, conversation.transform).GetComponent<Message>().Init(message);
        phone.StartDepleting();
        canType = true;
        typingMessage = GetRandomMessage();
        SoundManager.instance.Play(receiveSound);
    }

    string GetRandomMessage() {
        switch (Random.Range(0, 8)) {
            case 0: return "oh!!";
            case 1: return "lol";
            case 2: return "ofc";
            case 3: return "let's hang";
            case 4: return "oh rly";
            case 5: return "u know it";
            case 6: return "perfect";
            case 7: return "okay!";
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
