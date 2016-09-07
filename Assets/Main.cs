using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public int num;
	public float speed;
	private GameObject[] obstacles;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRot;

	// Use this for initialization
	void Start () {
        originalRot = transform.localRotation;
		obstacles = new GameObject[num];
		int i;
		for (i = 0; i < num; i++) {
			obstacles [i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			//obstacles [i].AddComponent<Rigidbody> ();
			obstacles [i].transform.position = new Vector3 (Random.Range (-5f, 5f), Random.Range (-5f, 5f), Random.Range (0f, 5f));
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W))
        {
			transform.Translate (0, 0, 1 * Time.deltaTime * speed);
		}
        else if (Input.GetKey (KeyCode.S))
        {
			transform.Translate (0, 0, -1 * Time.deltaTime * speed);
		}
        if (Input.GetKey (KeyCode.Space))
        {
            transform.Translate(0, 1 * Time.deltaTime * speed, 0);
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            transform.Translate(0, -1 * Time.deltaTime * speed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(1 * Time.deltaTime * speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
        }

        rotationX += Input.GetAxis("Mouse X") * speed;
        rotationY += Input.GetAxis("Mouse Y") * speed;
        rotationY = Mathf.Clamp(rotationY, -90F, 90F);
        Quaternion qX = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion qY = Quaternion.AngleAxis(rotationY, Vector3.left);
        transform.localRotation = originalRot * qX * qY;
    }
}
