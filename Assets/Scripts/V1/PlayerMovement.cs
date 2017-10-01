using UnityEngine;

namespace Assets.Scripts.V1{
public class PlayerMovement : MonoBehaviour {

	public Vector3 targetPosition;
	public float minDistance = 0.1f;

	public float moveDistance = 1;
	public float strideSize = 0.2f;
	public float lastMove;
	public float movementSpeed = 0.25f;
	public float rotationSpeed = 0.5f;

	private bool canMove{get{ 
			return (Vector3.Distance(transform.position, targetPosition) < strideSize);}}

	public void Awake(){
		lastMove = Time.time;
	}

	void Update () {
	
		TryMove ();
	}

	private void TryMove(){
		if (Input.GetButtonDown ("Jump") ) {
			Jump ();
		} else if (Input.GetKeyDown (KeyCode.K) && canMove) { //Switch Gravity
			SwitchGravity();
			lastMove = Time.time;
		} 
		else if (Input.GetKeyDown(KeyCode.W) && canMove) { // Forward movement
			GoForward();
			lastMove = Time.time;
		} 
		else if (Input.GetKeyDown(KeyCode.S) && canMove) { // Backward movement
			//TurnBack();
			GoBackWards();
			lastMove = Time.time;
		} 
		else if (Input.GetKeyDown(KeyCode.D) && canMove) { // Right movement
			//TurnRight();
			GoRight();
			lastMove = Time.time;
		}
		else if (Input.GetKeyDown(KeyCode.A) && canMove) { // Left movement
			//TurnLeft();
			GoLeft();
			lastMove = Time.time;
		}
		Rotate ();
		Move ();
	}

	void Jump ()
	{
		//ToDo
	}

	void SwitchGravity ()
	{
		//ToDo
	}

	void GoForward ()
	{
		targetPosition = transform.position + Vector3.forward *moveDistance;
	}

	void GoBackWards ()
	{
		targetPosition = transform.position + Vector3.back*moveDistance;
	}

	void GoRight ()
	{
		targetPosition = transform.position + Vector3.right*moveDistance;
	}

	void GoLeft ()
	{
		targetPosition = transform.position + Vector3.left*moveDistance;
	}

	void Rotate ()
	{
		Vector3 targetDir = targetPosition - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	private void Move(){
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, strideSize);
	}
}
}