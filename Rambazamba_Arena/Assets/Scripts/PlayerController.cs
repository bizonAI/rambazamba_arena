using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public GameObject meleeRightAttacker;
    public GameObject meleeLeftAttacker;

    public GameObject itemHolderRight;
    public GameObject itemHolderLeft;

    GameObject currentItem;

    Animator anim;

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

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        }
    }

    void EquipNewItem()
    {
        currentItem = Instantiate(sceneItem.Item, itemHolderRight.transform.position, itemHolderRight.transform.rotation);
        currentItem.transform.parent = itemHolderRight.transform;
        Destroy(sceneItem.gameObject);
        //currentItem.transform.position = new Vector3(0, 0, 0);

        //Get Stats From Item

        //attack value
        //attack speed
        //is holding Weapon or fists

        Item item = currentItem.GetComponent<Item>();

        hasMeleeWeapon = true;

        anim.SetBool("hasMeleeWeapon", item.isStaffWeapon);

        anim.speed = 1;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Ground")
        {
            isGrounded = true;
        }

        if(other.collider.tag == "Item")
        {
            sceneItem = other.gameObject.GetComponent<SceneItem>();
            EquipNewItem();
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

            Vector3 inputMovementOfl = new Vector3(xMove, 0, yMove);

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("hasMeleeWeapon", hasMeleeWeapon);

            if (!hasMeleeWeapon)
            {
                if (!anim.GetCurrentAnimatorStateInfo(1).IsName("BoxingRight") && !rootMotionAnimationIsPlaying)
                {
                    anim.SetTrigger("meleeAttack");
                }
            }

            if (hasMeleeWeapon)
            {
                if (!anim.GetCurrentAnimatorStateInfo(1).IsName("MeleeWeaponAttack") && !rootMotionAnimationIsPlaying)
                {
                    anim.SetTrigger("weaponMeleeAttack");
                }
            }
            
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
