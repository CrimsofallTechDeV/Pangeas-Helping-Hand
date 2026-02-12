using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay
{
    public class DogGuy : MonoBehaviour
    {
        public void OnInit()
        {
            GetComponent<Animator>().SetBool("Sleeping", !GameManager.Instance.IsDay);
        }
    }
}
