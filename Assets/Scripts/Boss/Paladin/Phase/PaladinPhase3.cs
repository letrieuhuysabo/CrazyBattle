using UnityEngine;

public class PaladinPhase3 : BossPhase
{
    public override int RandomSkill()
    {
        return Random.Range(0, 5);
    }
}
