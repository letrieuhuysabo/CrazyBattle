using UnityEngine;

public class PaladinPhase2 : BossPhase
{
    public override int RandomSkill()
    {
        return Random.Range(0, 4);
    }
}
