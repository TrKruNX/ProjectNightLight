using UnityEngine;

public class TurtorialBools : MonoBehaviour
{
    public bool canJump = false;
    public bool canDoubleJump = false;
    public bool canGrapple = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpOn"))
        {
            canJump = true;
        }

        if (other.CompareTag("DoubleJumpOn"))
        {
            canDoubleJump = true;
        }

        if (other.CompareTag("GrappleOn"))
        {
            canGrapple = true;
        }
    }
}
