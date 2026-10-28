using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBullet : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed = 8f;

    public void OnAttack(InputAction.CallbackContext context)
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 start = weapon.position;
        Vector2 target = mouse;
        Vector2 direction = (target - start).normalized;

        GameObject b = Instantiate(bullet, weapon.position, Quaternion.identity);
        b.AddComponent<Rigidbody2D>().gravityScale = 0;
        b.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

        Destroy(b, 3f);
    }
}
