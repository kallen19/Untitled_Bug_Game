using UnityEngine;

public class CampfirePuzzle : MonoBehaviour, IInteractable
{
    private InventoryUI inventory;
    private SpriteRenderer sr;
    public Sprite litSprite;
    private bool lit;
    public int sticksRequired;
    public void Start()
    {
        inventory = GameObject.Find("ItemContainer").GetComponent<InventoryUI>();
        sr = GetComponent<SpriteRenderer>();
        lit = false;
    }
    
    public void Interact()
    {
        sr.sprite = litSprite;
        for (int i = 0; i < sticksRequired; i++)
        {
            inventory.UseItem(Item.Stick);
        }
        lit = true;
    }

    public bool CanInteract()
    {
        return !lit && inventory.HasItems(Item.Stick, sticksRequired);
    }
}
