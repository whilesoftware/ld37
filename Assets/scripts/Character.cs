using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public Transform move_target = null;
    public Transform look_target = null;

    public Transform body;
    public Transform body_outline;
    public Transform eye_background;
    public Transform eye_outline;
    public Transform pupil;
    public Transform mouth;
    public Transform left_foot_moving_background;
    public Transform left_foot_moving_outline;
    public Transform right_foot_moving_background;
    public Transform right_food_moving_outline;

    public Transform left_pupil;
    public Transform right_pupil;
    public Transform eyes_centered;
    public Transform mouth_centered;
    public Transform left_foot_stationary;
    public Transform right_foot_stationary;

    public Transform moving_specific;
    public Transform stationary_specific;

    private SpriteRenderer sr_body;
    private SpriteRenderer sr_body_outline;
    private SpriteRenderer sr_eye_background;
    private SpriteRenderer sr_eye_outline;
    private SpriteRenderer sr_pupil;
    private SpriteRenderer sr_mouth;
    private SpriteRenderer sr_left_foot_moving_background;
    private SpriteRenderer sr_left_foot_moving_outline;
    private SpriteRenderer sr_right_foot_moving_background;
    private SpriteRenderer sr_right_foot_moving_outline;

    private SpriteRenderer sr_left_pupil;
    private SpriteRenderer sr_right_pupil;
    private SpriteRenderer sr_eyes;
    private SpriteRenderer sr_mouth_centered;
    private SpriteRenderer sr_left_foot_stationary;
    private SpriteRenderer sr_right_foot_stationary;

    private Rigidbody2D r2d;
    private bool first_update = true;

    // Use this for initialization
    void Start () {
        r2d = transform.GetComponent<Rigidbody2D>();
	}

    public void set_move_target(Transform new_target) {
        move_target = new_target;
    }

    private void point_left(bool newval) {
        sr_body.flipX = newval;
        sr_body_outline.flipX = newval;
        sr_eye_background.flipX = newval;
        sr_eye_outline.flipX = newval;
        sr_pupil.flipX = newval;
        sr_mouth.flipX = newval;
        sr_left_foot_moving_background.flipX = newval;
        sr_left_foot_moving_outline.flipX = newval;
        sr_right_foot_moving_background.flipX = newval;
        sr_right_foot_moving_outline.flipX = newval;

        sr_left_pupil.flipX = newval;
        sr_right_pupil.flipX = newval;
        sr_eyes.flipX = newval;
        sr_mouth_centered.flipX = newval;
        sr_left_foot_stationary.flipX = newval;
        sr_right_foot_stationary.flipX = newval;
    }

    private void FirstUpdate() {
        sr_body = body.GetComponent<SpriteRenderer>();
        sr_body_outline = body_outline.GetComponent<SpriteRenderer>();
        sr_eye_background = eye_background.GetComponent<SpriteRenderer>();
        sr_eye_outline = eye_outline.GetComponent<SpriteRenderer>();
        sr_pupil = pupil.GetComponent<SpriteRenderer>();
        sr_mouth = mouth.GetComponent<SpriteRenderer>();
        sr_left_foot_moving_background = left_foot_moving_background.GetComponent<SpriteRenderer>();
        sr_left_foot_moving_outline = left_foot_moving_outline.GetComponent<SpriteRenderer>();
        sr_right_foot_moving_background = right_foot_moving_background.GetComponent<SpriteRenderer>();
        sr_right_foot_moving_outline = right_food_moving_outline.GetComponent<SpriteRenderer>();

        sr_left_pupil = left_pupil.GetComponent<SpriteRenderer>();
        sr_right_pupil = right_pupil.GetComponent<SpriteRenderer>();
        sr_eyes = eyes_centered.GetComponent<SpriteRenderer>();
        sr_mouth_centered = mouth_centered.GetComponent<SpriteRenderer>();
        sr_left_foot_stationary = left_foot_stationary.GetComponent<SpriteRenderer>();
        sr_right_foot_stationary = right_foot_stationary.GetComponent<SpriteRenderer>();

        point_left(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (first_update) {
            first_update = false;
            FirstUpdate();
        }
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

        if (r2d.velocity.x > 0) {
            point_left(false);
        } else if (r2d.velocity.x < 0) {
            point_left(true);
        }

        moving_specific.gameObject.SetActive(r2d.velocity.x != 0);
        stationary_specific.gameObject.SetActive(r2d.velocity.x == 0);
        
    }
}

