using UnityEngine;

public class AxotolClimbTrigger : MonoBehaviour
{
    public bool TopTrigger;
    public AxotolClimbing climbing;

    public void OnTriggerEnter(Collider col)
    {
        if(TopTrigger)
        {
            if(col.tag == "Player")
            {
                climbing.PlayerWon = true;
            }

            if(col.tag == "Axotol")
            {
                climbing.OnReachedTop();
            }
        }
        else
        {
            if(col.tag == "Axotol")
            {
                climbing.OnReachedGround();
            }
        }
    }
}
