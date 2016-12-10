using UnityEngine;
using System.Collections;

public class Common {
	static StateManager state;

	//static CameraController camera_controller;
	//static GameManager game_manager;
	static TimeManager time;

	//static DebugService debug;
	void debug(string message) {
		Log.debug(message);
	}
}
