using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;

    [SerializeField] AudioSource source = null;

    public void Play(AudioClip clip) {
        source.PlayOneShot(clip);
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
}
