using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	bool first_update = true;
	public float initial_time_scale = 1;

	float target_time_scale;
	float lerp_ratio;

	public void LerpToTimeScale(float target, float ratio) {
		target_time_scale = target;
		lerp_ratio = ratio;
	}

	public void SetTimeScale(float target) {
		Time.timeScale = target;
		target_time_scale = target;
		lerp_ratio = 1;
	}

	// Use this for initialization
	void Start () {
		Common.time = this;
	}

	void FirstUpdate() {
		Time.timeScale = initial_time_scale;
		target_time_scale = initial_time_scale;
		lerp_ratio = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (first_update) {
			first_update = false;
			FirstUpdate();
		}

		Time.timeScale = Mathf.Lerp(Time.timeScale, target_time_scale, Time.unscaledDeltaTime * lerp_ratio);
	}
}
