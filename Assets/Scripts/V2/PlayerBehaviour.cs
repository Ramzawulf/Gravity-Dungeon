using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Tile;
using Assets.Scripts.V2;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public static PlayerBehaviour instance;
	public Tile currentTile;
	[SerializeField]
	public Transform forwardCheck;
	[SerializeField]
	public Transform downCheck;
	[SerializeField]
	public Transform floorCheck;
	[SerializeField]
	public Transform perpendicularCheckEnd;
	public Collider LineOfSight;
	public float rotationSpeed = 0.5f;
	public float distanceToTile = 0.25f;
	public bool isRotating;
	public bool switchingGravity;
	private Collider col;
	bool isMoving = false;
	public float strideSize = 0.2f;
	bool isLocked = false;

	public IEnumerator MoveTowards(Directions targetDirection){
		//Face towards
		LockControls();
		yield return StartCoroutine( FaceTowards (targetDirection));
		bool isPathClear = CheckForClearPath ();
		Tile t;
		if (isPathClear) {
			yield return StartCoroutine(MoveForward ());
			t = GetTileBelow ();
			if (t != null) {
				StepIntoTile (t);
			} else {
				t = CheckForPerpendicularTile ();
				if (t == null) {
					yield return StartCoroutine(Fall ());
				} else {
					yield return StartCoroutine(FaceDown ());
					yield return StartCoroutine(MoveForward ());
					StepIntoTile (t);
				}
			}
		} else {
			 GameObject obj = GetObjectInFront ();
			t = obj.GetComponent<Tile>();
			if (t != null) {
				yield return StartCoroutine(FaceUp ());
				StepIntoTile (t);
				//yield return StartCoroutine(MoveForward ());
			} else {
				//ToDo: Interactable.
			}
		}
		UnlockControls ();
	}
		
	//Stops the player from receiven external input
	void LockControls ()
	{
		isLocked = true;
	}

	void UnlockControls ()
	{
		isLocked = false;
	}

	//Updates the reference to the current tile and triggers events on tile..
	void StepIntoTile (Tile t)
	{
		if(currentTile != null)
			currentTile.OnStepOut (); 
		currentTile = t;
		AlignToTile (t);
		currentTile.OnStepIn (gameObject);
	}

	//Aligns the players poition to the centre of the tile.
	void AlignToTile (Tile t)
	{
		transform.position = t.centre.position;
	}

	//Moves the player one unit forward
	IEnumerator MoveForward ()
	{
		Vector3 targetPosition = transform.position + transform.forward;
		isMoving = true;
		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, strideSize);
			if (targetPosition == transform.position)
				break;
			yield return null;
		}
		isMoving = false;
	}

	public IEnumerator FaceDown ()
	{
		switchingGravity = true;
		int targetRotation = 90;
		float rotatedDegrees = 0;
		while(true) {
			if ((rotatedDegrees + rotationSpeed) > targetRotation) {
				transform.Rotate (Vector3.right*(targetRotation - rotatedDegrees));
				break;
			}
			rotatedDegrees += rotationSpeed;
			transform.Rotate (Vector3.right*rotationSpeed);
			yield return null;
		}
		switchingGravity = false;
	}

	public IEnumerator Fall ()
	{
		Tile t;
		isMoving = true;
		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, (transform.position + transform.up * -1), strideSize);
			t = GetTileBelow ();
			if (t != null)
				break;
			yield return null;
		}
		t = GetTileBelow ();
		if (t != null)
			StepIntoTile (t);
		else
			Die ();
		isMoving = false;
	}

	Tile CheckForPerpendicularTile ()
	{
		RaycastHit hit;
		Vector3 direction = perpendicularCheckEnd.position - downCheck.position;
		float distance = Vector3.Distance(downCheck.position, perpendicularCheckEnd.position);
		Tile t = null;

		if (Physics.Raycast (downCheck.position, direction, out hit, distance)) {
			t = hit.collider.gameObject.GetComponent<Tile> ();
		}
		return t;
	}

	Tile GetTileBelow ()
	{
		RaycastHit hit;
		Tile tile = null;
		Vector3 direction = downCheck.position - transform.position;
		float distance = Vector3.Distance (downCheck.position , transform.position);
		col.enabled = false;
		if(Physics.Raycast(transform.position, direction, out hit, distance)){
			tile = hit.collider.gameObject.GetComponent<Tile> ();
		}

		col.enabled = true;
		if (tile != null)
			return tile;
		else
			return null;
	}

	IEnumerator FaceTowards (Directions direction)
	{
		isRotating = true;
		int targetRotation = 0;
		switch (direction) {
		case Directions.Right:
			targetRotation = 90;
			break;
		case Directions.Back:
			targetRotation = 180;
			break;
		case Directions.Left:
			targetRotation = -90;
			break;
		}

		float rotatedDegrees = 0;

		if(targetRotation < 0){
		
			targetRotation = 90;
			while(true) {
				if (Mathf.Abs(rotatedDegrees + rotationSpeed) > targetRotation) {
					transform.Rotate (Vector3.down*(targetRotation - rotatedDegrees));
					break;
				}
				rotatedDegrees += rotationSpeed;
				transform.Rotate (Vector3.down*rotationSpeed);
				yield return null;
			}

		}else{
			
		while(true) {
			if (Mathf.Abs(rotatedDegrees + rotationSpeed) > targetRotation) {
				transform.Rotate (Vector3.up*(targetRotation - rotatedDegrees));
				break;
			}
			rotatedDegrees += rotationSpeed;
			transform.Rotate (Vector3.up*rotationSpeed);
			yield return null;
			}
		}
		isRotating = false;
		//Todo fix CCW rotation
	}

	bool CheckForClearPath ()
	{
		Vector3 direction = forwardCheck.position - transform.position;
		float distance = Vector3.Distance (forwardCheck.position , transform.position);

		return !(Physics.Raycast (transform.position, direction, distance));
	}

	GameObject GetObjectInFront ()
	{
		RaycastHit hit;
		
		Vector3 direction = forwardCheck.position - transform.position;
		float distance = Vector3.Distance (forwardCheck.position , transform.position);

		if (Physics.Raycast (transform.position, direction, out hit, distance))
			return hit.collider.gameObject;
		return null;
	}

	IEnumerator FaceUp ()
	{
		switchingGravity = true;
		int targetRotation = 90;
		float rotatedDegrees = 0;
		while(true) {
			if ((rotatedDegrees + rotationSpeed) > targetRotation) {
				transform.Rotate (Vector3.left*(targetRotation - rotatedDegrees));
				break;
			}
			rotatedDegrees += rotationSpeed;
			transform.Rotate (Vector3.left*rotationSpeed);
			yield return null;
		}
		switchingGravity = false;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		//Front checking ray
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, forwardCheck.position);
		Gizmos.DrawWireSphere (forwardCheck.position, 0.05f);
		//Check for perpendicular Tiles
		Gizmos.color = Color.white;
		Gizmos.DrawLine(downCheck.position, perpendicularCheckEnd.position);
		Gizmos.DrawLine(floorCheck.position, downCheck.position);
		Gizmos.DrawWireSphere (floorCheck.position, 0.025f);
		Gizmos.DrawWireSphere (downCheck.position, 0.025f);
		Gizmos.DrawWireSphere (perpendicularCheckEnd.position, 0.025f);

	}

	void Die ()
	{
		print ("Player died");
	}

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		col = gameObject.GetComponent<Collider> ();
	}

	void Start () {
		Tile t = GetTileBelow ();
		if (t == null)
			StartCoroutine(Fall ());
	}

	void Update () {
		if (isLocked)
			return;
		if (Input.GetKeyDown (KeyCode.W))
			StartCoroutine (MoveTowards(Directions.Forward));
		if (Input.GetKeyDown (KeyCode.A))
			StartCoroutine (MoveTowards(Directions.Left));
		if (Input.GetKeyDown (KeyCode.S))
			StartCoroutine (MoveTowards(Directions.Back));
		if (Input.GetKeyDown (KeyCode.D))
			StartCoroutine (MoveTowards(Directions.Right));
		if (Input.GetKeyDown (KeyCode.T)) {
			if(GetTileBelow() != null)
				StepIntoTile(GetTileBelow());
		}
			
	}
}
