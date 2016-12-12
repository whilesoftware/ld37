using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public Transform game_logo;
    public Character big;
    public Character little;

    public Transform grass_prefab;
    public float grass_delta;
    public float grass_count;
    public float grass_rangex;
    public float grass_startx;
    public float grass_starty;
    public float grass_rangey;

    public float grass2_delta;
    public float grass2_count;
    public float grass2_rangex;
    public float grass2_startx;
    public float grass2_starty;
    public float grass2_rangey;

    private bool first_update = true;

	// Use this for initialization
	void Start () {
        Common.game = this;
	}

    void FirstUpdate() {
        // let's subscribe to all the big game state notifications
        Common.state.StateChange += handleStateChange;
        for(int n=0; n < grass_count; n++) {
            float x = grass_startx + grass_delta * n + Random.Range(-grass_rangex, grass_rangex);
            float y = grass_starty + Random.Range(-grass_rangey, grass_rangey);

            Transform newgrass = (Transform) Instantiate(grass_prefab, new Vector3(x, y, 0), Quaternion.identity);
            Animator anim = newgrass.GetComponent<Animator>();
            anim.Play("grass", -1, Random.Range(0f, 1f));
        }

        for (int n = 0; n < grass2_count; n++) {
            float x = grass2_startx + grass2_delta * n + Random.Range(-grass2_rangex, grass2_rangex);
            float y = grass2_starty + Random.Range(-grass2_rangey, grass2_rangey);

            Transform newgrass = (Transform)Instantiate(grass_prefab, new Vector3(x, y, 0), Quaternion.identity);
            Animator anim = newgrass.GetComponent<Animator>();
            anim.Play("grass", -1, Random.Range(0f, 1f));
        }
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
                //game_logo.gameObject.SetActive(false);
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
 