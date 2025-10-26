using System.Threading.Tasks;
using UnityEngine;

public class SingleplayerDeadController : MonoBehaviour
{
    public static async void Respawn(GameObject player)
    {
        // ném vũ khí
        player.GetComponent<PlayerAttackController>().ThrowLeftWeapon();
        player.GetComponent<PlayerAttackController>().ThrowRightWeapon();
        // disable nhân vật
        player.transform.position = new Vector3(player.transform.position.x, SingleplayerMapPropertiesReader.deadZoneY + 0.5f, 0);
        float n = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAttackController>().enabled = false;

        if (SingleplayerPlayerDeadController.instance.hearth <= 0)
        {
            return;
        }
        await WaitTask.WaitForSeconds(4f);
        // hồi sinh
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAttackController>().enabled = true;
        player.transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().Respawn();
        player.GetComponent<PlayerAttackController>().ThrowRightWeapon();
        player.GetComponent<PlayerAnimatorController>().ChangeAnim("isDeath", false);
        player.transform.position = SingleplayerMapPropertiesReader.posRespawn;
        player.GetComponent<Rigidbody2D>().gravityScale = n;
        player.GetComponent<Collider2D>().enabled = true;
    }
}
