using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	public Camera[] cameras;
	public GameObject character;
    
	private Plane[] planes;
	private Camera cam;

	// Use this for initialization
	void Start () {
		Debug.Log("TESTING: " + cameras.Length);
		
        turnOffCameras();
		switchCamera();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Transform camTrans;
		Vector3 fwd;
		
		cam = Camera.main;
		
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
			
		if (GeometryUtility.TestPlanesAABB(planes, character.collider.bounds))
		{
			camTrans = cam.transform;
			fwd = character.transform.position - camTrans.position;
			
			if(Physics.Raycast (camTrans.position, fwd, out hit))
			{			
				if(hit.collider.gameObject.name != "PC")
				{
					switchCamera();
				}
			}
		}
		else 
		{
			switchCamera();
		}
	}
	
	private void switchCamera()
	{
		RaycastHit hit;
		Transform camTrans;
		Vector3 fwd;
		
		for(int i=0; i < cameras.Length; i++)
		{
			cam = cameras[i];
			planes = GeometryUtility.CalculateFrustumPlanes(cam);
			
			if (GeometryUtility.TestPlanesAABB(planes, character.collider.bounds))
			{
				camTrans = cam.transform;
				fwd = character.transform.position - camTrans.position;
				
				if(Physics.Raycast (camTrans.position, fwd, out hit))
				{
					Debug.Log("Camera: " + i + " Object: " + hit.collider.gameObject.name);
					
					if(hit.collider.gameObject.name == "PC")
					{
						cam.enabled = true;
						turnOffCameras(i);
						break;
					}
				}
			}
		}
	}
	
	private void turnOffCameras(int ignore = -1)
	{
		for(int i=0; i < cameras.Length; i++)
		{
			if(i != ignore)
				cameras[i].enabled = false;
		}
	}
}
