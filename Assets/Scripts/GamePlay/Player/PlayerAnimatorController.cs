using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeAnim(string nameOfAnim, bool n)
    {
        anim.SetBool(nameOfAnim, n);
    }
    public void ChangeAnim(string nameOfAnim)
    {
        anim.SetTrigger(nameOfAnim);
    }
}
