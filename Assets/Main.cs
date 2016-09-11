using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public int num;                                                     // # of obstacles
	public float maxSpeed;
    public float sensitivity;                                           // speed of rotations
    public float acceleration;                                          // ship acceleration

    private float deceleration;                                         // ship deceleration
    private Obstacle[] obstacles;                                       // stores all obstacle objects
    private Transform ship;                                             // handle to ship for tilting
    private float rotationX = 0F;                                       // Global ship orientation angles
    private float rotationY = 0F;
    private float rotationZ = 0F;
    private float tiltRotZ = 0F;                                        // Local ship tilt angles 
    private float tiltRotY = 0F;
    private float tiltRotX = 0F;
    private Vector3 accelDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;                            // v in local frame of reference
    public Vector3 absVelocity = Vector3.zero;                        // v in world space
    private float rotationSpeed;                                        // speed of rotation of ship

	// Use this for initialization
	void Start () {
        obstacles = new Obstacle[num];
        ship = transform.Find("Ship");
        deceleration = acceleration / maxSpeed;
        rotationSpeed = maxSpeed * sensitivity;

		int i;
		for (i = 0; i < num; i++) {
            obstacles[i] = new Obstacle(this, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        accelDir = Vector3.zero;
		if (Input.GetKey (KeyCode.W))                                   // W KEY
        {
            accelDir.z = 1;
		}
        else if (Input.GetKey (KeyCode.S))                              // S KEY
        {
			accelDir.z = -1;
		}
        if (Input.GetKey (KeyCode.Space))                               // SPACEBAR
        {
            accelDir.y = 1;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))                         // L ALT KEY
        {
            accelDir.y = -1;
        }
        if (Input.GetKey(KeyCode.D))                                    // D KEY
        {
            //accelDir.x = 1;
            rotationY += rotationSpeed * Time.deltaTime * Time.deltaTime;
            tiltRotY += (tiltRotY + 15F) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))                               // A KEY
        {
            //accelDir.x = -1;
            rotationY -= rotationSpeed * Time.deltaTime * Time.deltaTime;
            tiltRotY -= (15F - tiltRotY) * Time.deltaTime;
        }
        else
        {
            tiltRotY -= tiltRotY * deceleration * Time.deltaTime;
            rotationY -= rotationY * deceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))                           // RIGHT ARROW
        {
            rotationZ -= rotationSpeed * Time.deltaTime * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))                       // LEFT ARROW
        {
            rotationZ += rotationSpeed * Time.deltaTime * Time.deltaTime;
        }
        else
        {
            rotationZ -= rotationZ * deceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))                              // UP ARROW
        {
            rotationX += rotationSpeed * Time.deltaTime * Time.deltaTime;
            tiltRotX += (tiltRotX + 15F) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))                       // DOWN ARROW
        {
            rotationX -= rotationSpeed * Time.deltaTime * Time.deltaTime;
            tiltRotX -= (15F - tiltRotX) * Time.deltaTime;
        }
        else
        {
            tiltRotX -= tiltRotX * deceleration * Time.deltaTime;
            rotationX -= rotationX * deceleration * Time.deltaTime;
        }

        rotationX %= 360;                                               // handle >360 angles
        rotationY %= 360;
        rotationZ %= 360;

        // Ship tilting effects
        tiltRotZ = Mathf.Clamp(tiltRotZ, -15F, 15F);                    // limit how far ship tilts
        tiltRotY = Mathf.Clamp(tiltRotY, -15F, 15F);
        tiltRotX = Mathf.Clamp(tiltRotX, -15F, 15F);
        Quaternion qTZ = Quaternion.AngleAxis(tiltRotZ, Vector3.forward);
        Quaternion qTY = Quaternion.AngleAxis(tiltRotY, Vector3.up);
        Quaternion qTX = Quaternion.AngleAxis(tiltRotX, Vector3.right);
        ship.localRotation = qTX * qTY * qTZ;

        // ship orientation effects
        transform.Rotate(rotationX, rotationY, rotationZ);

        // ship movement effects
        accelerate();

        absVelocity = absoluteVelocity();
        foreach (Obstacle o in obstacles)
        {
            o.update();
        }
    }


    void accelerate()
    {
        velocity += ((acceleration * accelDir.normalized) - (velocity * deceleration)) * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        gameObject.transform.Translate(velocity * Time.deltaTime);
    }

    Vector3 absoluteVelocity()
    {
        return transform.TransformDirection(velocity);
    }
}
