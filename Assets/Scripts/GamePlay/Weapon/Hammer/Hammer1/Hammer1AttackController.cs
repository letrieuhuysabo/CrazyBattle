using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Hammer_1_AttackController", menuName = "Scriptable Objects/Weapon/AttackController/Hammer_1")]
public class Hammer1AttackController : WeaponAttackController
{
    [SerializeField] GameObject airAttackPrefab, landBuffEffectPrefab;
    [SerializeField] float maxScaleOfSkill, multipliedDamagePerLandAttack, ratioScaleOfMultiplyDamage, ratioScaleOfVelocityY;
    float multiplyDamage;
    public override void AttackAtLand(Transform myTransform, HashSet<Transform> allies)
    {
        multiplyDamage *= multipliedDamagePerLandAttack;
        GameObject landEffect = Instantiate(landBuffEffectPrefab);
        landEffect.transform.position = myTransform.position;
        Destroy(landEffect, 1);
        CheckBroken(weaponProperties.DurabilityLostPerLandAttack);
    }

    public override void AttackInAir(Transform myTransform, HashSet<Transform> allies)
    {
        Rigidbody2D rb = myTransform.GetComponent<Rigidbody2D>();
        if ((-rb.linearVelocityY / ratioScaleOfVelocityY) > 1)
        {
            multiplyDamage *= -rb.linearVelocityY / ratioScaleOfVelocityY;
        }
        GameObject airAttack = Instantiate(airAttackPrefab);
        // set up vị trí
        if (myTransform.GetComponent<PlayerMovement>().FacingRight)
        {

            airAttack.transform.position = myTransform.position + new Vector3(0.8f, -0.1f, 0);
        }
        else
        {

            airAttack.transform.position = myTransform.position + new Vector3(-0.8f, -0.1f, 0);
        }
        // set up kích thước và hiệu ứng theo sát thương gây ra
        float realScale;
        if (multiplyDamage > ratioScaleOfMultiplyDamage)
        {
            // scale
            airAttack.transform.localScale = new Vector3(multiplyDamage / ratioScaleOfMultiplyDamage, multiplyDamage / ratioScaleOfMultiplyDamage, 0);
            realScale = multiplyDamage / ratioScaleOfMultiplyDamage;
            if (realScale > maxScaleOfSkill)
            {
                airAttack.transform.localScale = new Vector3(maxScaleOfSkill, maxScaleOfSkill, 0);
                realScale = maxScaleOfSkill;
            }

            // particle system
            for (int i = 2; i <= 3; i++)
            {
                var emission = airAttack.transform.GetChild(i).GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = Mathf.Lerp(50, 1000, (realScale-1) / (maxScaleOfSkill-1));
            }
            var main4 = airAttack.transform.GetChild(4).GetComponent<ParticleSystem>().main;
            main4.startSize = Mathf.Lerp(0.05f, 1f, (realScale-1) / (maxScaleOfSkill - 1));
        }

        // set up transform của chủ nhân cho slash
        Hammer1Attacktion hammer1Attacktion = airAttack.GetComponent<Hammer1Attacktion>();
        SetupOwner(myTransform, hammer1Attacktion);
        SetupDamage((weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent) * multiplyDamage, hammer1Attacktion);
        SetUpAllies(allies, hammer1Attacktion);
        multiplyDamage = 1;
        PlayAndStopParticle.StopParticle(airAttack.transform,0.03f);
        Destroy(airAttack, 1);
        CheckBroken(weaponProperties.DurabilityLostPerAirAttack);
    }

    public override void SetUpDatas()
    {
        multiplyDamage = 1;
    }
}
