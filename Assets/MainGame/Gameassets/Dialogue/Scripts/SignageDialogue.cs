using UnityEngine;

[CreateAssetMenu(fileName = "NewSignage_Dialogue", menuName = "Signage Dialogue")]
public class SignageDialogue : ScriptableObject
{
    public string signName;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}
