using UnityEngine;
using System.Collections;

public class Little : MonoBehaviour {

	public float chase_distance = 2;
    public float max_velocity = 1;
	private bool has_move_target = false;
	public Vector3 move_target;

	public Transform look_target = null;

	public Transform body_background;
	public Transform body_outline;
	public Transform eye_background;
	public Transform eye_outline;
	public Transform pupil;
	public Transform mouth_smile;
	public Transform mouth_woah;
	public Transform left_foot_moving_background;
	public Transform left_foot_moving_outline;
	public Transform right_foot_moving_background;
	public Transform right_food_moving_outline;
	public Transform feet_stationary_background;
	public Transform feet_stationary_outline;

	public Transform moving_specific;
	public Transform stationary_specific;

	private SpriteRenderer sr_body_background;
	private SpriteRenderer sr_body_outline;
	private SpriteRenderer sr_eye_background;
	private SpriteRenderer sr_eye_outline;
	private SpriteRenderer sr_pupil;
	private SpriteRenderer sr_mouth_smile;
	private SpriteRenderer sr_mouth_woah;
	private SpriteRenderer sr_left_foot_moving_background;
	private SpriteRenderer sr_left_foot_moving_outline;
	private SpriteRenderer sr_right_foot_moving_background;
	private SpriteRenderer sr_right_foot_moving_outline;
	private SpriteRenderer sr_feet_stationary_background;
	private SpriteRenderer sr_feet_stationary_outline;

	private Rigidbody2D r2d;
	private bool first_update = true;

	private void point_left(bool newval) {
		sr_body_background.flipX = newval;
		sr_body_outline.flipX = newval;
		sr_eye_background.flipX = newval;
		sr_eye_outline.flipX = newval;
		sr_pupil.flipX = newval;
		sr_mouth_smile.flipX = newval;
		sr_mouth_woah.flipX = newval;
		sr_left_foot_moving_background.flipX = newval;
		sr_left_foot_moving_outline.flipX = newval;
		sr_right_foot_moving_background.flipX = newval;
		sr_right_foot_moving_outline.flipX = newval;
		sr_feet_stationary_background.flipX = newval;
		sr_feet_stationary_outline.flipX = newval;
	}

	private void FirstUpdate() {
		sr_body_background = body_background.GetComponent<SpriteRenderer>();
		sr_body_outline = body_outline.GetComponent<SpriteRenderer>();
		sr_eye_background = eye_background.GetComponent<SpriteRenderer>();
		sr_eye_outline = eye_outline.GetComponent<SpriteRenderer>();
		sr_pupil = pupil.GetComponent<SpriteRenderer>();
		sr_mouth_smile = mouth_smile.GetComponent<SpriteRenderer>();
		sr_mouth_woah = mouth_woah.GetComponent<SpriteRenderer>();
		sr_left_foot_moving_background = left_foot_moving_background.GetComponent<SpriteRenderer>();
		sr_left_foot_moving_outline = left_foot_moving_outline.GetComponent<SpriteRenderer>();
		sr_right_foot_moving_background = right_foot_moving_background.GetComponent<SpriteRenderer>();
		sr_right_foot_moving_outline = right_food_moving_outline.GetComponent<SpriteRenderer>();
		sr_feet_stationary_background = feet_stationary_background.GetComponent<SpriteRenderer>();
		sr_feet_stationary_outline = feet_stationary_outline.GetComponent<SpriteRenderer>();

		point_left(false);
	}

	// Use this for initialization
	void Start () {
		r2d = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (first_update) {
			first_update = false;
			FirstUpdate();
		}

		// if the Big is too far away, run after it
		Vector3 big_pos = Common.game.big.transform.position;
		if (Mathf.Abs(big_pos.x - transform.position.x) > chase_distance) {
			move_target = big_pos;
			has_move_target = true;
		}

		// if we have a target
		if (has_move_target) {
			// if we haven't already reached it, move towards it
			float distance = Mathf.Abs(move_target.x - transform.position.x);
			//Common.debug("distance: " + distance.ToString());
			if (distance > 0.1) {
				if (move_target.x < transform.position.x) {
					r2d.velocity = new Vector2(-max_velocity, 0);
				}else {
					r2d.velocity = new Vector2(max_velocity, 0);
				}
			}else {
				// we just reached it!
				// what should happen now that we've arrived?
				has_move_target = false;
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
