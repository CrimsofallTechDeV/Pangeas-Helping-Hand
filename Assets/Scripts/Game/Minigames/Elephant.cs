using UnityEngine;
using UnityEngine.AI;

public class Elephant : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform faceT, standT;
    public float rotationSpeed = 5f;

    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool moving = false, facing;

    //trigger by animation event
    public void OnConveyorAnimEnd()
    {
        moving = true;
        agent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if(facing)
        {
            FaceTarget(faceT.position);
			transform.parent.position = standT.position;
			return;
        }

        if(!moving) { 
            animator.SetBool("Run", false);
            return;
        }

        animator.SetBool("Run", true);
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //move to next waypoint
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = waypoints.Length - 1;
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void FaceTarget(Vector3 TargetPos)
    {
        Vector3 angle = (TargetPos - transform.parent.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(angle.x, 0, angle.z));
        transform.parent.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void End()
    {
        //jump and stand on the bucket:
        moving = false;
        animator.SetTrigger("Jump");
        Destroy(agent);
    }

    public void FaceEnd()
    {
        facing = true;
        transform.parent.position = standT.position;
    }
}
