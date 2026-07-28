using UnityEngine;

public class PickMeUp : MonoBehaviour, IInteractable
{
    [SerializeField] SpriteRenderer sr;
    
    Color[] colors;

    int currentColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentColor = 0;
        colors = new Color[] {Color.magenta, Color.coral, Color.teal, Color.azure};
        sr.color = colors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        currentColor = currentColor == colors.Length - 1 ? 0 : currentColor + 1;
        sr.color = colors[currentColor];
    }

    public bool CanInteract()
    {
        return true;
    }

}
