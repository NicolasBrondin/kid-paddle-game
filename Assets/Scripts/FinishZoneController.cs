using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneController : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            GameController.Instance.Finish();
        }
    }
}
