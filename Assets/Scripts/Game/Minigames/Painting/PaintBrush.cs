using UnityEngine;

public class PaintBrush : MonoBehaviour
{
	public GameObject shapePrefab;   // Assign a prefab (circle, square, etc.)
	public float spacing = 0.1f, distance, spawnOffset = 0.01f;     // Distance between stamps
	public bool IsDrawing = true;    // Toggle drawing on/off
	public Color currentColor;
	public LayerMask canvasMask;
	public Transform rayPoint;
	
	//number of decals spawned over time for this - will automatically stop after spawning 3,000 decals!
	public int numberSpawned;
	
	private Vector3 lastHitPoint;
	//private float lastTime;

	private void Update()
	{
		if(numberSpawned >= 3000) return;
		
		// Only draw if IsDrawing is true
		if (IsDrawing)
		{
			// Cast a ray forward from the brush to detect the canvas
			Ray ray = new Ray(rayPoint.position, rayPoint.forward);

			if (Physics.Raycast(ray, out RaycastHit hit, distance, canvasMask))
			{
				if(hit.collider.tag != "Canvas") return; //ensure we only draw on canvas objects!

				//ensure the brush does not keep spawning on once place!
				if(Vector3.Distance(lastHitPoint, hit.point) >= spacing) 
				{
					// Calculate a small offset opposite to the hit normal
					Vector3 spawnPos = hit.point - hit.normal * spawnOffset;
	
					// Align shape with canvas surface using hit.normal
					Quaternion rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
	
					// Spawn shape slightly above the canvas
					GameObject g = Instantiate(shapePrefab, spawnPos, rotation);
					SpriteRenderer r = g.GetComponent<SpriteRenderer>();
					r.color = currentColor;
					
					//make this on a higher layer than the last to ensure no overlapping/glitching happens
					r.sortingOrder = numberSpawned;
					g.transform.parent = hit.collider.transform; //parent to canvas!
					lastHitPoint = hit.point;
					
					numberSpawned++;
				}
			}

			//lastTime = Time.time + spacing;
		}
	}
	
	public void OnPicked(bool value)
	{
		if(value) IsDrawing = true;
		else IsDrawing = false;
		
		lastHitPoint = Vector3.zero;
	}
}
