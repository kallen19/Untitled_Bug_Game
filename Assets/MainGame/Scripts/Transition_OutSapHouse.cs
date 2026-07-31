using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_OutSapHouse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Town 1");
        }
    }
}
