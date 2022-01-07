using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = player.transform.position;
        temp.z -= 10;
        temp.x += 3;
        temp.y += 2;
        transform.position = temp;
    }
}
