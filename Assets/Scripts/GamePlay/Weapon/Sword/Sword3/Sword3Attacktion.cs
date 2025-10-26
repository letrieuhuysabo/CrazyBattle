using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword3Attacktion : WeaponAttacktion
{
    float delayTime;
    float absorbForce;
    public float DelayTime { get => delayTime; set => delayTime = value; }
    public float AbsorbForce { get => absorbForce; set => absorbForce = value; }

    HashSet<Transform> inRangeTransforms;
    void Awake()
    {
        inRangeTransforms = new();
    }

    [System.Obsolete]
    void Start()
    {
        StartCoroutine(AbsorbCoroutine());
    }

    [System.Obsolete]
    IEnumerator AbsorbCoroutine()
    {
        
        while (true)
        {
            // gây sát thương cho các player trong vùng
            foreach (Transform tran in inRangeTransforms)
            {
                if (!IsOwner(tran) && !IsAlly(tran))
                {
                    tran.Find("HurtBox").GetComponent<PlayerHurtBoxController>().Damaged(damage, tran.position);
                }

            }
            // tạo lực hút
            // tạo tham chiếu đến các WeaponAttacktion
            WeaponAttacktion[] weaponAttacktions = FindObjectsOfType<WeaponAttacktion>();
            // hút các weaponAttacktion đó lại
            foreach (WeaponAttacktion weaponAttacktion in weaponAttacktions)
            {
                GameObject attacktionGameObj = weaponAttacktion.gameObject;
                StartCoroutine(AbsorbGameObjectCoroutine(attacktionGameObj));
            }
            // hút player lại
            PlayerMovement[] playerMovements = FindObjectsOfType<PlayerMovement>();
            // Debug.Log("hello2");
            foreach (PlayerMovement playerMovement in playerMovements)
            {
                StartCoroutine(AbsorbGameObjectCoroutine(playerMovement.gameObject));
            }
            // Debug.Log("hello");
            yield return new WaitForSeconds(delayTime);
        }
    }
    IEnumerator AbsorbGameObjectCoroutine(GameObject gameObjectBeAbsorbed)
    {
        float duration = 0.2f;
        while (duration > 0)
        {
            // Debug.Log(gameObjectBeAbsorbed.name);
            if (gameObjectBeAbsorbed != null)
            {
                Vector3 direction = transform.position - gameObjectBeAbsorbed.transform.position;
                gameObjectBeAbsorbed.transform.position += direction.normalized * absorbForce * Time.deltaTime;
            }
            
            duration -= Time.deltaTime;
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !IsOwner(collision.transform))
        {
            inRangeTransforms.Add(collision.transform.parent);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && inRangeTransforms.Contains(collision.transform.parent))
        {
            inRangeTransforms.Remove(collision.transform.parent);
        }
    }
}
