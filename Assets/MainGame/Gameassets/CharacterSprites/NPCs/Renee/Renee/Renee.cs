using UnityEngine;

public class Renee : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string ReneeID { get; private set; }

    [SerializeField] private GameObject openedArea;
    [SerializeField] private SpriteRenderer closedSprite;

    void Start()
    {
        ReneeID ??= GlobalHelper.GenerateUniqueID(gameObject);

        closedSprite.enabled = true;
        openedArea.SetActive(false);
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        OpenCage();
    }

    private void OpenCage()
    {
        SetOpened(true);
    }

    public void SetOpened(bool opened)
    {
        IsOpened = opened;

        if (opened)
        {
            closedSprite.enabled = !opened;
            openedArea.SetActive(true);
        }
    }
}