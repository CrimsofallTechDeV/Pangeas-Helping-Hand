using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;

public class PictureCamera : MonoBehaviour
{
	public InputActionProperty inputActionLeft, inputActionRight;
	public AudioSource audioSource;
	public Camera gameCamera;       // Assign your in-game camera here
	public GameObject imageObject, screen;
	
	private string saveDirectory;
	private int lastSaveIndex;
	private bool held;
	private RenderTexture defaultRT;

	private void Start()
	{
		// Path to save the captured image
		saveDirectory = Application.dataPath+"/Camshots/";
		defaultRT = gameCamera.targetTexture;
		
		if(!Directory.Exists(saveDirectory)) 
		{
			Directory.CreateDirectory(saveDirectory);
		}
	}
	
	private void Update()
	{
		if(!held)
			return;
		
		if(inputActionLeft.action.WasPressedThisFrame() ||
			inputActionRight.action.WasPressedThisFrame()) {
			CaptureAndSave();
		}
	}

	public void CaptureAndSave()
	{
		audioSource.PlayOneShot(audioSource.clip);

		// Create a temporary RenderTexture
		RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
		gameCamera.targetTexture = rt;

		// Render the camera’s view
		Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		gameCamera.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		screenShot.Apply();

		// Reset
		gameCamera.targetTexture = defaultRT;
		RenderTexture.active = null;
		Destroy(rt);

		// Save to PNG
		byte[] bytes = screenShot.EncodeToPNG();
		File.WriteAllBytes(saveDirectory+$"image{lastSaveIndex}.png", bytes);
		
		//spawn image from camera!
		GameObject g = Instantiate(imageObject, transform.position, Quaternion.identity);
		g.GetComponent<MeshRenderer>().material.mainTexture = LoadLastImage();
		
		lastSaveIndex++;
	}

	private Texture2D LoadLastImage()
	{
		string filePath = saveDirectory+$"image{lastSaveIndex}.png";
		if (File.Exists(filePath))
		{
			byte[] bytes = File.ReadAllBytes(filePath);
			Texture2D tex = new Texture2D(2, 2);
			tex.LoadImage(bytes);

			return FlipTextureVertically(tex);
		}
		else
		{
			Debug.Log("No saved image found at: " + filePath);
		}
		
		//means error loading image sprite
		return null;
	}

	private Texture2D FlipTextureVertically(Texture2D tex)
	{
		Color[] pixels = tex.GetPixels();
		int width = tex.width;
		int height = tex.height;
		Color[] flipped = new Color[pixels.Length];

		for (int y = 0; y < height; y++)
		{
			int flippedRow = height - 1 - y;
			Array.Copy(pixels, y * width, flipped, flippedRow * width, width);
		}

		tex.SetPixels(flipped);
		tex.Apply();
		return tex;
	}
	
	public void OnHeldChange(bool _held)
	{
		held = _held;
		
		screen.SetActive(held);
		gameCamera.enabled = held;
	}
}
