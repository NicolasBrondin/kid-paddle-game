using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SPEED = 1.0f;
    public float repelForce = 1.0f;
    public float jumpForce = 10f;
    public Transform groundObject;
    public GameObject weapon;

    private Rigidbody2D rb2d;
    private Animator anim;
    public FixedJoystick variableJoystick;
    public Canvas mobileUI;
    private bool grounded = false;
    private bool attacked = false;
    private bool jump = false;
    private bool facingRight = true;
    private float damageTimer;
    public float damageDuration = 1.0f;
    private int life = 3;
    private int coins = 0;
    private int keys = 0;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(this.mobileUI != null && Application.platform != RuntimePlatform.Android) {
            this.mobileUI.enabled = false;
        } else {
            this.mobileUI.enabled = true;
        };
    }

    void Update()
    {
        transform.rotation = new Quaternion(0,0,0,0);
        this.grounded = Physics2D.Linecast(transform.position, groundObject.position, 1 << LayerMask.NameToLayer("Ground"));
        float verticalMovement = 0.0f;
        if(Application.platform == RuntimePlatform.Android) {
            verticalMovement = variableJoystick.Vertical;
        } else {
            verticalMovement = Input.GetAxis("Vertical");
        };
        if(verticalMovement > 0.2f && this.grounded && !this.jump){
            this.jump = true;
        }
        if(this.grounded){
            this.anim.SetBool("isTouchingFloor", true);
        } else {
            this.anim.SetBool("isTouchingFloor", false);
        }
    }

    void FixedUpdate()
    {
        float horizontalMovement = 0.0f;
        if(Application.platform == RuntimePlatform.Android) {
            horizontalMovement = variableJoystick.Horizontal;
        } else {
            horizontalMovement = Input.GetAxis("Horizontal");
        };
        if(horizontalMovement > 0 && !facingRight) {
            this.facingRight = true;
            this.anim.SetInteger("direction", 1);
        } else if (horizontalMovement < 0 && facingRight) {
            this.facingRight = false;
            this.anim.SetInteger("direction", -1);
        }
        if(horizontalMovement != 0 && grounded) {
            this.anim.SetBool("isWalking", true);
            SoundController.Instance.playEffect("walk");
        } else {
            this.anim.SetBool("isWalking", false);
        }
        
        transform.position += new Vector3(horizontalMovement, 0) * Time.deltaTime * SPEED;
        
        if(jump) {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            SoundController.Instance.playEffect("jump");
        }

        if(damageTimer > 0.0f && (Time.fixedTime - damageTimer) > damageDuration) {
            this.anim.SetBool("isDamaged", false);
            this.damageTimer = 0.0f;
        }
        if (Input.GetKeyDown("space")){
             this.anim.SetTrigger("attack");
             SoundController.Instance.playEffect("attack");
        }
    }

    public void updateCoins(int i){
        this.coins += i;
        UIController.Instance.setCoins(this.coins);
    }

    public void updateKeys(int i){
        this.keys += i;
        UIController.Instance.setKeys(this.keys);
    }

    public void updateLife(int i){
        this.life += i;
        if(this.life <= 0){
            this.life = 0;
            GameController.Instance.Die();
        }
        UIController.Instance.setLife(this.life);
    }

    public int getKeysCount(){
        return this.keys;
    }

    public void getWeapon(){
        weapon.SetActive(true);
    }
/*
    public void isAttachAnimationPlaying(){
        this.anim.
    }*/

    void OnCollisionEnter2D (Collision2D other) {
        if(other.collider.gameObject.CompareTag("Mob")){
            BlorkController blorkScript = (BlorkController) other.collider.GetComponent(typeof(BlorkController));
            if(blorkScript && !blorkScript.GetIsDead()) {
                float verticalRepelForce = grounded ? repelForce : 0f;
                if(facingRight){
                    rb2d.AddForce(new Vector2(-repelForce,verticalRepelForce));
                } else {
                    rb2d.AddForce(new Vector2(repelForce,verticalRepelForce));
                }
                this.anim.SetBool("isDamaged", true);
                SoundController.Instance.playEffect("hit");
                this.updateLife(-1);
                /*this.anim.Play("Damage");*/
                this.damageTimer = Time.fixedTime;
                //
                //anim.SetFloat("Speed", Mathf.Abs(h));
            }
        }
    }
}
