using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	public delegate void StateChangeEvent();
	public event StateChangeEvent Loading, Pregame, Running, Paused, Endgame, Quitting, StateChange;

	private int current_state = NULL;
	private float state_start_time;

	public const int NULL = -3;
	public const int PRELOAD = -2;
	public const int LOADING = -1;
	public const int PREGAME = 0;
	public const int RUNNING = 1;
	public const int ENDGAME = 4;
	public const int PAUSED = 5;
	public const int QUITTING = 7;
	public const int SHUTDOWN = 8;

	private bool first_update = true;
	private bool shutdown_on_next_pass = false;

	public int get() {
		return current_state;
	}
	public float get_start_time() {
		return state_start_time;
	}

	public void set(int new_state) {
		current_state = new_state;
		state_start_time = Time.time;

		switch(current_state) {
		case LOADING:
			Common.debug("LOADING");
			if (Loading != null) Loading();
			break;
		case PREGAME:
			Common.debug("PREGAME");
			if (Pregame != null) Pregame();
			break;
		case RUNNING:
			Common.debug("RUNNING");
			if (Running != null) Running();
			break;
		case PAUSED:
			Common.debug("PAUSED");
			if (Paused != null) Paused();
			break;
		case ENDGAME:
			Common.debug("ENDGAME");
			if (Endgame != null) Endgame();
			break;
		case QUITTING:
			Common.debug("QUITTING");
			if (Quitting != null) Quitting();
			break;
		}

		// now fire the generic state_changed event
		if (StateChange != null) StateChange();
	}


	// Use this for initialization
	void Start () {
		Common.state = this;

		set(PRELOAD);
	}

	void FirstUpdate() {
	}
	
	// Update is called once per frame
	void Update () {
		if (first_update) {
			first_update = false;
			FirstUpdate();
		}

		if (shutdown_on_next_pass) {
			set(SHUTDOWN);
		}

		
		switch(current_state) {
		case PRELOAD:
			set(LOADING);
			break;
		case LOADING:
			set(PREGAME);
			break;
		case RUNNING:
			break;
		case PAUSED:
			break;
		case ENDGAME:
			break;
		case QUITTING:
			shutdown_on_next_pass = true;
			break;
		}
	}

}

