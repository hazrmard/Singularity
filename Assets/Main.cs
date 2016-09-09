using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public int num;
	public float maxSpeed;
    public float acceleration;
    public float deceleration;
    private Obstacle[] spheres;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRot;
    private Vector3 accelDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        originalRot = transform.localRotation;
        spheres = new Obstacle[num];

		int i;
		for (i = 0; i < num; i++) {
            spheres[i] = new Obstacle(this.gameObject, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(0f, 10f)));
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        accelDir = Vector3.zero;
		if (Input.GetKey (KeyCode.W))
        {
            accelDir.z = 1;
		}
        else if (Input.GetKey (KeyCode.S))
        {
			accelDir.z = -1;
		}
        if (Input.GetKey (KeyCode.Space))
        {
            accelDir.y = 1;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            accelDir.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            accelDir.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            accelDir.x = -1;
        }

        accelerate();

        rotationX += Input.GetAxis("Mouse X") * maxSpeed;
        rotationY += Input.GetAxis("Mouse Y") * maxSpeed;
        rotationY = Mathf.Clamp(rotationY, -90F, 90F);
        Quaternion qX = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion qY = Quaternion.AngleAxis(rotationY, Vector3.left);
        transform.localRotation = originalRot * qX * qY;
    }


    void accelerate()
    {
        velocity += ((acceleration * accelDir.normalized) - (velocity * deceleration)) * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        gameObject.transform.Translate(velocity * Time.deltaTime);
    }
}
