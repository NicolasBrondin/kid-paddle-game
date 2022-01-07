using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlorkController : EnemyController
{
    public float speed = 2.8f;
    public float destroyDelay = 1.0f;
    public float maxWalkingDistance = 100.0f;

    private bool isDead = false;
    private float destroyTimer;
    private bool facingRight = false;
    private float currentWalkingDistance = 0.0f;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void OnHit () {
        isDead = true;
        this.anim.SetBool("isDead", true);
        destroyTimer = Time.fixedTime;
    }

    public bool GetIsDead(){
        return this.isDead;
    }

    void Walk(){
        float horizontalMovement = 0.0f;
        if(this.currentWalkingDistance >= maxWalkingDistance && this.facingRight ){
            this.facingRight = false;
            this.anim.SetInteger("direction", -1);
        } else if(this.currentWalkingDistance <= 0.0f && !this.facingRight){
            this.facingRight = true;
            this.anim.SetInteger("direction", 1);
        } else if(!facingRight) {
            horizontalMovement = -1.0f;
            this.currentWalkingDistance -= 1.0f;
        } else {
            horizontalMovement = 1.0f;
            this.currentWalkingDistance += 1.0f;
        } 
                
        transform.position += new Vector3(horizontalMovement, 0) * Time.deltaTime * this.speed;
    }

    void FixedUpdate(){
        if(!this.isDead) {
            this.Walk();
        }
        
    }

    void Update(){
        if(isDead && (Time.fixedTime - destroyTimer) > destroyDelay) {
            Destroy(gameObject);
            this.spawnItem();
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Weapon")){
            //PlayerController playerScript = (PlayerController) GameController.Instance.player.GetComponent(typeof(PlayerController));
            this.OnHit();
        }
    }
}
