using UnityEngine;

public class PaintingCanvas : MonoBehaviour
{
	public void OnGrab()
	{
		//remove all sprites and make it plain
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
