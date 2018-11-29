using UnityEngine;

public class SceneItem : MonoBehaviour
{
    public GameObject[] Item;

    public enum WeaponType
    {
        staffWeapon,
        fistWeapon,
        carryWeapon       
    }

    public WeaponType weapon;
}
