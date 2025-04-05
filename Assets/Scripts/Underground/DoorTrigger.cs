using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        animator.Play("Fade");
        Invoke(nameof(EnterGame),1);
    }
    private void EnterGame()
    {
        SceneManager.LoadScene("OpenWorld");
    }
}
