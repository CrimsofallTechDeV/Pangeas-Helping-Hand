using UnityEngine;
using UnityEngine.AI;

namespace CrimsofallTechnologies.VR.Gameplay.BugGame
{
    public class BugAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Animator animator;
        public Transform target;

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            agent.SetDestination(target.position);
        }

        public void Stop()
        {
            agent.SetDestination(transform.position);
            agent.isStopped = true;
        }
    }
}
