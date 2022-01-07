using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject coinsText;
    public GameObject coinsIcon;
    public GameObject keysText;
    public GameObject keysIcon;
    public static UIController Instance = null;

    private float cameraWidth;
    private float cameraHeight;

    private void Awake(){
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null){
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this){
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

    private void Start(){
        this.resetElementsPosition();
    }

    public void setLife(int life){
        this.heart1.GetComponent<SpriteRenderer>().enabled = true;
        this.heart2.GetComponent<SpriteRenderer>().enabled = true;
        this.heart3.GetComponent<SpriteRenderer>().enabled = true;
        if(life < 3){
            this.heart3.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(life < 2){
            this.heart2.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(life < 1){
            this.heart1.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void setCoins(int coins){
        this.coinsText.GetComponent<Text>().text = coins.ToString();
    }

    public void setKeys(int keys){
        this.keysText.GetComponent<Text>().text = keys.ToString();
    }

    private Vector2 getPointRelativeToCanvas(Vector2 point){
        return  new Vector2(point.x - (this.cameraWidth/2), (this.cameraHeight/2) - point.y);
    }

    private void resetElementsPosition(){
        RectTransform canvasRect = this.GetComponent<RectTransform>();
        float h = canvasRect.rect.height;
        float w = canvasRect.rect.width;
        if(this.cameraWidth != w || this.cameraHeight != h){
            this.cameraHeight = h;
            this.cameraWidth = w;
            this.heart1.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(50,50));
            this.heart2.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(100,50));
            this.heart3.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(150,50));
            this.coinsText.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(50,100));
            this.coinsIcon.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(90,100));
            this.keysText.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(150,100));
            this.keysIcon.GetComponent<RectTransform>().anchoredPosition = this.getPointRelativeToCanvas(new Vector2(180,100));
        }
    }

    private void Update(){
        this.resetElementsPosition();
    }
}


    