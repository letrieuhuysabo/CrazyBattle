using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSkillAttacktion : MonoBehaviour
{
    protected Transform owner;
    protected HashSet<Transform> allies;
    protected float damage;

    public Transform Owner { get => owner; set => owner = value; }
    public HashSet<Transform> Allies { get => allies; set => allies = value; }
    public float Damage { get => damage; set => damage = value; }
    public HashSet<Transform> HitedTrans { get => hitedTrans; set => hitedTrans = value; }

    protected HashSet<Transform> hitedTrans;

    void Awake()
    {
        hitedTrans = new();
        InitAwake();
    }
    protected virtual void InitAwake() { }
    void Start()
    {
        InitStart();
    }
    protected virtual void InitStart() { }
    protected bool IsOwner(Transform hurtBox)
    {
        return hurtBox.parent == owner;
    }
    protected bool AlreadyHitThisTransform(Transform hurtBox)
    {
        // Debug.Log(hitedTrans);
        return hitedTrans.Contains(hurtBox);
    }
    protected bool IsAlly(Transform hurtBox)
    {
        try
        {
            return allies.Contains(hurtBox.parent);
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }
}
