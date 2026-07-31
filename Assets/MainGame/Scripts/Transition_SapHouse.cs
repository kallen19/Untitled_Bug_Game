using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_SapHouse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("SapHouse_Inside");
        }
    }
}
