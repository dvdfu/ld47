using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour {
    [SerializeField] TextAsset nounsFile = null;
    [SerializeField] TextAsset adjectivesFile = null;

    string[] nouns;
    string[] adjectives;

    public string GenSentence(int wordCount) {
        bool noun = false;
        string sentence = "";
        for (int i = 0; i < wordCount; i++) {
            if (noun) {
                sentence += GenNoun();
            } else {
                sentence += GenAdjective();
            }
            if (i < wordCount - 1) {
                sentence += " ";
            } else {
                sentence += ".";
            }
            noun = !noun;
        }
        return sentence;
    }

    void Awake() {
        nouns = nounsFile.text.Split('\n');
        adjectives = adjectivesFile.text.Split('\n');
    }

    void Start() {
        Debug.Log(GenSentence(10));
    }

    string GenNoun() {
        int i = Random.Range(0, nouns.Length);
        return nouns[i];
    }

    string GenAdjective() {
        int i = Random.Range(0, adjectives.Length);
        return adjectives[i];
    }
}
