using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Attack : MonoBehaviour
{
    
    private InputAction attackAction;
    
    private GameObject hurtbox;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        hurtbox = GameObject.Find("MantisHurtbox");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.IsPressed())
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
        
        hurtbox.transform.position = attackPosition;
        hurtbox.transform.rotation = AngleFromVectorDiff(attackPosition, transform.position);
        hurtbox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        hurtbox.SetActive(false);
        
    }

    Quaternion AngleFromVectorDiff(Vector3 vector1, Vector3 vector2)
    {
        Vector3 vector = vector1 - vector2;
        return Quaternion.Euler(0f, 0f, 180f / Mathf.PI * Mathf.Atan(vector.y / vector.x));
    }
}
