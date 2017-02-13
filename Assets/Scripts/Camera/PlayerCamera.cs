using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

	public static PlayerCamera instance;
	public PlayerBehaviour player;
	public float fadeLevel = 0.1f;
	public float rangeOffset = 0.1f;
	private GameObject[] hidden;
	[SerializeField]
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

	}

	public void ClearView ()
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
			if (r != null) {
				col = new Color(r.material.color.r,r.material.color.g,r.material.color.b, fadeLevel);
				r.material.color =  col;
			}
		}
	}

	GameObject[] GetElementsInBetween ()
	{
		float distance = Vector3.Distance (transform.position, player.gameObject.transform.position) - rangeOffset;
		Collider[] hits = Physics.OverlapSphere (transform.position, distance);
		Ray ray = new Ray(transform.position, transform.position.DirectionTo(player.gameObject.transform.position));

			
		List<GameObject> result = new List<GameObject>();

		foreach (Collider h in hits) {
			if (!h.CompareTag ("Player")) {
				result.Add (h.GetComponent<Collider>().gameObject);
			}
		}
		return result.ToArray ();
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, Vector3.Distance (transform.position, player.gameObject.transform.position));

	}
}
