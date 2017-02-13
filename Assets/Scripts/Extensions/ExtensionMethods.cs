using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods{

	public static Vector3 DirectionTo(this Vector3 value, Vector3 target){
		return (target - value).normalized;
	}
}
