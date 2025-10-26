using UnityEngine;

public class PaladinPhase1 : BossPhase
{
    public override int RandomSkill()
    {
        return Random.Range(0, 5);
    }
}
