using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerPickWeapon : MonoBehaviour
{
    protected HashSet<Transform> weapons;
    PlayerAttackController playerAttackController;

    public HashSet<Transform> Weapons { get => weapons; set => weapons = value; }

    void Start()
    {
        Weapons = new();
        playerAttackController = transform.parent.GetComponent<PlayerAttackController>();
        playerAttackController.PlayerPickWeapon = this;
        
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponContainer"))
        {
            Weapons.Add(collision.transform);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponContainer"))
        {
            if (Weapons.Contains(collision.transform))
            {
                Weapons.Remove(collision.transform);
            }
        }
    }
    async protected void PickToRightHand()
    {
        Transform weaponTransform = FindNearestWeapon();
        if (weaponTransform != null)
        {
            // update thuộc tính
            WeaponContainer weaponContainer = weaponTransform.GetComponent<WeaponContainer>();
            await Task.Delay(50);
            // mang vào tay
            playerAttackController.BringRightWeapon(weaponContainer.Weapon);
            // trả weaponContainer về pool
            weaponContainer.ReturnToPool();
            // cập nhật số lần nhặt vũ khí vào Data
            UpdatePickWeaponTimeToData();
        }


    }
    async protected void PickToLeftHand()
    {
        Transform weaponTransform = FindNearestWeapon();
        if (weaponTransform != null)
        {
            // update thuộc tính
            WeaponContainer weaponContainer = weaponTransform.GetComponent<WeaponContainer>();
            await Task.Delay(50);
            // mang vào tay
            playerAttackController.BringLeftWeapon(weaponContainer.Weapon);
            // trả weaponContainer về pool
            weaponContainer.ReturnToPool();
            // cập nhật số lần nhặt vũ khí vào Data
            UpdatePickWeaponTimeToData();
        }
    }
    Transform FindNearestWeapon()
    {
        Transform minDisTran = null;
        foreach (Transform t in Weapons)
        {
            if (minDisTran == null || Vector3.Distance(t.position, transform.position) < Vector3.Distance(minDisTran.position, transform.position))
            {
                minDisTran = t;
            }
        }
        // minDis là transform của vũ khí gần nhân vật nhất
        return minDisTran;
    }
    protected abstract void UpdatePickWeaponTimeToData();
}
