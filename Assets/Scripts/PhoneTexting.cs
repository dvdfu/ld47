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

    public void OnFailTask() {
        ReceiveMessage("hello??");
    }

    void Awake() {
        typingMessage = "pogchamp";
        clicks = 0;
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
        SoundManager.instance.Play(receiveSound);
    }

    IEnumerator SendMessageRoutine() {
        canType = false;
        Instantiate(yourMessagePrefab, conversation.transform).GetComponent<Message>().Init(typingMessage);
        yield return new WaitForSeconds(2);
        ReceiveMessage("what");
    }
}
