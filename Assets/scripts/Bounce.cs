using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    public float rate = 1;
    public float size = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = transform.position;
        v.x = size * Mathf.Sin(Time.time * rate);
        transform.position = v;
	}
}
