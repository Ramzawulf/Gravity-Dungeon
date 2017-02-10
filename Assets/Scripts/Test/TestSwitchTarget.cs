using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitchTarget : MonoBehaviour, ISwitchTriggered {

	#region ISwitchTriggered implementation


	public void OnSwitchOn ()
	{
		StartCoroutine (Shrink());
	}

	public void OnSwitchOff ()
	{
		StartCoroutine (Grow());
	}

	private IEnumerator Shrink(){

		for (int i = 0; i < 10; i++) {
			transform.localScale = transform.localScale * 0.5f;
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator Grow(){

		for (int i = 0; i < 10; i++) {
			transform.localScale = transform.localScale * 2f;
			yield return new WaitForSeconds(0.1f);
		}
		transform.localScale = new Vector3 (1,1,1);
	}

	#endregion


}
