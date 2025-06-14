using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float Speed = 5;
    [SerializeField] private float jumpForce = 3;
    private Rigidbody2D _rigidbody;
    private IsGroundedChecker isGroundedChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
    }

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.InputManager != null)
        {
            GameManager.Instance.InputManager.OnJump += HandleJump;
        }
    }

    private void FixedUpdate()
    {
        float moveDirection = GameManager.Instance.InputManager.Movement;
        _rigidbody.linearVelocity = new Vector2(moveDirection * Speed, _rigidbody.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded())
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null && GameManager.Instance.InputManager != null)
        {
            GameManager.Instance.InputManager.OnJump -= HandleJump;
        }
    }
}