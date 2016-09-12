using UnityEngine;
using System.Collections;

public class Obstacle {

    private GameObject obj;                     // stores the game object
    private Main observer;                      // of type Main:MonoBehavior
    private float maxSpeed;                     // for calculating lorentz factors
    private float maxSpeed2;                    // to prevent recalculation of square
    private Vector3 velocity = Vector3.zero;    // obstacle's velocity in local frame
    private Vector3 lorentz = Vector3.one;      // lorentz factor computed every frame (local)
    private Vector3 absPosition;                // coordinates in rest frame

    private Vector3 relVelocity;                // obs v relative to self in self's coords
    private Vector3 relPosition;                // obs pos relative to self in self's coords

	// Use this for initialization
	public Obstacle (Main obs, Vector3 pos) {
        obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos;
        absPosition = pos;
        observer = obs;
        maxSpeed = observer.maxSpeed;
        maxSpeed2 = maxSpeed * maxSpeed;
        velocity = Vector3.zero;                // stationary obstacles for now
	}

    private void lengthContraction()
    {
        Vector3 invLorentz = new Vector3(1 / lorentz.x, 1 / lorentz.y, 1 / lorentz.z);
        obj.transform.localScale = invLorentz;
        Vector3 relP = relativePosition();
        Vector3 moveRel = relP - Vector3.Scale(invLorentz, relP);
        obj.transform.position = absPosition + moveRel;
    }

    public void update()
    {   
        // first calculate all relative parameters
        relVelocity = relativeVelocity(observer.absVelocity);
        lorentz.x = lorentzFactor(relVelocity.x);
        lorentz.y = lorentzFactor(relVelocity.y);
        lorentz.z = lorentzFactor(relVelocity.z);

        relPosition = relativePosition();

        // implement relativistic effects based on calculated parameters
        lengthContraction();
    }

    private Vector3 relativeVelocity(Vector3 otherVelocity)
    {
        Vector3 deltaV = otherVelocity - absoluteVelocity();
        Vector3 denominator = new Vector3(1, 1, 1) - Vector3.Scale(otherVelocity, velocity) / maxSpeed2;
        return obj.transform.InverseTransformDirection(Vector3.Scale(deltaV, new Vector3(1/denominator.x, 1/denominator.y, 1/denominator.z)));
    }

    private Vector3 absoluteVelocity()
    {
        return obj.transform.TransformDirection(velocity);
    }

    private Vector3 relativePosition()
    {
        return obj.transform.InverseTransformDirection(observer.gameObject.transform.position - absPosition);
    }

    private float lorentzFactor(float v)
    {
        return 1 / Mathf.Sqrt(1 - Mathf.Pow(v, 2) / maxSpeed2);
    }
}
