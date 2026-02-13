using Unity.XR.CoreUtils;
using UnityEngine;

public class CloudFallProtector : MonoBehaviour
{
    public XROrigin xR;

    private Vector3 startingPos;

    private void Start()
    {
        startingPos = xR.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //just prevent player from falling down!
            xR.transform.position = startingPos;
        }
    }
}
