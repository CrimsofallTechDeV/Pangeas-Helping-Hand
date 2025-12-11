using UnityEngine;

public class DogGuy : MonoBehaviour
{
    public void OnInit()
    {
        GetComponent<Animator>().SetBool("Sleeping", !GameManager.Instance.IsDay);
    }
}
