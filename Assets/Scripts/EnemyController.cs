using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject itemToBeSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnItem() {
        
        Vector3 itemPosition = transform.position;
        itemPosition.y += 1.5f;
        //if(facingRight){
            itemPosition.x += 1.5f;
        /*} else {
                itemPosition.x -= 1.5f;
        }*/

        GameObject newItem;
        newItem = (GameObject)Instantiate(itemToBeSpawned, itemPosition, Quaternion.identity);

        Rigidbody2D rbItem = newItem.GetComponent<Rigidbody2D>();

        float horizontalVelocity;

        //if(facingRight){
            horizontalVelocity = 1f;
       /* } else {
            horizontalVelocity = -6f;
        }*/
        rbItem.velocity = new Vector2(horizontalVelocity, 2f);

       /* if(facingRight){
            rbItem.angularVelocity = 360f;
        } else {
            rbItem.angularVelocity = -360f;
        }*/

        
    }
}
