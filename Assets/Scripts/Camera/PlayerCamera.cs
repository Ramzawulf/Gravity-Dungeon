using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

	public static PlayerCamera instance;
	public Transform targetPosition;
	public PlayerBehaviour player;

	public float maximumAlpha = 0.3f;

	public float minimumAlpha = 0.6f;

	public float rangeOffset = 0.1f;

	public float originOffset= 0.5f;

	public float threshold = 1.2f;

	public float movementSpeed = 0.2f;
	public float radius = 2f;
	private GameObject[] hidden;
	private Camera camera;



	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		camera = GetComponent<Camera> ();
	}

	void Update () {
		LookAtPlayer ();
		GetClose ();
		ClearView ();
	}

	private void ClearView ()
	{
		DisplayHiddenElements ();
		HideElements ();
	}

	void DisplayHiddenElements ()
	{
		Renderer r;
		if (hidden == null)
			return;
		Color col;
		foreach (var e in hidden) {
			r = e.GetComponent<Renderer> ();
			if (r != null) {
				col = new Color(r.material.color.r,r.material.color.g,r.material.color.b, 1);
				r.material.color =  col;
			}
				
		}
	}

	void HideElements ()
	{
		hidden = GetElementsInBetween ();
		Renderer r;
		Color col;

		foreach (var e in hidden) {
			r = e.GetComponent<Renderer> ();
			if (r != null && !player.LineOfSight.bounds.Contains (e.transform.position)) {
				float a = AlphaLevel (Vector3.Distance(player.transform.position, e.transform.position));
				col = new Color(r.material.color.r,r.material.color.g,r.material.color.b, a);
				r.material.color =  col;
			}
		}
	}

	GameObject[] GetElementsInBetween ()
	{
		float distance = Vector3.Distance (transform.position, player.transform.position) - rangeOffset;
		//RaycastHit[] hits = Physics.SphereCastAll (transform.position, radius, transform.position.DirectionTo (player.gameObject.transform.position));

		Collider[] hits = Physics.OverlapSphere (transform.position+ Vector3.up * originOffset, distance);
		//Ray ray = new Ray(transform.position + Vector3.up * originOffset, transform.position.DirectionTo(player.gameObject.transform.position));

			
		List<GameObject> result = new List<GameObject>();

		/*
		foreach (RaycastHit h in hits) {
			if (!h.collider.CompareTag ("Player")) {
				result.Add (h.collider.GetComponent<Collider>().gameObject);
			}
		}
		*/

		foreach (Collider h in hits) {
			if (!h.CompareTag ("Player")) {
				result.Add (h.GetComponent<Collider>().gameObject);
			}
		}
		return result.ToArray ();
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		//Gizmos.DrawWireSphere (transform.position, Vector3.Distance (transform.position, player.gameObject.transform.position));

	}

	void GetClose ()
	{
		transform.position = Vector3.MoveTowards (transform.position, targetPosition.position, movementSpeed);
	}

	void LookAtPlayer ()
	{
		
			transform.LookAt (player.gameObject.transform);

	}

	float AlphaLevel (float f)
	{
		float maxRange = Vector3.Distance (transform.position, player.transform.position);

		float mapMin = 1;
		float mapMax = 0;
	
		return minimumAlpha - minimumAlpha*(f/maxRange)+maximumAlpha;
	}
}
