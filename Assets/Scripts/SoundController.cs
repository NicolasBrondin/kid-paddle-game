using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundItem {
    private AudioClip clip;
    private float priority = 1.0f;
    private float volume = 1.0f;
    private float speed = 1.0f;

    public SoundItem(AudioClip clip, float priority, float volume, float speed){
        this.clip = clip;
        this.priority = priority;
        this.volume = volume;
        this.speed = speed;
    }

    public AudioClip getClip(){
        return this.clip;
    }

    public float getPriority(){
        return this.priority;
    }

    public float getVolume(){
        return this.volume;
    }

    public float getSpeed(){
        return this.speed;
    }
}

public class SoundController : MonoBehaviour
{
    private AudioClip backgroundMusicClip;
    private Dictionary<string, SoundItem> effectClips = new Dictionary<string, SoundItem>();

    public AudioSource backgroundMusicAudioSource;
    public AudioSource effectsAudioSource;
    public SoundItem currentSoundItem;
    public static SoundController Instance = null;

    private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null){
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this){
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

    public void playEffect (String key) {
        SoundItem soundItem = effectClips[key];
        AudioClip clipToBePlayed = soundItem.getClip();
        if(clipToBePlayed != null){
            if(currentSoundItem == null || !effectsAudioSource.isPlaying || (currentSoundItem != soundItem && currentSoundItem.getPriority() <= soundItem.getPriority())){
                currentSoundItem = soundItem;
                effectsAudioSource.clip = clipToBePlayed;
                effectsAudioSource.volume = soundItem.getVolume();
                effectsAudioSource.pitch = soundItem.getSpeed();
                effectsAudioSource.Play();
            }
        }
    }

    void Start(){
        backgroundMusicClip = (AudioClip)Resources.Load("Sounds/Caketown");
        effectClips.Add("jump", new SoundItem((AudioClip)Resources.Load("Sounds/effects/jump"), 1.0f, 0.5f, 1.0f));
        effectClips.Add("walk", new SoundItem((AudioClip)Resources.Load("Sounds/effects/walk"), 0.5f, 0.05f, 1.7f));
        effectClips.Add("loot", new SoundItem((AudioClip)Resources.Load("Sounds/effects/loot"), 1.0f, 0.5f, 1.0f));
        effectClips.Add("death", new SoundItem((AudioClip)Resources.Load("Sounds/effects/death"), 1.0f, 1.5f, 1.0f));
        effectClips.Add("hit", new SoundItem((AudioClip)Resources.Load("Sounds/effects/hit"), 1.0f, 0.5f, 1.0f));
        effectClips.Add("attack", new SoundItem((AudioClip)Resources.Load("Sounds/effects/weapon"), 1.0f, 0.5f, 1.0f));
        effectClips.Add("door", new SoundItem((AudioClip)Resources.Load("Sounds/effects/door"), 1.0f, 0.2f, 1.0f));
        backgroundMusicAudioSource.clip = backgroundMusicClip;
        backgroundMusicAudioSource.Play();
    }
}