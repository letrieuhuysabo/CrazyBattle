using System.Threading.Tasks;
using UnityEngine;

public abstract class MultiplayerDeadController : MonoBehaviour
{
    public static async void Respawn(GameObject player)
    {
        // ném vũ khí
        player.GetComponent<PlayerAttackController>().ThrowLeftWeapon();
        player.GetComponent<PlayerAttackController>().ThrowRightWeapon();
        // disable nhân vật
        player.transform.position = new Vector3(player.transform.position.x, MultiplayerMapPropertiesReader.deadZoneY + 0.5f, 0);
        player.GetComponent<PlayerInput>().DisableKeys();
        player.SetActive(false);
        await WaitTask.WaitForSeconds(4f);
        // hồi sinh
        player.SetActive(true);
        player.GetComponent<PlayerInput>().SetupKeysFromConfigs();
        player.transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().Respawn();
        player.GetComponent<PlayerAnimatorController>().ChangeAnim("isDeath", false);
        player.transform.position = MultiplayerMapPropertiesReader.posRespawn;
    }
}
