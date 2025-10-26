using System.Collections.Generic;
using UnityEngine;

public class Human4Skill : CharacterSkillController
{
    // mô tả chiêu thức:
    // Tạo 1 cổng khổng gian ở vị trí người chơi, cổng này tồn tại trong 20 giây (tôi đa tồn tại cùng lúc 2 cổng), cổng này cần 2 giây để hoạt động sau khi đc tạo
    // Khi bước vào cổng này, người chơi sẽ được dịch chuyển đến cổng còn lại, sau đó cả 2 cổng cần 3 giây để hoạt động lại
    [SerializeField] GameObject portalPrefab, teleportEffectPrefab1, teleportEffectPrefab2;
    [SerializeField] float lifeTimeOfPortals, delayTimeOfPortals, sleepTimeOfPortal;
    List<GameObject> portals = new();
    // delay time là thời gian kích hoạt sau khi đc tạo
    // sleep time là thời gian nghỉ sau khi thực hiện dịch chuyển
    public override void Skill()
    {
        CreatePortal(playerTransform.position);
    }
    public void CreatePortal(Vector3 pos)
    {
        if (portals.Count == 2) // nếu có đủ 2 portal
        {
            PlayAndStopParticle.StopParticle(portals[0].transform);
            Destroy(portals[0], 1.5f);
            portals.RemoveAt(0);
        }
        GameObject portal = Instantiate(portalPrefab);
        portal.transform.position = pos;
        Human4SkillAttacktion human4SkillAttacktion = portal.GetComponent<Human4SkillAttacktion>();
        human4SkillAttacktion.Human4Skill = this;
        human4SkillAttacktion.DelayTime = delayTimeOfPortals;
        human4SkillAttacktion.SleepTime = sleepTimeOfPortal;
        human4SkillAttacktion.LifeTime = lifeTimeOfPortals;
        portals.Add(portal);
        CD();
    }
    public void Teleport(GameObject portal, Transform player)
    {
        if (portals.Count != 2)
        { // chỉ thực hiện dịch chuyển khi có 2 cổng
            return;
        }
        // nếu 1 trong 2 cổng ko active thì cũng ko dịch chuyển
        for (int i = 0; i < portals.Count; i++)
        {
            if (!portals[i].GetComponent<Human4SkillAttacktion>().Active)
            {
                return;
            }
        }
        // cho 2 cổng vào chế độ ngủ
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].GetComponent<Human4SkillAttacktion>().Sleep();
        }
        // dịch chuyển
        if (portal == portals[0])
        {
            TeleToPortal(1, player);
        }
        else
        {
            TeleToPortal(0, player);
        }
    }
    public void TeleToPortal(int indexOfPortal, Transform player)
    {
        
        CreateTeleportEffect1(player.position);
        player.position = portals[indexOfPortal].transform.position;
        CreateTeleportEffect2(player.position);
    }
    public void DestroyPortal()
    {
        Destroy(portals[0]);
        portals.RemoveAt(0);
    }
    public void CreateTeleportEffect1(Vector3 pos)
    {
        // Debug.Log(pos);
        GameObject teleportEffect = Instantiate(teleportEffectPrefab1);
        teleportEffect.transform.position = pos;
        Destroy(teleportEffect, 0.5f);
    }
    public void CreateTeleportEffect2(Vector3 pos)
    {
        // Debug.Log(pos);
        GameObject teleportEffect = Instantiate(teleportEffectPrefab2);
        teleportEffect.transform.position = pos;
        Destroy(teleportEffect, 0.5f);
    }
}
