using UnityEngine;

public class Notes : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string NotesID { get; private set; }

    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private SpriteRenderer closedNotes;

    void Start()
    {
        NotesID ??= GlobalHelper.GenerateUniqueID(gameObject);

        closedNotes.enabled = true;
        noteCanvas.SetActive(false);
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        OpenNote();
    }

    private void OpenNote()
    {
        SetOpened(true);
    }

    public void SetOpened(bool opened)
    {
        IsOpened = opened;

        if (opened)
        {
            closedNotes.enabled = !opened;
            noteCanvas.SetActive(true);
        }
    }
}

