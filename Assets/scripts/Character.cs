using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Transform move_target = null;
    private Transform look_target = null;

    private Rigidbody2D r2d;

    // Use this for initialization
    void Start () {
        r2d = transform.GetComponent<Rigidbody2D>();
	}

    public void set_move_target(Transform new_target) {
        move_target = new_target;
    }
	
	// Update is called once per frame
	void Update () {
        // if we have a target
        if (move_target) {
            // if we haven't already reached it, move towards it
            float distance = Mathf.Abs(move_target.position.x - transform.position.x);
            Common.debug("distance: " + distance.ToString());
            if (distance > 0.2) {
                if (move_target.position.x < transform.position.x) {
                    r2d.velocity = new Vector2(-1, 0);
                }else {
                    r2d.velocity = new Vector2(1, 0);
                }
            }else {
                // we just reached it!
                // what should happen now that we've arrived?

                move_target = null;
                r2d.velocity = Vector2.zero;
            }
        }
	}
}

