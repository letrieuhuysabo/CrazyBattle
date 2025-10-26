using UnityEngine;

[CreateAssetMenu(fileName = "RootContainer", menuName = "Scriptable Objects/RootContainer")]
public class RootContainer : ScriptableObject
{
    [SerializeField] private Transform root;
    
    public Transform Root { get => root; set => root = value; }
    public float hp, defensePercent, attackPercent, moveSpeed, jumpForce, cdReducePercent, immortalTime;

}
