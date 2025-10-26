using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterSkillController : MonoBehaviour
{
    [SerializeField] float cd;
    protected Image cdBar;
    [SerializeField] string notes;
    public Image CdBar { get => cdBar; set => cdBar = value; }
    public string Notes { get => notes; set => notes = value; }
    public float Cd { get => cd; set => cd = value; }
    protected Transform playerTransform;
    void Start()
    {
        // tạo tham chiếu
        playerTransform = transform.parent.parent;

        Init();
    }
    protected virtual void Init(){}
    public abstract void Skill();
    public void CD()
    {

        StartCoroutine(CDCoroutine());
    }
    IEnumerator CDCoroutine()
    {
        cdBar.fillAmount = 0;
        DoiMauCDBar(Color.red);
        float duration = 0;
        while (duration < Cd)
        {

            duration += Time.deltaTime;
            cdBar.fillAmount = duration / Cd;
            yield return null;
        }
        cdBar.fillAmount = 1;
        DoiMauCDBar(Color.green);
    }
    public void UseSkill()
    {
        if (cdBar.fillAmount == 1)
        {
            Skill();
        }

    }
    protected void DoiMauCDBar(Color color)
    {
        cdBar.color = color;
    }
}
