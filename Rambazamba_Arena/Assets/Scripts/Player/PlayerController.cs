using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public float jumpForce = 2.0f;
    public float health = 100.0f;

    public GameObject meleeRightAttacker;
    public GameObject meleeLeftAttacker;

    public GameObject itemHolderRight;
    public GameObject itemHolderLeft;

    //Weapons
    GameObject currentStaffWeapon;
    GameObject currentFistWeaponLeft;
    GameObject currenFistWeaponRight;
    GameObject currentCarryWeapon;

    Animator anim;
    Rigidbody rigid;

    float xMovement;
    float yMovement;
    float playerSpeed;

    float airCounter;

    bool rootMotionAnimationIsPlaying;
    bool isWalking;

    //DEBUG PUBLIC
    public bool hasMeleeWeapon;

    bool isGrounded;
    public float groundlessTime = 0.5f;

    SceneItem sceneItem;
    Item item;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void GroundedChecker()
    {
        if (!isGrounded)
        {
            airCounter += Time.deltaTime;
            anim.SetFloat("fallCounter", airCounter);
            anim.SetBool("isGrounded", isGrounded);
        }
        else
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("fallCounter", 0);
        }
    }

    void GetHit(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MeleeAttacker") && /*other.gameObject != meleeRightAttacker*/ !other.gameObject.transform.IsChildOf(gameObject.transform))
        {
            anim.SetTrigger("gotHit");
            GetHit(other.GetComponent<Attacker>().damage);
            Debug.Log("hit");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Ground")
        {
            isGrounded = false;
            airCounter = 0;
            anim.SetFloat("fallCounter", airCounter);
        }
    }

    private void Update()
    {
        //GOT HIT DEBUG
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("gotHit");
        }

        GroundedChecker();
        Attack();
    }

    

    private void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        if (!rootMotionAnimationIsPlaying)
        {
            Debug.Log("Can move player");

            playerSpeed = Mathf.Clamp(Mathf.Abs(xMove) + Mathf.Abs(yMove), 0, 1);
            anim.SetFloat("speedPercent", playerSpeed);

            if (playerSpeed > 0)
            {
                anim.applyRootMotion = false;
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
                transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
            } else if (playerSpeed == 0)
            {
                anim.applyRootMotion = true;
                isWalking = false;
                anim.SetBool("isWalking", isWalking);
            }

            Vector3 movement = new Vector3(xMove, 0.0f, yMove);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            }
        }
        else
        {
            playerSpeed = 0;
            anim.SetFloat("speedPercent", playerSpeed);
        }

        //JUMP 
        if(isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("jump");
        }
    }

    //Triggerd by animations
    void Jump()
    {
        rigid.AddForce(transform.up * jumpForce * 100);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!anim.GetCurrentAnimatorStateInfo(1).IsName("BoxingRight") && !rootMotionAnimationIsPlaying)
            {
                anim.SetTrigger("attack");
            }

            if (!anim.GetCurrentAnimatorStateInfo(1).IsName("MeleeWeaponAttack") && !rootMotionAnimationIsPlaying)
            {
                anim.SetTrigger("attack");
            }

            // if carry somthing 
            /*
            if ()
            {
                set animation to let something falling 
                spawn object at players position
            }
            */
        }
    }

    void RootMotionAnimationIsPlaying()
    {
        rootMotionAnimationIsPlaying = true;
        anim.applyRootMotion = true;
    }

    void RootMotionAnimationStops()
    {
        rootMotionAnimationIsPlaying = false;
    }

    // normal right Melee Attack 
    void StartRightAttack()
    {
        meleeRightAttacker.SetActive(true);
    }

    void StopRightAttack()
    {
        meleeRightAttacker.SetActive(false);
    }

    //
    void StartLeftAttack()
    {
        meleeLeftAttacker.SetActive(true);
    }

    void StopLeftAttack()
    {
        meleeLeftAttacker.SetActive(false);
    }
}
