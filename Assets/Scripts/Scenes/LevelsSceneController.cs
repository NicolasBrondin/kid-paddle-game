using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsSceneController : MonoBehaviour
{
    public void OnClickLevel1(){
        GameController.Instance.LoadScene("Level-1");
    }
    public void OnClickLevel2(){
        GameController.Instance.LoadScene("Level-2");
    }
    public void OnClickBack(){
        GameController.Instance.LoadScene("Start");
    }
}
