using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinHurtBox : BossHurtBox
{
    Renderer bossRenderer;
    public override void ChangeAnimToDead()
    {
        BossAnimator.instance.ChangeAnim("Death");
        StartCoroutine(EndGameCoroutine());
    }
    IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(2);
        GameOverController.instance.LostAllHearth();

        // ChangeScene.instance.ChangeToScene(0);
    }

    public override async void ChangeAnimToHurt()
    {
        bossRenderer.material.color = Color.red;
        await WaitTask.WaitForSeconds(0.2f);
        bossRenderer.material.color = Color.white;
    }
    
    public override void Init()
    {
        bossRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }
    void Awake()
    {
        instance = this;
    }

    public override void ChangeForm()
    {
        if (hp <= maxHp * (1 / 3f))
        {
            PaladinAttack6.instance.State = new PaladinAttack6State3();
        }
        else if (hp <= maxHp * (2 / 3f))
        {
            PaladinAttack6.instance.State = new PaladinAttack6State2();
        }
        
    }
}
