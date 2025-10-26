using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorController))]
public abstract class PlayerAttackController : MonoBehaviour
{
    Weapon rightWeaponAttackController, leftWeaponAttackController;
    [SerializeField] Transform rightHand, leftHand;
    public Transform MyTransform { get => myTransform; set => myTransform = value; }
    public Weapon RightWeaponAttackController { get => rightWeaponAttackController; set => rightWeaponAttackController = value; }
    public Weapon LeftWeaponAttackController { get => leftWeaponAttackController; set => leftWeaponAttackController = value; }
    public PlayerPickWeapon PlayerPickWeapon { get => playerPickWeapon; set => playerPickWeapon = value; }
    public float CdAttackRight { get => cdAttackRight; set => cdAttackRight = value; }
    public float CdAttackLeft { get => cdAttackLeft; set => cdAttackLeft = value; }
    public HashSet<Transform> Allies { get => allies; set => allies = value; }

    protected PlayerPickWeapon playerPickWeapon;

    PlayerProperties playerProperties;
    float cdAttackRight, cdAttackLeft;
    PlayerAnimatorController anim;
    protected Transform myTransform, enemyTransform;
    protected HashSet<Transform> allies;
    PlayerInput playerInput;
    void Awake()
    {
        anim = GetComponent<PlayerAnimatorController>();
        playerProperties = GetComponent<PlayerProperties>();
        cdAttackRight = 1;
        cdAttackLeft = 1;
        myTransform = transform;
        allies = new();
        playerInput = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(playerInput.keyWeaponRight))
        {
            if (Input.GetKey(playerInput.keyDown))
            {
                if (playerPickWeapon.Weapons.Count == 0)
                {
                    RightAttack();
                }
            }
            else
            {
                RightAttack();
            }
            
        }
        else if (Input.GetKeyDown(playerInput.keyWeaponLeft))
        {
            if (Input.GetKey(playerInput.keyDown))
            {
                if (playerPickWeapon.Weapons.Count == 0)
                {
                    LeftAttack();
                }
            }
            else
            {
                LeftAttack();
            }
        }
    }
    async protected void RightAttack()
    {
        Debug.Log(rightWeaponAttackController.Durability);
        // Debug.Log(cdAttackRight);
        if (cdAttackRight <= 0 && RightWeaponAttackController != null)
        {
            // Debug.Log(allies.Count);
            // Debug.Log(anim);
            // đổi animation
            anim.ChangeAnim("2_Attack");
            // tấn công
            RightWeaponAttackController.Attack(myTransform, allies);
            // cd
            try
            {
                cdAttackRight = RightWeaponAttackController.AttackSpeed;
                await WaitTask.WaitForSeconds(cdAttackRight - cdAttackRight * playerProperties.CdReducePercent);
            }
            catch (NullReferenceException) { }
            cdAttackRight = 0;
        }

    }
    async protected void LeftAttack()
    {
        if (cdAttackLeft <= 0 && leftWeaponAttackController != null)
        {
            // Debug.Log(anim);
            // đổi animation
            anim.ChangeAnim("2_Attack");
            // tấn công
            leftWeaponAttackController.Attack(myTransform, allies);
            // cd
            try
            {
                cdAttackLeft = leftWeaponAttackController.AttackSpeed;
                await WaitTask.WaitForSeconds(cdAttackLeft - cdAttackLeft*playerProperties.CdReducePercent);
            }
            catch (NullReferenceException) { }
            cdAttackLeft = 0;
        }

    }
    public void BringRightWeapon(Weapon weaponNeedBring)
    {
        // bỏ vũ khí hiện tại
        ThrowRightWeapon();
        // hiện vũ khí lên tay
        GameObject weapon = new()
        {
            name = "Weapon"
        };
        weapon.transform.SetParent(rightHand.transform, true);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.AddComponent<SpriteRenderer>().sprite = weaponNeedBring.WeaponSprite;
        // setup data cho vũ khí
        weaponNeedBring.AttackController.SetUpDatas();
        // báo cho weapon đó đang nằm trên tay này
        weaponNeedBring.PlayerAttackController = this;
        // set up đòn đánh
        rightWeaponAttackController = weaponNeedBring;
        // reset cd
        cdAttackRight = 0;
        // hiện thông tin vũ khí trên UI
        ShowRightWeaponUI(weaponNeedBring);
        Debug.Log("Start: " + weaponNeedBring.Durability);
    }
    public void ThrowRightWeapon()
    {
        // tắt thông tin vũ khí trên UI
        RectTransform rightWeapon = transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().
                                    IconHead.transform.parent.Find("Weapons").Find("WeaponRight").GetComponent<RectTransform>();
        rightWeapon.gameObject.SetActive(false);
        
        cdAttackRight = 1;
        if (rightHand.transform.childCount > 0)
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);

        }
        rightWeaponAttackController = null;
    }
    public void BringLeftWeapon(Weapon weaponNeedBring)
    {
        // bỏ vũ khí hiện tại
        ThrowLeftWeapon();
        // hiện vũ khí lên tay
        GameObject weapon = new()
        {
            name = "Weapon"
        };
        weapon.transform.SetParent(leftHand.transform, true);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weapon.transform.localScale = new Vector3(1, 1, 1);
        SpriteRenderer weaponRenderer = weapon.AddComponent<SpriteRenderer>();
        weaponRenderer.sprite = weaponNeedBring.WeaponSprite;
        weaponRenderer.sortingOrder++;
        // setup data cho vũ khí
        weaponNeedBring.AttackController.SetUpDatas();
        // báo cho weapon đó đang nằm trên tay này
        weaponNeedBring.PlayerAttackController = this;
        // set up đòn đánh
        leftWeaponAttackController = weaponNeedBring;
        // reset cd
        cdAttackLeft = 0;
        // hiện thông tin vũ khí trên UI
        ShowLeftWeaponUI(weaponNeedBring);
    }
    public void ThrowLeftWeapon()
    {
        // tắt thông tin vũ khí trên UI
        RectTransform leftWeapon = transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().
                                    IconHead.transform.parent.Find("Weapons").Find("WeaponLeft").GetComponent<RectTransform>();
        leftWeapon.gameObject.SetActive(false);
        
        cdAttackLeft = 1;
        if (leftHand.transform.childCount > 0)
        {
            Destroy(leftHand.transform.GetChild(0).gameObject);
        }
        leftWeaponAttackController = null;
    }
    public void ThrowWeapon(Weapon weapon)
    {
        if (leftWeaponAttackController == weapon)
        {
            ThrowLeftWeapon();
        }
        else if (rightWeaponAttackController == weapon)
        {
            ThrowRightWeapon();
        }
    }
    public void UpdateDurability(Weapon weapon)
    {
        if (leftWeaponAttackController == weapon)
        {
            ShowLeftWeaponUI(weapon);
        }
        else if (rightWeaponAttackController == weapon)
        {
            ShowRightWeaponUI(weapon);
        }
    }
    public void ShowRightWeaponUI(Weapon weapon)
    {
        RectTransform rightWeapon = transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().
                                    IconHead.transform.parent.Find("Weapons").Find("WeaponRight").GetComponent<RectTransform>();
        rightWeapon.gameObject.SetActive(true);
        rightWeapon.GetComponent<Image>().sprite = weapon.WeaponSprite;
        rightWeapon.Find("DurabilityBar").Find("DurabilityFill").GetComponent<Image>().fillAmount = weapon.Durability / 100f;           
    }
    public void ShowLeftWeaponUI(Weapon weapon)
    {
        RectTransform leftWeapon = transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().
                                    IconHead.transform.parent.Find("Weapons").Find("WeaponLeft").GetComponent<RectTransform>();
        leftWeapon.gameObject.SetActive(true);
        leftWeapon.GetComponent<Image>().sprite = weapon.WeaponSprite;
        leftWeapon.Find("DurabilityBar").Find("DurabilityFill").GetComponent<Image>().fillAmount = weapon.Durability / 100f;
    }
}
