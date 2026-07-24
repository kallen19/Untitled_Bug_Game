using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transition_Town : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Town");
        }
    }
}
