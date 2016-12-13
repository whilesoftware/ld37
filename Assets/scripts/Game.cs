using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public class ClickTarget {
        public Transform foreground;
        public Transform background;

        public Animator foreground_anim;
        public Animator background_anim;

        public Color background_color;

        public SpriteRenderer foreground_sprite_renderer;
        public SpriteRenderer background_sprite_renderer;

        public string foreground_clipname;
        public string background_clipname;

        public bool is_hovered = false;

        public ClickTarget(Transform _foreground) {
            foreground = _foreground;
            foreground_anim = foreground.GetComponent<Animator>();
            foreground_sprite_renderer = foreground.GetComponent<SpriteRenderer>();
            foreach (AnimationClip clip in foreground_anim.runtimeAnimatorController.animationClips) {
                foreground_clipname = clip.name;
            }

            if (foreground.childCount > 0) {
                background = _foreground.GetChild(0);
                background_anim = background.GetComponent<Animator>();
                background_sprite_renderer = background.GetComponent<SpriteRenderer>();
                background_color = background_sprite_renderer.color;
                foreach (AnimationClip clip in background_anim.runtimeAnimatorController.animationClips) {
                    background_clipname = clip.name;
                }
            }

            
        }

        public void set_animate(bool newstate) {
            Common.debug("new animate state: " + foreground.name + " - " + newstate);
            if (newstate) {
                foreground_anim.speed = 1;
                if (background_anim != null) {
                    background_anim.speed = 1;
                }
            }else {
                foreground_anim.speed = 0;
                if (background_anim != null) {
                    background_anim.speed = 0;
                }
            }
        }

        public void toggle_animate() {

        }

        public void show_background(bool newstate) {
            if (background_sprite_renderer != null) {
                background_sprite_renderer.enabled = newstate;
            }
        }
        public void show_foreground(bool newstate) {
            foreground_sprite_renderer.enabled = newstate;
        }

        public void set_background_color(Color newcolor) {
            background_color = newcolor;
            background_sprite_renderer.color = background_color;
        }
    }

    public Transform game_logo;
    public Character big;
    public Little little;

    public Transform big_tree;
    public Transform little_tree;
    public Transform big_bed;
    public Transform little_bed;
    public Transform bedside_table;
    public Transform books;
    public Transform dresser;
    public Transform shower;
    public Transform toilet;
    public Transform vanity;
    public Transform stove;
    public Transform chair;
    public Transform toys;
    public Transform fan;

    public Transform floor;
    public Transform roof;
    public Transform right_wall;
    public Transform left_wall;

    private bool first_update = true;

    private Dictionary<string, ClickTarget> click_targets;

    private void add_target(Transform t) {
        click_targets.Add(t.name, new ClickTarget(t));
    }

	// Use this for initialization
	void Start () {
        Common.game = this;

        // create a hash of the various click targets
        click_targets = new Dictionary<string, ClickTarget>();
        add_target(big_tree);
        add_target(little_tree);
        add_target(big_bed);
        add_target(little_bed);
        add_target(bedside_table);
        add_target(books);
        add_target(dresser);
        add_target(shower);
        add_target(toilet);
        add_target(vanity);
        add_target(stove);
        add_target(chair);
        add_target(toys);
        add_target(fan);
    }

    void FirstUpdate() {
        // let's subscribe to all the big game state notifications
        Common.state.StateChange += handleStateChange;
    }

    void stop_all_target_animators() {
        foreach(ClickTarget ct in click_targets.Values) {
            ct.set_animate(false);
        }
    }

    void hide_all_backgrounds() {
        foreach (ClickTarget ct in click_targets.Values) {
            ct.show_background(false);
        }
    }

    void stop_house_animations() {
        floor.GetComponent<Animator>().speed = 0;
        roof.GetComponent<Animator>().speed = 0;
        right_wall.GetComponent<Animator>().speed = 0;
        left_wall.GetComponent<Animator>().speed = 0;
    }

    private void handleStateChange() {
        int newstate = Common.state.get();

        Common.debug("game saw state change to: " + newstate.ToString());

        switch(newstate) {
            case StateManager.LOADING:
                break;
            case StateManager.PREGAME:
                stop_all_target_animators();
                hide_all_backgrounds();
                stop_house_animations();
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
                            //ClickTarget ct = click_targets[hit.transform.name];
                            //ct.set_animate(true);
                        }
                    }
                }else {
                    // no buttons pressed at the moment

                    ClickTarget hover_target = null;

                    // show the background graphic and animation for anything that is hovered
                    RaycastHit2D hit = Physics2D.Raycast(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero);

                    if (hit.collider) {
                        if (hit.collider.tag == "clicktarget") {
                            hover_target = click_targets[hit.transform.name];
                        }
                    }

                    foreach(ClickTarget ct in click_targets.Values) {
                        if (ct == hover_target) {
                            if (ct.is_hovered) {
                                // we're already tracking this one
                            }else {
                                // start animating
                                ct.show_background(true);
                                ct.set_animate(true);
                            }
                        }else {
                            if (ct.is_hovered) {
                                ct.show_background(false);
                                ct.set_animate(false);
                            }
                        }

                        ct.is_hovered = (ct == hover_target);
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
 