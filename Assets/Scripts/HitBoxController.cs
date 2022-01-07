using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public GameObject parentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("KillBox")){
            BlorkController parentScript = (BlorkController) this.parentGameObject.GetComponent(typeof(BlorkController));
            parentScript.OnHit();
        }
    }
}
