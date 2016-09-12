using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {
    private Main player;
    private UnityEngine.UI.Text speedLabel;

	// Use this for initialization
	void Start ()
    {
        player = gameObject.GetComponent<Main>();
        speedLabel = player.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float s = Mathf.Floor(100 * player.absVelocity.magnitude / player.maxSpeed) / 100;
        speedLabel.text = "Speed = " + s.ToString("F2") + "c";
	}
}
