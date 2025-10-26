using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    private float hp;
    private float defensePercent;
    private float attackPercent;
    private float moveSpeed;
    private float jumpForce;
    private float cdReducePercent;
    private float immortalTime;
    public float CdReducePercent { get => cdReducePercent; set => cdReducePercent = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float AttackPercent { get => attackPercent; set => attackPercent = value; }
    public float DefensePercent { get => defensePercent; set => defensePercent = value; }
    public float Hp { get => hp; set => hp = value; }
    public float ImmortalTime { get => immortalTime; set => immortalTime = value; }

    public void SetupDatas(float hp, float defensePercent, float attackPercent, float moveSpeed, float jumpForce, float cdReducePercent, float immortalTime)
    {
        this.hp = hp;
        this.defensePercent = defensePercent;
        this.attackPercent = attackPercent;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.cdReducePercent = cdReducePercent;
        this.immortalTime = immortalTime;
    }
}
