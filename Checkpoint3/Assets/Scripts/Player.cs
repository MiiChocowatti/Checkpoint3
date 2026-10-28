using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float speed = 13f;
    private Vector2 moveInput;
    private SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(x, 0.0f, 0.0f);

        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(0.0f, y, 0.0f);
    }
}
