using UnityEngine;
using UnityEngine.AI;

public class StartBot : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Animator anim;
    void Start()
    {
        agent.destination = player.position;
    }
    void Update()
    {
        agent.destination = player.position;
        if (agent.remainingDistance <= 1f)
        {
            agent.isStopped = true;
            agent.destination = transform.position;
            anim.Play("Idle");
            Destroy(this);
        }
    }
}
