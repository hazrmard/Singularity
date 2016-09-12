/* Originally taken and modified from:
 * https://gist.github.com/ftvs/5822103
 * */

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount;
    private float currShakeAmount;
    public float decreaseFactor;
    private float currDecreaseFactor;

    Vector3 originalPos;

    void Awake()
    {
        currShakeAmount = 0F;                       // start w/ 0 shake
        currDecreaseFactor = decreaseFactor;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (0F <= currShakeAmount && currShakeAmount <= shakeAmount)
            {
                camTransform.localPosition += Random.insideUnitSphere * currShakeAmount;
                currShakeAmount += Time.deltaTime * currDecreaseFactor;
            } else
            {
                if (currShakeAmount < 0F) { return; }
                currDecreaseFactor = -0.5f * decreaseFactor;
            }
        } else
        {
            currShakeAmount = 0F;
            currDecreaseFactor = decreaseFactor;
        }
    }
}