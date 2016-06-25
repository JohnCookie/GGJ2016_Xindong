using UnityEngine;
using System.Collections;

public class InputDector : MonoBehaviour
{
	Vector3 cameraForward = new Vector3 (0, 0, 0.1f);
	Vector3 cameraBackward = new Vector3 (0, 0, -0.1f);
	Vector3 cameraLeft = new Vector3 (-0.1f, 0, 0);
	Vector3 cameraRight = new Vector3 (0.1f, 0, 0);

	// Update is called once per frame
	void Update ()
	{
		// create soldier
		if (Input.GetKeyDown (KeyCode.Q)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (true);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (true, 1, type);
			}
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (true);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (true, 2, type);
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (true);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (true, 3, type);
			}
		}

		if (Input.GetKeyDown (KeyCode.U)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (false);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (false, 1, type);
			}
		}
		if (Input.GetKeyDown (KeyCode.I)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (false);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (false, 2, type);
			}
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			int type = QueueMgr.getInstance ().TryGetPrepared (false);
			if (type >= 0) {
				WorldMgr.getInstance ().createPlayer (false, 3, type);
			}
		}

		// use skill
		if (Input.GetKeyDown (KeyCode.A)) {
			WorldMgr.getInstance ().useSkill (true, 1);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			WorldMgr.getInstance ().useSkill (true, 2);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			WorldMgr.getInstance ().useSkill (true, 3);
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			WorldMgr.getInstance ().useSkill (false, 1);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			WorldMgr.getInstance ().useSkill (false, 2);
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			WorldMgr.getInstance ().useSkill (false, 3);
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			Camera.main.transform.position += cameraForward;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Camera.main.transform.position += cameraBackward;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Camera.main.transform.position += cameraLeft;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Camera.main.transform.position += cameraRight;
		}
	}
}

