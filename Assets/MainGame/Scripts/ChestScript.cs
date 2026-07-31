using System.Collections;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChestScript : MonoBehaviour, IInteractable
{
    public Sprite closed;
    public Sprite[] openAnimation;
    
    public bool filled = false;

    public GameObject prize;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.gray;
    }
    
    public void Interact()
    {
        Instantiate(prize, transform.position + Vector3.down * 0.5f, Quaternion.identity);
        StartCoroutine(OpenAnim());
        filled = false;
    }

    public void FillChest(GameObject thePrize)
    {
        prize = thePrize;
        filled = true;
        sr.color = Color.white;
        Debug.Log("interact with me");
    }

    public bool CanInteract()
    {
        return filled;
    }

    IEnumerator OpenAnim()
    {
        foreach (Sprite animSprite in openAnimation)
        {
            sr.sprite = animSprite;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        foreach (Sprite animSprite in openAnimation.Reverse())
        {
            sr.sprite = animSprite;
                     yield return new WaitForSeconds(0.1f);
                 }

        sr.color = Color.gray;
    }
}
