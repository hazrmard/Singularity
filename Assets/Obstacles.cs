using UnityEngine;
using System.Collections;

public class Obstacle {

    private GameObject obj;                     // stores the game object
    private Main observer;                      // of type Main:MonoBehavior
    private float maxSpeed;                     // for calculating lorentz factors
    private float maxSpeed2;                    // to prevent recalculation of square
    private Vector3 velocity;                   // obstacle's velocity
    private Vector3 lorentz;                    // lorentz factor computed every frame

	// Use this for initialization
	public Obstacle (Main obs, Vector3 pos) {
        obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos;
        observer = obs;
        maxSpeed = observer.maxSpeed;
        maxSpeed2 = maxSpeed * maxSpeed;
        velocity = Vector3.zero;                // stationary obstacles for now
	}

    private void lengthContraction()
    {
        obj.transform.localScale = new Vector3(1 / lorentz.x, 1 / lorentz.y, 1 / lorentz.z);
    }

    private float lorentzFactor(float v)
    {
        return 1 / Mathf.Sqrt(1 - Mathf.Pow(v, 2) / maxSpeed2);
    }

    public void update()
    {
        Vector3 relVelocity = observer.absVelocity - velocity;
        lorentz.x = lorentzFactor(relVelocity.x);
        lorentz.y = lorentzFactor(relVelocity.y);
        lorentz.z = lorentzFactor(relVelocity.z);

        lengthContraction();
    }
}
