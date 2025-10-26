using UnityEngine;

public class Human3Skill : CharacterSkillController
{
    // mô tả chiêu thức:
    // Khi dùng, dịch chuyển đến 1 điểm ở hướng theo hướng điều khiển
    // Ở cả vị trí trước và sau khi dịch chuyển, tạo 1 vụ nổ lửa, gấy sát thương cho kẻ địch
    [SerializeField] float distanceTeleport, delayTimeToExplore, damageOfExplore;
    [SerializeField] GameObject fireExplorePrefab;
    public override void Skill()
    {
        PlayerInput playerInput = playerTransform.GetComponent<PlayerInput>();
        // set up hướng
        Vector3 directionTeleport = new();
        // trên dưới
        if (Input.GetKey(playerInput.keyUp))
        {
            directionTeleport.y = 1;
        }
        else if (Input.GetKey(playerInput.keyDown))
        {
            directionTeleport.y = -1;
        }
        else
        {
            directionTeleport.y = 0;
        }
        // trái phải
        if (Input.GetKey(playerInput.keyLeft))
        {
            directionTeleport.x = -1;
        }
        else if (Input.GetKey(playerInput.keyRight))
        {
            directionTeleport.x = 1;
        }
        else // nếu ko điều khiển trái phải thì sẽ dịch chuyển theo hướng nhìn của nhân vật
        {
            if (directionTeleport.y == 0) // nếu dịch chuyển theo trên/dưới thì ko bắt buộc dịch chuyển trái/phải
            {
                if (playerTransform.GetComponent<PlayerMovement>().FacingRight)
                {
                    directionTeleport.x = 1;
                }
                else
                {
                    directionTeleport.x = -1;
                }
            }
        }

        // đã có hướng, thực hiện dịch chuyển
        // tạo vụ nổ 1

        CreateExplore(playerTransform.position);
        // di chuyển
        playerTransform.position += directionTeleport.normalized * distanceTeleport;
        // tạo vụ nổ 2
        CreateExplore(playerTransform.position);
        CD();
    }
    void CreateExplore(Vector3 pos)
    {
        GameObject fireExplore = Instantiate(fireExplorePrefab);
        Human3SkillAttacktion human3SkillAttacktion = fireExplore.GetComponent<Human3SkillAttacktion>();
        // setup delay time
        human3SkillAttacktion.DelayTime = delayTimeToExplore;
        // setup damage
        human3SkillAttacktion.Damage = damageOfExplore;
        // set up owner
        human3SkillAttacktion.Owner = playerTransform;
        fireExplore.transform.position = pos;
        // PlayAndStopParticle.StopParticle(fireExplore.transform,2);
        Destroy(fireExplore, 2);
    }
}
