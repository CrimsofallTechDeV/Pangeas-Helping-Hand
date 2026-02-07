using UnityEngine;

public class BugSmasher : MonoBehaviour
{
    public bool Active = false;
    public float swingThreshold = 2.5f;   // speed needed to trigger swing

    private Vector3 lastPosition;
    private bool hasSwung = false;
    public AudioClip splatSound, swingSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!Active)
            return;

        //when player swings play a swing sound (calculate swing from position)
        float distance = Vector3.Distance(transform.position, lastPosition);
        float speed = distance / Time.deltaTime;

        // Detect swing
        if (speed > swingThreshold)
        {
            if (!hasSwung)
            {
                if(!source.isPlaying) source.PlayOneShot(swingSound, 0.35f);
                hasSwung = true;
            }
        }
        else
        {
            hasSwung = false;
        }

        // Store position for next frame
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bug") && Active)
        {
            BugAI ai = other.GetComponent<BugAI>();
            ai.animator.SetBool("Splat", true);
            source.PlayOneShot(splatSound);
            ai.Stop();
            Destroy(other.gameObject, 2f);
        }
    }

    public void ToggleActive(bool state)
    {
        Active = state;
    }
}
