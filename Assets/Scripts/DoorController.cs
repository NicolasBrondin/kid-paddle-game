using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject openedDoorObject;
    public GameObject closedDoorObject;
    public GameObject uiCanvas;
    public GameObject helpButton;
    public GameObject actionButton;
    private bool isDoorOpened = false;
    private bool playerIsNear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        closedDoorObject.SetActive(true);
        openedDoorObject.SetActive(false);
        helpButton.SetActive(false);
        actionButton.SetActive(false);
        uiCanvas.SetActive(false);
    }

    private void OpenDoor(){
        closedDoorObject.SetActive(false);
        openedDoorObject.SetActive(true);
        this.isDoorOpened = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.playerIsNear && !this.isDoorOpened){
            uiCanvas.SetActive(true);
            helpButton.SetActive(false);
            actionButton.SetActive(false);
            PlayerController playerScript = (PlayerController) GameController.Instance.player.GetComponent(typeof(PlayerController));
            if(playerScript.getKeysCount() > 0) {
                
                actionButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && this.isDoorOpened == false){
                    playerScript.updateKeys(-1);
                    this.OpenDoor();
                    SoundController.Instance.playEffect("door");
                }
            } else {
                helpButton.SetActive(true);
            }
        } else {
            uiCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            this.playerIsNear = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            this.playerIsNear = false;
        }
    }
}
