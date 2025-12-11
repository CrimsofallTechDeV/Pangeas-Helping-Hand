using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(1)]
public class CustomSceneManager : MonoBehaviour
{
    public Light sun;

    public GameObject[] dayObjects, nightObjects;

    public AudioClip musicClipDay, musicClipNight;

    private void Awake()
    {
        GameManager.Instance.SetCustomSceneManager(this);
    }
}
