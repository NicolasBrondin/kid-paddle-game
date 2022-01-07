using UnityEngine;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    private float finishDelay = 0.5f;
    private float finishTime = -1.0f;
    public GameObject player;
    public static GameController Instance = null;
    private void Awake()
	{
        Debug.Log("[GC] Awake");
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null){
			Instance = this;
            Debug.Log("[GC] No instance, assigning it");
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this){
            Instance.player = this.player;
			Destroy(gameObject);
            Debug.Log("[GC] Already exists, delete the wrong one");
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

    public void Update(){
        if(this.finishTime > 0 && (Time.fixedTime - this.finishTime) > this.finishDelay) {
            this.LoadScene("Levels");
            this.finishTime = -1.0f;
        }
    }

    public void LoadScene(String key){
        Application.LoadLevel(key);
    }

    public void Reload(){
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Die(){
        this.Reload();
    }

    public void StartGame(){
        this.LoadScene("Level-1");
    }

    public void Finish(){
        this.finishTime = Time.fixedTime;
    }

}