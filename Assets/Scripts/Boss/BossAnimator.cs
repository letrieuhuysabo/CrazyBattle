using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimator : MonoBehaviour
{
    protected Animator anim;
    public static BossAnimator instance;
    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }
    public void ChangeAnim(string s)
    {
        anim.SetTrigger(s);
    }
    public void ChangeAnim(string s, bool b)
    {
        anim.SetBool(s, b);
    }
}
