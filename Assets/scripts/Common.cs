using UnityEngine;
using System.Collections;

public class Common {
	public static StateManager state;

    //static CameraController camera_controller;
    public static Game game;
	public static TimeManager time;

	//static DebugService debug;
	public static void debug(string message) {
		Debug.Log(message);
	}
}
