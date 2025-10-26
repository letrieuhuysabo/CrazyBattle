using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(BossAnimator))]
public class PaladinController : MonoBehaviour
{
    public static PaladinController instance;
    // BossAnimator bossController;
    BossPhase phase;
    int currentSkill = -1;
    void Awake()
    {
        instance = this;
        // bossController = GetComponent<BossAnimator>();
        phase = new PaladinPhase1();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadyAttack(3);
    }

    public async void ReadyAttack(float delay)
    {
        await WaitTask.WaitForSeconds(delay);
        int rand;
        do
        {
            rand = phase.RandomSkill();
        }
        while (rand == currentSkill);
        currentSkill = rand;
        rand = 3;
        try
        {
            transform.Find("Attacks").GetChild(rand).GetComponent<BossAttack>().Attack();
        }
        catch(MissingReferenceException){}
    }
}
