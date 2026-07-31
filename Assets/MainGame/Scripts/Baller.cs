using System;
using UnityEngine;

public class Baller : MonoBehaviour
{
    GameObject playerRef;
    
    public float initialSpeed;
    public float falloff;
    public float closenessMultiplier;
    public float cutoff;

    public Rigidbody2D rb;
    private Vector2 knockbackDirection;
    private float currentSpeed;

    public GameObject prize;
    
    private ChestScript chestScript;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerRef.GetComponent<Collider2D>());
        chestScript = GameObject.Find("Chest").GetComponent<ChestScript>();
        if (chestScript == null)
        {
            Debug.Log("CAN'TFIND CHEST SCRIPT");
        }
    }

    void Update()
    {
        
        if (currentSpeed > cutoff)
        {
            rb.linearVelocity = knockbackDirection * currentSpeed;
            currentSpeed *= falloff;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            knockbackDirection = (transform.position - playerRef.transform.position).normalized;

            rb.AddForce(knockbackDirection * initialSpeed, ForceMode2D.Impulse);
            currentSpeed = rb.linearVelocity.magnitude;
        }
        else if (other.CompareTag("Hole"))
        {
            //Instantiate(prize, other.transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);

            chestScript.FillChest(prize);
            Debug.Log("chest filled with " + prize.name);

            gameObject.SetActive(false);
        }
    }
}
