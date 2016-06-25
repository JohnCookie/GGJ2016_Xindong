using UnityEngine;
using System.Collections;

public class Lucio : MonoBehaviour {
	public GameObject m_model;
	public UILabel m_labelBlood;
	public UIProgressBar m_hpBar;
	public UIProgressBar m_energyBar;
	public Animation anim;

	public int currTargetIndex=0;
	public float speed = 5.0f;
	Vector3 startPos;
	Vector3 moveTarget;
	Vector3 moveTargetWorld;
	float currMoveTime=0.0f;
	int maxHp = 200;
	int currHp = 200;
	public int damage = 5;
	float atkInterval = 3.0f;
	float atkTimeRemain = 3.0f;

	float maxEnergy = 100;
	float currEnergy = 0;
	float recoverEnergyInterval = 0.5f;
	float recoverRemain = 0.5f;
	float recoverSpeed = 5;

	bool inited = false;
	int path = 2;
	bool isAtk = true;
	bool isLeader = false;
	bool skillReady = false;

	int status = 0; //0 move 1 attack

	// Use this for initialization
	void Awake () {

	}

	public void Init(int mpath, bool matk){
		currTargetIndex = 0;
		path = mpath;
		isAtk = matk;
		m_labelBlood.text = currHp.ToString();
		m_hpBar.value = (float)((float)currHp/(float)maxHp);
		m_energyBar.value = 0.0f;
		if(isAtk){
			transform.localPosition = PathMgr.getInstance().getAtkNextPath(currTargetIndex, path).localPosition;
		}else{
			transform.localPosition = PathMgr.getInstance().getDefNextPath(currTargetIndex, path).localPosition;
		}
		moveToNext();
		anim.wrapMode = WrapMode.Loop;
		anim.Play ("Walk");
		inited = true;
	}

	public void setLeader(bool leader){
		isLeader = leader;
	}
	// Update is called once per frame
	void Update () {
		if(inited){
			if(!WorldMgr.getInstance().getUnderBattle()){
				status = 0;
				anim.Play ("Walk");
			}

			// recover Energy
			recoverRemain-=Time.deltaTime;
			if(recoverRemain<0){
				currEnergy+=recoverSpeed;
				if(currEnergy>=maxEnergy){
					currEnergy=maxEnergy;
					skillReady = true;
				}
				recoverRemain=recoverEnergyInterval;
			}
			m_energyBar.value = currEnergy/maxEnergy;

			switch(status){
			case 0:
				//move
				if(Vector3.Distance(transform.localPosition, moveTarget)<0.1f){
					currMoveTime=0.0f;
					currTargetIndex+=1;
					if(currTargetIndex>=30){
						WorldMgr.getInstance().destroyPlayer(gameObject);
						Destroy(gameObject);
					}
					moveToNext();
				}else{
					if(WorldMgr.getInstance().checkFriendTooNear(gameObject, isAtk)){
						if(WorldMgr.getInstance().getUnderBattle()){
							if(status!=1){
								Debug.Log(gameObject.name+" Attack");
								status = 1;
								anim.Play ("Attack");
							}
						}else{
							status = 0;
							anim.Play ("Walk");
						}
						return;	
					}
					if(WorldMgr.getInstance().checkEnermyTooNear(gameObject, isAtk)){
						if(status!=1){
							Debug.Log(gameObject.name+" Attack");
							status = 1;
							anim.Play ("Attack");
						}
						return;
					}
					transform.localPosition =  Vector3.Lerp(startPos, moveTarget, currMoveTime/(Vector3.Distance(startPos, moveTarget)/speed) );
					currMoveTime+=Time.deltaTime;
				}
				break;
			case 1:
				//attack
				atkTimeRemain-=Time.deltaTime;
				if(atkTimeRemain<0){
					//make attack
					WorldMgr.getInstance().makeDamage(isAtk, damage);
					atkTimeRemain=atkInterval;
				}
				break;
			}
		}


		m_hpBar.transform.rotation = Camera.main.transform.rotation;
		m_energyBar.transform.rotation = Camera.main.transform.rotation;
		m_labelBlood.transform.rotation = Camera.main.transform.rotation;
	}

	void moveToNext(){
		startPos = transform.localPosition;
		if(isAtk){
			moveTarget = PathMgr.getInstance().getAtkNextPath(currTargetIndex+1, path).localPosition;
			moveTargetWorld = PathMgr.getInstance().getAtkNextPath(currTargetIndex+1, path).position;
		}else{
			moveTarget = PathMgr.getInstance().getDefNextPath(currTargetIndex+1, path).localPosition;
			moveTargetWorld = PathMgr.getInstance().getDefNextPath(currTargetIndex+1, path).position;
		}
		m_model.transform.LookAt(moveTargetWorld);
	}

	public void getHurt(int damage){
		currHp-=damage;
		m_labelBlood.text = currHp.ToString();
		m_hpBar.value = (float)((float)currHp/(float)maxHp);
		if(currHp<=0){
			WorldMgr.getInstance().destroyPlayer(gameObject);
			Destroy(gameObject);
		}
	}

	public int isSkillReady(){
		if(skillReady){
			return 0;
		}else{
			return -1;
		}
	}

	public int getSoldierPath(){
		return path;
	}

	public void useSkill(){
		WorldMgr.getInstance().makeDamage(isAtk, damage*2);
		currEnergy=0;
		skillReady = false;
		recoverRemain = recoverEnergyInterval;
		SkillMgr.getInstance ().ShowSkillCut (2);
	}
}
