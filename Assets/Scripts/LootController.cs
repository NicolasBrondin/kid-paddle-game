using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public string type = "unknown";

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            PlayerController playerScript = (PlayerController) GameController.Instance.player.GetComponent(typeof(PlayerController));
            if(this.type == "apple"){
                playerScript.updateLife(1);
            } else if(this.type == "coin") {
                playerScript.updateCoins(1);
            } else if(this.type == "key") {
                playerScript.updateKeys(1);
            } else if(this.type == "weapon") {
                playerScript.getWeapon();
            }
            SoundController.Instance.playEffect("loot");
            
            Destroy(this.gameObject);
        }
    }
}
