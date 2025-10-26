using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinAttack6State2 : PaladinAttack6State
{
    public override int CountOfOrbs()
    {
        return 2;
    }
    public async override void CreateSKill(List<GameObject> orbs)
    {
        await WaitTask.WaitForSeconds(1.5f);
        try
        {
            Vector3 centerPos = orbs[0].transform.position;
            Vector3 targetPos0 = centerPos + new Vector3(-1.5f, -1.5f, 0);
            Vector3 targetPos1 = centerPos + new Vector3(1.5f, -1.5f, 0);
            float duration = 0f;
            while (duration < 1)
            {
                orbs[0].transform.position = Vector3.Lerp(centerPos, targetPos0, duration / 1f);
                orbs[1].transform.position = Vector3.Lerp(centerPos, targetPos1, duration / 1f);
                duration += Time.deltaTime;
                await Task.Yield();
            }
            orbs[0].transform.position = targetPos0;
            orbs[1].transform.position = targetPos1;
        }
        catch(MissingReferenceException){}

    }
}
