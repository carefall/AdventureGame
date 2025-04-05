using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LichTalking : MonoBehaviour
{
    [SerializeField] string[] lines;
    [SerializeField] PlayerInput Input;
    [SerializeField] TextMeshProUGUI subs;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source, musicsource;
    [SerializeField] NavMeshAgent agent;
    private bool ready = false;
    [SerializeField] Animator animator;
    void Start()
    {
        StartCoroutine(Talk());
    }
    private void Update()
    {
        if (ready)
        {
            agent.destination = musicsource.gameObject.transform.position;
        }
    }
    private IEnumerator Talk() 
    {
        for (int i = 0; i < lines.Length; i++)
        {
            source.clip = clips[i];
            source.Play();
            for (int j = 0; j < lines[i].Length; j++)
            {
                subs.text = lines[i].Substring(0, j);
                yield return new WaitForSeconds(0.05f);
            }
            if (i != lines.Length - 1)
            {
                yield return new WaitForSeconds(4);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
        for (int i = 0; i < 180; i++)
        {
            musicsource.gameObject.transform.eulerAngles += new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.011f);
        }
        Input.enabled = true;
        musicsource.Play();
        ready = true;
        animator.Play("walk");
        GetComponent<BoxCollider>().enabled = true;
        subs.gameObject.SetActive(false);
    }

}
