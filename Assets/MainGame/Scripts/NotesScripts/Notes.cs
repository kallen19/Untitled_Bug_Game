using System;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string NotesID { get; private set; }

    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private SpriteRenderer closedNotes;

    [SerializeField] private GameObject noteButton;
    // [SerializeField] private GameObject objToClose;

    void Start()
    {
        NotesID ??= GlobalHelper.GenerateUniqueID(gameObject);

        closedNotes.enabled = true;
        noteCanvas.SetActive(false);
    }

    //void Update()
    //{
    //    if (!noteCanvas.activeInHierarchy)
    //    {
    //        Debug.Log("closed");
    //        objToClose.SetActive(false);
    //    } else
    //    {
    //        objToClose.SetActive(true);

    //        Debug.Log("opened");
    //    }

    //}

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

    public void CloseNote()
    {
        noteCanvas.SetActive(false);
    }

    public void SetOpened(bool opened)
    {
        IsOpened = opened;

        if (opened)
        {
            closedNotes.enabled = !opened;
            noteCanvas.SetActive(true);
        }
        else
        {
            noteCanvas.SetActive(false);
        }
    }
}

