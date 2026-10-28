using UnityEngine;
using System.Collections;

public class BossBehaviours : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRange = 4f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int health = 100;

    private Animator animator;
    private SpriteRenderer sr;
    private int maxHealth;
    private float nextAttack;
    private bool dead;
    private Color originalColor;
    private float timer = 0f;
    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        maxHealth = health;

        if (sr != null)
        {
            originalColor = sr.color;
        }
    }
    void Update()
    {
        timer += UnityEngine.Time.deltaTime;

        if (dead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            animator.SetFloat("pMove", 0f);
        }

        else if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            animator.SetFloat("pMove", 1f);

            Turn();
        }

        else
        {
            animator.SetFloat("pMove", 0f);
            Turn();

            if (Time.time >= nextAttack)
            {
                animator.SetTrigger("pAttack");
                nextAttack = timer = attackCooldown;
            }
        }
    }
    void Turn()
    {
        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void TakeDamage(int damage)
    {
        if (dead) return;

        health -= damage;

        StopAllCoroutines();
        StartCoroutine(HitEffect());

        if (health <= 0)
        {
            dead = true;
            animator.SetFloat("pMove", 0f);
            animator.SetTrigger("Dead");
            Turn();
        }
    }
    IEnumerator HitEffect()
    {
        if (sr == null) yield break;

        sr.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sr.color = originalColor;
    }
    public void FinishDeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}