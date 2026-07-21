using System;
using UnityEngine;

public enum EnemyState
{
    Following,
    Knockback,
    Sleeping
}

public class EnemyMove : MonoBehaviour
{
    public EnemyManager enemyManager;

    public EnemyState state = EnemyState.Following;
    
    public float speedUnitsPerSec = 0;
    public Transform targetTransform;

    public float knockbackAmountMax = 2;

    public float knockbackTime = 0.5f;
    private float knockbackTimer = 0;

    private float knockbackAmountReal;

    public float knockbackFalloffRatio = 0.95f;
    
    public Rigidbody2D rb;
    
    Vector2 moveVector;

    private GameObject playerRef;

    public float wakeUpDistance;
    
    
    // enemy hurt

    public float maxHealth;

    public float health;
    
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        rb = GetComponent<Rigidbody2D>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        targetTransform = playerRef.transform;
        health = maxHealth;
        state = EnemyState.Sleeping;
    }

    // Update is called once per frame
    void Update()
    {
        Track();

        switch (state)
        {
            case EnemyState.Following:
                if(!PlayerNear())
                {
                    state = EnemyState.Sleeping;
                    
                } else
                {
                                    rb.linearVelocity = moveVector * speedUnitsPerSec;

                }
                break;
            case EnemyState.Knockback:
                Vector3 awayVector = (transform.position - targetTransform.position);
                awayVector.Normalize();
                //rb.AddForce(awayVector * knockbackAmount, ForceMode2D.Impulse);
                rb.linearVelocity = awayVector * knockbackAmountReal;
                
                // faux friction
                knockbackAmountReal *= knockbackFalloffRatio;
                
                knockbackTimer -= Time.deltaTime;
                if (knockbackTimer <= 0)
                {
                    knockbackTimer = 0;
                    state = EnemyState.Following;
                }

                break;
            case EnemyState.Sleeping:
                if(PlayerNear())
                {
                    state = EnemyState.Following;
                }
                break;
        }
        //transform.Translate(moveVector.x * speedUnitsPerSec * Time.fixedDeltaTime, moveVector.y * speedUnitsPerSec * Time.fixedDeltaTime, 0);
        
        
    }
    
    void FixedUpdate()
    {

    }

    void Track()
    {
        moveVector = targetTransform.position - transform.position;
        moveVector.Normalize();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("i hit the enemy");
            state = EnemyState.Knockback;
            knockbackTimer = knockbackTime;
            knockbackAmountReal = knockbackAmountMax;
            Hurt(other.GetComponent<DamageValue>().getDamageValue());
        }

        //playerRef.GetComponent<move>().Knockback(transform);
    }
    
    // hurt

    public void Hurt(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameObject.SetActive(false);
            enemyManager.EnemyDeath.Invoke();
        }
    }

    private bool PlayerNear()
    {
        return Vector3.Distance(transform.position, playerRef.transform.position) < wakeUpDistance;    
    }
}
