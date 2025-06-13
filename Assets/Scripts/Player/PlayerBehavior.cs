using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float Speed = 5;

    private void Update()
    {
        float moveDirection = GameManager.Instance.InputManager.Movement;
        transform.Translate(moveDirection * Time.deltaTime * Speed, 0, 0);
    }
}