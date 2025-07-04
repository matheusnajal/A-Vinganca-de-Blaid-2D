using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    
    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private float moveDirection;

    private Rigidbody2D _rigidbody;
    private IsGroundedChecker isGroundedCheker;
    private Health health;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        isGroundedCheker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();

        health.OnDead += HandlePlayerDeath;
        health.OnHurt += PlayHurtSound;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        MovePlayer();
        FlipSpriteAccordingToMoveDirection();
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.InputManager.Movement;
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);
    }

    private void FlipSpriteAccordingToMoveDirection()
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void HandleJump()
    {
        if (isGroundedCheker.IsGrounded() == false) return;
        GameManager.Instance.Audiomanager.PlaySFX(SFX.PlayerJump);
        _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    private void PlayHurtSound()
    {
        GameManager.Instance.Audiomanager.PlaySFX(SFX.PlayerHurt);
    }

    private void HandlePlayerDeath()
    {
        GameManager.Instance.Audiomanager.PlaySFX(SFX.PlayerDeath);
        GetComponent<Collider2D>().enabled = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.InputManager.DisablePlayerInput();
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.Audiomanager.PlaySFX(SFX.PlayerWalk);
    }

    private void Attack()
    {
        GameManager.Instance.Audiomanager.PlaySFX(SFX.PlayerAttack);
        Collider2D[] hittedEnemies =
            Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);
        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Cheking enemy");
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                GameManager.Instance.Audiomanager.PlaySFX(SFX.EnemyHurt);
                print("Getting damage");
                enemyHealth.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}