using UnityEngine;
using System.Collections;

public class Obstacle {

    internal GameObject obj;
    internal GameObject observer;

	// Use this for initialization
	public Obstacle (GameObject obs, Vector3 pos) {
        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = pos;
        observer = obs;
	}
}
