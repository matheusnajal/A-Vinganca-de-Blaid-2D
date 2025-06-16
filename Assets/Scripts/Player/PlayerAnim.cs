using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IsGroundedChecker))]
[RequireComponent(typeof(Health))]

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker groundedChecker;
    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDead += PlayDeadAnim;

        playerHealth.OnDead += PlayDeadAnim;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAttack += PlayAttackAnim;
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("IsJump", !groundedChecker.IsGrounded());
    }

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayDeadAnim()
    {
        animator.SetTrigger("dead");
    }

    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }
}