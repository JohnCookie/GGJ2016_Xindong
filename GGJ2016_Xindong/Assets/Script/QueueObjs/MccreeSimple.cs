using UnityEngine;
using System.Collections;

public class MccreeSimple : MonoBehaviour {
	public GameObject m_model;
	public Animation m_anim;

	public int currTargetIndex=0;
	public float speed = 5.0f;
	Vector3 startPos;
	Vector3 moveTarget;
	Vector3 moveTargetWorld;
	float currMoveTime=0.0f;

	bool inited = false;
	int path = 2;
	bool isAtk = true;

	int status = 0; //0 move 1 prepare

	// Use this for initialization
	void Awake () {

	}

	public void Init(int mpath, bool matk){
		currTargetIndex = 0;
		path = mpath;
		isAtk = matk;
		if(isAtk){
			transform.localPosition = PathMgr.getInstance().getAtkPrepareNextPath(currTargetIndex).localPosition;
		}else{
			transform.localPosition = PathMgr.getInstance().getDefPrepareNextPath(currTargetIndex).localPosition;
		}
		moveToNext();
		inited = true;
	}

	// Update is called once per frame
	void Update () {
		if(inited){
			//move
			if(Vector3.Distance(transform.localPosition, moveTarget)<0.1f){
				currMoveTime=0.0f;
				currTargetIndex+=1;
				if(currTargetIndex>=20){
					status = 1;
				}
				moveToNext();
			}else{
				if(QueueMgr.getInstance().checkFriendTooNear(gameObject, isAtk)){
					return;	
				}
				transform.localPosition =  Vector3.Lerp(startPos, moveTarget, currMoveTime/(Vector3.Distance(startPos, moveTarget)/speed) );
				currMoveTime+=Time.deltaTime;
			}
		}
	}

	void moveToNext(){
		startPos = transform.localPosition;
		if(isAtk){
			moveTarget = PathMgr.getInstance().getAtkPrepareNextPath(currTargetIndex+1).localPosition;
			moveTargetWorld = PathMgr.getInstance().getAtkPrepareNextPath(currTargetIndex+1).position;
		}else{
			moveTarget = PathMgr.getInstance().getDefPrepareNextPath(currTargetIndex+1).localPosition;
			moveTargetWorld = PathMgr.getInstance().getDefPrepareNextPath(currTargetIndex+1).position;
		}
		m_model.transform.LookAt(moveTargetWorld);
	}

	public bool isPrepared(){
		return status == 1;
	}
}
