using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public GameObject leftItemHolder;
    public GameObject rightItemHolder;

    Animator anim;
    SceneItem sceneItem;

    GameObject leftItem;
    GameObject rightItem;

    public enum WeaponType
    {
        fist,
        staff,
        carry
    }

    public WeaponType weaponType;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void SetWeaponType()
    {
        if (rightItem != null)
            Destroy(rightItem);

        if (leftItem != null)
            Destroy(leftItem);

        if (rightItem == null)
        {
            rightItem = Instantiate(sceneItem.Item[0], rightItemHolder.transform.position, rightItemHolder.transform.rotation);
            rightItem.transform.parent = rightItemHolder.transform;
        }
        

        if (sceneItem.weapon == SceneItem.WeaponType.fistWeapon)
        {
            anim.SetBool("hasMeleeWeapon", false);
            anim.SetBool("carryItem", false);
            weaponType = WeaponType.fist;
        }

        if (sceneItem.weapon == SceneItem.WeaponType.staffWeapon)
        {
            anim.SetBool("hasMeleeWeapon", true);
            anim.SetBool("carryItem", false);
            weaponType = WeaponType.staff;
        }

        if (sceneItem.weapon == SceneItem.WeaponType.carryWeapon)
        {
            anim.SetBool("hasMeleeWeapon", false);
            anim.SetBool("carryItem", true);
            weaponType = WeaponType.carry;
        }

        EquipItem();

        Destroy(sceneItem.gameObject);
    }

    void EquipItem()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Item")
        {
            

            sceneItem = other.gameObject.GetComponent<SceneItem>();
            SetWeaponType();
        }
    }
}
