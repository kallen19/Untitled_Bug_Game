using System;
using UnityEngine;

public class TheyShallNotAffectThePlayerOrAntsIGuess : MonoBehaviour
{
    Collider2D playerRef;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        foreach (Transform child in transform)
        {
            Physics2D.IgnoreCollision(child.gameObject.GetComponent<BoxCollider2D>(), playerRef);
        }
    }
}
