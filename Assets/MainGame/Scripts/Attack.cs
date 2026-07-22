using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Attack : MonoBehaviour
{
    
    private InputAction attackAction;
    
    private GameObject hurtbox;
    
// LENGTH OF TIME ATTACK IS OUT, IN SECONDS
public float attackDurationSeconds;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        hurtbox = GameObject.Find("MantisHurtbox");
        hurtbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.WasPerformedThisFrame())
        {
            DoAttack();
        }
            
    }

    void DoAttack()
    {
        Vector3 attackPosition = FindAttackPos();
        StartCoroutine(SpawnHurtbox(attackPosition));
    }

    Vector3 FindAttackPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseToPlayerDist = mousePos - transform.position;
        Vector3 restrainedAttackPos = transform.position + (mouseToPlayerDist.normalized / 2f);
        return new Vector3(restrainedAttackPos.x, restrainedAttackPos.y, 0f);
    }

    IEnumerator SpawnHurtbox(Vector3 attackPosition)
    {
        // animation

        if(attackPosition.x > transform.position.x)
        {
            // ATTACK LEFT
        }
        else
        {
            // ATTACK RIGHT
        }


        hurtbox.transform.position = attackPosition;
        hurtbox.transform.rotation = AngleFromVectorDiff(attackPosition, transform.position);
        hurtbox.SetActive(true);
        yield return new WaitForSeconds(attackDurationSeconds);
        hurtbox.SetActive(false);

        // END ANIMATION (??? i don't know how animations work)
        
    }

    Quaternion AngleFromVectorDiff(Vector3 vector1, Vector3 vector2)
    {
        Vector3 vector = vector1 - vector2;
        return Quaternion.Euler(0f, 0f, 180f / Mathf.PI * Mathf.Atan(vector.y / vector.x));
    }
}
