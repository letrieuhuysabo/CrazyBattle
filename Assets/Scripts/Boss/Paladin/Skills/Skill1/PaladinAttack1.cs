using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinAttack1 : BossAttack
{
    [SerializeField] float timeJump, jumpForce, timeSkill;
    [SerializeField] GameObject skillEffect1_1Prefab, skillEffect1_2Prefab;
    [SerializeField] AudioClip skillSound, jumpSound, landingSound;

    public override void Attack()
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(AttackCoroutine()));
    }
    IEnumerator AttackCoroutine()
    {
        // đổi animation
        bossAnimator.ChangeAnim("Jump");
        yield return new WaitForSeconds(0.5f);
        // thực hiện nhảy đến góc màn hình
        Vector3 startPos = transform.position;

        if (startPos.x > 0) // nhảy sang góc trái
        {
            BossHurtBox.instance.AddToObserver(StartCoroutine(NhayDenGoc(startPos, -12)));
        }
        else // nhảy sang góc phải
        {
            BossHurtBox.instance.AddToObserver(StartCoroutine(NhayDenGoc(startPos, 12)));
        }
    }
    IEnumerator NhayDenGoc(Vector3 startPos, float target)
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(NhayLenTren(startPos)));
        float duration = timeJump;
        while (duration > 0)
        {
            bossTransform.position = new Vector3(Mathf.Lerp(target, startPos.x, duration / timeJump), bossTransform.position.y, 0);
            duration -= Time.deltaTime;
            yield return null;
        }
        bossTransform.position = new Vector3(target, startPos.y, 0);
        if (bossTransform.position.x < 0)
        {
            bossTransform.localScale = new Vector3(Mathf.Abs(bossTransform.localScale.x), bossTransform.localScale.y, 0);
        }
        else
        {
            bossTransform.localScale = new Vector3(-Mathf.Abs(bossTransform.localScale.x), bossTransform.localScale.y, 0);
        }

    }
    IEnumerator NhayLenTren(Vector3 startPos)
    {
        // nhảy lên
        SoundSource.instance.PlaySound(jumpSound);
        float duration = 0;
        while (duration < timeJump / 2)
        {
            bossTransform.position = new Vector3(bossTransform.position.x, Mathf.Lerp(startPos.y, jumpForce + startPos.y, duration / (timeJump / 2)), 0);
            duration += Time.deltaTime;
            yield return null;
        }
        // rơi xuống
        duration = timeJump / 2;
        while (duration > 0)
        {
            bossTransform.position = new Vector3(bossTransform.position.x, Mathf.Lerp(startPos.y, jumpForce + startPos.y, duration / (timeJump / 2)), 0);
            duration -= Time.deltaTime;
            yield return null;
        }
        SoundSource.instance.PlaySound(landingSound);
        bossAnimator.ChangeAnim("FinishJump");
        bossTransform.position = new Vector3(bossTransform.position.x, startPos.y, 0);
        BossHurtBox.instance.AddToObserver(StartCoroutine(DoSkill()));
    }
    IEnumerator DoSkill()
    {
        Vector3 posTarget;
        Vector3 directionOfEffect;
        if (bossTransform.position.x < 0) // lướt từ trái sang
        {
            posTarget = new Vector3(12, bossTransform.position.y, 0);
            directionOfEffect = new Vector3(0, 0, 0);
        }
        else
        {
            posTarget = new Vector3(-12, bossTransform.position.y, 0);
            directionOfEffect = new Vector3(0, 180, 0);
        }
        yield return null;
        bossAnimator.ChangeAnim("Attack_1");
        
        yield return new WaitForSeconds(1f);
        // tạo hiệu ứng vùng sét đi theo
        GameObject skillEffect1_1 = Instantiate(skillEffect1_1Prefab);
        skillEffect1_1.transform.position = bossTransform.position + new Vector3(0, 1, 0);
        skillEffect1_1.SetActive(true);
        SetDamage(skillEffect1_1.GetComponent<BossAttacktion>());
        BossHurtBox.instance.AddToObserver(skillEffect1_1);
        // phát âm thanh
        SoundSource.instance.PlaySound(skillSound);
        yield return new WaitForSeconds(1.5f);
        float duration = timeSkill;
        Vector3 startPos = bossTransform.position;
        // tạo vệt khói khi lướt
        GameObject skillEffect1_2 = Instantiate(skillEffect1_2Prefab);
        skillEffect1_2.SetActive(true);
        BossHurtBox.instance.AddToObserver(skillEffect1_2);
        // xoay vùng sét
        skillEffect1_1.transform.GetChild(0).eulerAngles = directionOfEffect;
        // lướt
        while (duration > 0)
        {
            // di chuyển boss
            bossTransform.position = Vector3.Lerp(posTarget, startPos, duration / timeSkill);
            // di chuyển vùng sét
            skillEffect1_1.transform.position = bossTransform.position + new Vector3(bossTransform.localScale.normalized.x * 2.45f, 1, 0);

            // di chuyển vệt khói
            skillEffect1_2.transform.position = bossTransform.position;
            duration -= Time.deltaTime;
            yield return null;
        }
        bossAnimator.ChangeAnim("FinishAttack1");
        PlayAndStopParticle.StopParticle(skillEffect1_1.transform);
        PlayAndStopParticle.StopParticle(skillEffect1_2.transform);
        Destroy(skillEffect1_1,2f);
        Destroy(skillEffect1_2,2f);
        yield return new WaitForSeconds(1);
        bossTransform.localScale = new Vector3(-bossTransform.localScale.x, bossTransform.localScale.y, 0);
        PaladinController.instance.ReadyAttack(1);
    }
}
