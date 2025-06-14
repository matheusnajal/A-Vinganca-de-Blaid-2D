using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(checkerPosition.position, checkSize, 0f, groundLayer);
    }

        private void OnDrawGizmos()
    {
        if (checkerPosition == null) return;
        if (IsGrounded())
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawCube(checkerPosition.position, checkSize);
    }
}