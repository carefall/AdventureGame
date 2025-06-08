using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StartBot : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Animator anim;
    void Start()
    {
        SaveData save = SaveSystem.GetSaveData();
        if (save.completedQuests.Contains(0) || save.quests.Length>0)
        {
            anim.Play("Idle");
            Destroy(this);
            return;
        }
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
