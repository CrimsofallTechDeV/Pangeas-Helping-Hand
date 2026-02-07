using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    #region Singleton

    public static LoadingUI Instance;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    #endregion

    public Animator animator;

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        DisableScreen();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = Camera.main;
    }

    public void EnableScreen()
    {
        animator.SetBool("Open", true);
    }

    public void DisableScreen()
    {
        animator.SetBool("Open", false);
    }
}
