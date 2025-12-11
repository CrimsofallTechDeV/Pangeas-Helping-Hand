using UnityEngine;

public class BugSmasher : MonoBehaviour
{
    public bool Active = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bug") && Active)
        {
            BugAI ai = other.GetComponent<BugAI>();
            ai.animator.SetBool("Splat", true);
            ai.Stop();
            Destroy(other.gameObject, 2f);
        }
    }

    public void ToggleActive(bool state)
    {
        Active = state;
    }
}
