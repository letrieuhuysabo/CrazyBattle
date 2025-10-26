using UnityEngine;

public class FootPlayer : MonoBehaviour
{
    // bool isGrounding;
    int stepJump;
    bool isGrounding;
    public int StepJump { get => stepJump; set => stepJump = value; }
    public bool IsGrounding { get => isGrounding; set => isGrounding = value; }

    void Awake()
    {
        // isGrounding = false;
        StepJump = 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounding = true;
            StepJump = 2;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounding = false;
            StepJump = 1;
        }
    }
}
