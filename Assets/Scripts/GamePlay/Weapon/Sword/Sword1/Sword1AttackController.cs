
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword_1_AttackController", menuName = "Scriptable Objects/Weapon/AttackController/Sword_1")]
public class Sword1AttackController : WeaponAttackController
{
    [SerializeField] GameObject slashPrefab;
    [SerializeField] float lifeTime;
    [SerializeField] float speedOfSlash;
    public override void AttackAtLand(Transform myTransform, HashSet<Transform> allies)
    {
        
        GameObject slash = Instantiate(slashPrefab);
        // set up transform của chủ nhân cho slash
        Sword1Attacktion weaponAttacktion = slash.GetComponent<Sword1Attacktion>();
        SetupOwner(myTransform, weaponAttacktion);
        // Debug.Log(allies.Count);
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        SetUpAllies(allies, weaponAttacktion);
        // set up hướng di chuyển
        Sword1Attacktion sword1Attacktion = slash.GetComponent<Sword1Attacktion>();
        if (myTransform.GetComponent<PlayerMovement>().FacingRight)
        {
            sword1Attacktion.Direction = Vector3.right;
            slash.transform.position = myTransform.position + new Vector3(0.8f, 0.3f, 0);
        }
        else
        {
            sword1Attacktion.Direction = Vector3.left;
            slash.transform.position = myTransform.position + new Vector3(-0.8f, 0.3f, 0);
        }
        // setup tốc độ
        weaponAttacktion.Speed = speedOfSlash;
        Destroy(slash, lifeTime);
        CheckBroken(weaponProperties.DurabilityLostPerLandAttack);
    }

    public override void AttackInAir(Transform myTransform, HashSet<Transform> allies)
    {

        for (int i = 0; i < 2; i++)
        {
            GameObject slash = Instantiate(slashPrefab);

            // set up transform của chủ nhân cho slash
            WeaponAttacktion weaponAttacktion = slash.GetComponent<WeaponAttacktion>();
            SetupOwner(myTransform, weaponAttacktion);
            SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
            SetUpAllies(allies, weaponAttacktion);
            // set up hướng di chuyển
            Sword1Attacktion sword1Attacktion = slash.GetComponent<Sword1Attacktion>();
            if (i == 0)
            {
                sword1Attacktion.Direction = new Vector3(0.75f, -1, 0).normalized;
                slash.transform.position = myTransform.position + new Vector3(0.4f, 0.5f, 0);
            }
            else
            {
                sword1Attacktion.Direction = new Vector3(-0.75f, -1, 0).normalized;
                slash.transform.position = myTransform.position + new Vector3(-0.4f, 0.5f, 0);
            }
            // setup tốc độ
            sword1Attacktion.Speed = speedOfSlash;
            Destroy(slash, lifeTime);
        }
        CheckBroken(weaponProperties.DurabilityLostPerAirAttack);
    }

    public override void SetUpDatas(){}
}
