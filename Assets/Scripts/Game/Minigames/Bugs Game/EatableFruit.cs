using UnityEngine;
using UnityEngine.AI;

public class EatableFruit : MonoBehaviour
{
    public FruitBugs fb;
    private bool alreadyPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bug"))
        {
            //make sure this is the fruit this bug wants to eat!
            BugAI ai = other.GetComponent<BugAI>();
            if(ai.target == transform) 
            {
                //remove the fruit as well as the bug!
                fb.RemoveFruit(gameObject);
                fb.RemoveBug(other.gameObject);

                Destroy(other.gameObject, 2f);
                Destroy(gameObject);
                ai.animator.SetBool("Splat", true);
            }
        }
    }

    public void Picked()
    {
        if(!alreadyPicked) {
            fb.PickedFruit(transform);
            alreadyPicked = true;
        }
    }
}
