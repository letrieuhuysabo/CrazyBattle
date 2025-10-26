using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword_2_AttackController", menuName = "Scriptable Objects/Weapon/AttackController/Sword_2")]
public class Sword2AttackController : WeaponAttackController
{
    [SerializeField] GameObject slashPrefab;
    [SerializeField] float lifeTime;
    
    void TaoSlash(Vector3 pos, Vector3 scale, Transform myTransform, HashSet<Transform> allies)
    {
        GameObject slash = Instantiate(slashPrefab);
        // set up transform của chủ nhân và tạo sát thương
        Sword2Attacktion weaponAttacktion = slash.GetComponent<Sword2Attacktion>();
        SetupOwner(myTransform, weaponAttacktion);
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        SetUpAllies(allies, weaponAttacktion);
        // set up vị trí
        slash.transform.position = pos;
        slash.transform.localScale = scale;
        // Debug.Log(pos);
        Destroy(slash, lifeTime);
    }
    async public override void AttackAtLand(Transform myTransform, HashSet<Transform> allies)
    {
        
        Vector3 startPos = myTransform.position;
        List<Vector3> pos = new()
        {
            new Vector3(1.5f, 0, 0),
            new Vector3(5f, 0, 0),
            new Vector3(8.5f, 0, 0)
        };

        if (myTransform.GetComponent<PlayerMovement>().FacingRight)
        {
            for (int i = 0; i < 3; i++)
            {
                TaoSlash(startPos + pos[i], new Vector3(1, 1, 1), myTransform, allies);
                await Task.Delay(200);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                TaoSlash(startPos + (-1 * pos[i]), new Vector3(-1, 1, 1), myTransform, allies);
                await Task.Delay(200);
            }
        }
        CheckBroken(weaponProperties.DurabilityLostPerLandAttack);
    }

    public override void AttackInAir(Transform myTransform, HashSet<Transform> allies)
    {
        Vector3 startPos = myTransform.position;
        List<Vector3> pos = new()
        {
            new Vector3(0, 0, 0),
            new Vector3(2, 2, 0),
            new Vector3(2, -2, 0),
            new Vector3(-2,2,0),
            new Vector3(-2,-2,0)
        };
        if (myTransform.GetComponent<PlayerMovement>().FacingRight)
        {
            for (int i = 0; i < 5; i++)
            {
                TaoSlash(startPos + pos[i], new Vector3(1, 1, 1), myTransform, allies);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                TaoSlash(startPos + pos[i], new Vector3(-1, 1, 1), myTransform, allies);
            }
        }
        CheckBroken(weaponProperties.DurabilityLostPerAirAttack);
    }

    public override void SetUpDatas(){}
}
