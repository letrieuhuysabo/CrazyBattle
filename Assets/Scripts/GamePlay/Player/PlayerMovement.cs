using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayerMovement : MonoBehaviour
{
    protected bool facingRight;
    protected Rigidbody2D rb;
    protected PlayerAnimatorController playerAnimatorController;
    PlayerProperties playerProperties;
    FootPlayer footPlayer;
    protected float xDir, yDir;
    protected Vector3 velocity;
    protected PlayerInput playerInput;
    public bool FacingRight { get => facingRight; set => facingRight = value; }

    protected void Awake()
    {
        facingRight = transform.localScale.x < 0;
        rb = GetComponent<Rigidbody2D>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        footPlayer = transform.Find("Foot").GetComponent<FootPlayer>();
        playerProperties = GetComponent<PlayerProperties>();
        playerInput = GetComponent<PlayerInput>();
    }
    [System.Obsolete]
    void Update()
    {
        // nhận input di chuyển
        if (Input.GetKey(playerInput.keyLeft))
        {
            xDir = -1;
        }
        else if (Input.GetKey(playerInput.keyRight))
        {
            xDir = 1;
        }
        else
        {
            xDir = 0;
        }
        // rơi xuống nhanh
        if (!transform.Find("Foot").GetComponent<FootPlayer>().IsGrounding && Input.GetKey(playerInput.keyDown))
        {
            yDir = -1;
        }
        else
        {
            yDir = 0;
        }
        // nhận input nhảy 
        if (Input.GetKeyDown(playerInput.keyUp))
        {
            if (Input.GetKey(playerInput.keyDown)) // nhảy cao
            {
                Jump(0.85f);
            }
            else // nhảy thấp
            {
                Jump(1.1f);
            }
        }
        // flip
        if ((xDir > 0 && !facingRight) || (xDir < 0 && facingRight))
        {
            Flip();
        }
    }
    protected void Flip()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 0);
        facingRight = !facingRight;
    }

    [System.Obsolete]
    protected void Jump(float n)
    {
        // Debug.Log(jumpForce);
        if (footPlayer.StepJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            footPlayer.StepJump--;
            rb.AddForce(Vector2.up * playerProperties.JumpForce / n, ForceMode2D.Impulse);
            // tạo effect
            GameObject jumpEffect = JumpEffectPool.instance.TakeObj(transform.position + new Vector3(0, -0.1f, 0));
            JumpEffectPool.instance.ReturnObj(jumpEffect, 1f);
        }

    }
    [System.Obsolete]
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * playerProperties.MoveSpeed, rb.velocity.y + yDir);
        playerAnimatorController.ChangeAnim("1_Move", rb.velocity.x != 0);
    }
    public bool WantToMoveAirSkill()
    {
        return !transform.Find("Foot").GetComponent<FootPlayer>().IsGrounding && xDir == 0;
    }
    public void ReCalculateFacingRight()
    {
        facingRight = transform.localScale.x < 0;
    }
}
