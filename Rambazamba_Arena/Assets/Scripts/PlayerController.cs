using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public GameObject meleeRightAttacker;
    public GameObject meleeLeftAttacker;

    Animator anim;

    float xMovement;
    float yMovement;
    float playerSpeed;

    float airCounter;

    bool rootMotionAnimationIsPlaying;

    bool isGrounded;
    public float groundlessTime = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //GOT HIT DEBUG
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("gotHit");
        }


        if (!isGrounded)
        {
            airCounter += Time.deltaTime;

            if(airCounter >= groundlessTime)
            {
                anim.SetBool("isGrounded", isGrounded);
            }
        }
        else
        {
            anim.SetBool("isGrounded", isGrounded);
            airCounter = 0;
        }

        Attack();
        Walking();
    }

    void Walking()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            anim.applyRootMotion = false;
        }
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        if (!rootMotionAnimationIsPlaying)
        {
            playerSpeed = Mathf.Clamp(Mathf.Abs(xMove) + Mathf.Abs(yMove), 0, 1);
            anim.SetFloat("speedPercent", playerSpeed);

            Vector3 inputMovementOfl = new Vector3(xMove, 0, yMove);

            if (playerSpeed > 0)
            {
                transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
            }

            Vector3 movementOfl = new Vector3(xMove, 0.0f, yMove);

            if (movementOfl != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementOfl), 0.15F);
            }
        }
        else
        {
            playerSpeed = 0;
            anim.SetFloat("speedPercent", playerSpeed);
        }
    }

    void Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Boxing_Right"))
        {
            anim.applyRootMotion = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !rootMotionAnimationIsPlaying)
            {
                anim.SetTrigger("meleeAttack");
            }
        }
    }

    void RootMotionAnimationIsPlaying()
    {
        rootMotionAnimationIsPlaying = true;
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
