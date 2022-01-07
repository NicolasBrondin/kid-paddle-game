using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerController : MonoBehaviour
{

    public float deathDelay = 0.5f;
    private bool shouldDie = false;
    private float deathTimer;

    // Start is called before the first frame update
    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            SoundController.Instance.playEffect("death");
            shouldDie = true;
            deathTimer = Time.fixedTime;
        }
    }

    void Update(){
        if(shouldDie && (Time.fixedTime - deathTimer) > deathDelay) {
            GameController.Instance.Die();
        }
    }
}
