using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public Transform game_logo;
    public Character big;
    public Character little;

    private bool first_update = true;

	// Use this for initialization
	void Start () {
        Common.game = this;
	}

    void FirstUpdate() {
        // let's subscribe to all the big game state notifications
        Common.state.StateChange += handleStateChange;
    }

    private void handleStateChange() {
        int newstate = Common.state.get();

        Common.debug("game saw state change to: " + newstate.ToString());

        switch(newstate) {
            case StateManager.LOADING:
                break;
            case StateManager.PREGAME:
                break;
            case StateManager.RUNNING:
                // hide the logo
                game_logo.gameObject.SetActive(false);
                break;
            case StateManager.PAUSED:
                break;
            case StateManager.ENDGAME:
                break;
            case StateManager.QUITTING:
                break;
        }
    }

    // Update is called once per frame
    void Update () {
	    if (first_update) {
            first_update = false;
            FirstUpdate();
        }

        switch(Common.state.get()) {
            case StateManager.LOADING:
                break;
            case StateManager.PREGAME:
                // tap to start (once we've been in this state for a moment or two
                if (Time.time - Common.state.get_start_time() > 1) {
                    if (Input.GetMouseButtonDown(0)) {
                        Common.state.set(StateManager.RUNNING);
                    }
                }
                break;
            case StateManager.RUNNING:
                // process click events to move the simulation along
                if (Input.GetMouseButtonDown(0)) {
                    // did the player click on a known object?

                    RaycastHit2D hit = Physics2D.Raycast(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero);

                    if (hit.collider) {
                        if (hit.collider.tag == "clicktarget") {
                            Common.debug("user clicked on a target: " + hit.collider.transform.name);
                            big.set_move_target(hit.collider.transform);
                        }
                    }
                }
                break;
            case StateManager.PAUSED:
                break;
            case StateManager.ENDGAME:
                break;
            case StateManager.QUITTING:
                break;
        }
	}
}
 