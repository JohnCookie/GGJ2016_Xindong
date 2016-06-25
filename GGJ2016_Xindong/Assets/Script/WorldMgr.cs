using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMgr : MonoBehaviour
{
	public Transform charRoot;
	public GameObject m_prefabMccree;
	public GameObject m_prefabReinhart;

	public float enermyNearDistance = 60.0f;
	public float friendlyNearDistance = 40.0f;

	List<GameObject> atkSoldiers = new List<GameObject>();
	List<GameObject> defSoldiers = new List<GameObject>();

	bool underBattle = false;

	private static WorldMgr _instance;
	private WorldMgr(){
	}
	public static WorldMgr getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("WorldMgr").GetComponent<WorldMgr>();
		}
		return _instance;
	}

	// Use this for initialization
	void Start ()
	{

	}

	void Update(){
		if(atkSoldiers.Count<=0){
			underBattle=false;
		}
		if(defSoldiers.Count<=0){
			underBattle=false;
		}
		if(atkSoldiers.Count>0 && defSoldiers.Count>0){
			if(Vector3.Distance(atkSoldiers[0].transform.localPosition, defSoldiers[0].transform.localPosition)<enermyNearDistance){
				underBattle = true;
			}else{
				underBattle = false;
			}
		}
	}

	public void createPlayer(bool isAtk, int path, int mType){
		switch(mType){
		case 0:
			GameObject player = Instantiate(m_prefabMccree) as GameObject;
			player.transform.parent = charRoot;
			player.transform.localScale = Vector3.one;
			player.GetComponent<Mccree>().Init(path, isAtk);
			if(isAtk){
				player.name = "MccreeA"+atkSoldiers.Count;
				atkSoldiers.Add(player);
			}else{
				player.name = "MccreeB"+defSoldiers.Count;
				defSoldiers.Add(player);
			}
			break;
		case 1:
			GameObject player2 = Instantiate(m_prefabReinhart) as GameObject;
			player2.transform.parent = charRoot;
			player2.transform.localScale = Vector3.one;
			player2.GetComponent<Reinhart>().Init(path, isAtk);
			if(isAtk){
				player2.name = "ReinhartA"+atkSoldiers.Count;
				atkSoldiers.Add(player2);
			}else{
				player2.name = "ReinhartB"+defSoldiers.Count;
				defSoldiers.Add(player2);
			}
			break;
		}
	}

	public void destroyPlayer(GameObject obj){
		for(int i=0; i<atkSoldiers.Count; i++){
			if(atkSoldiers[i]==obj){
				bool result = atkSoldiers.Remove(obj);
			}
		}
		for(int i=0; i<defSoldiers.Count; i++){
			if(defSoldiers[i]==obj){
				defSoldiers.Remove(obj);
				bool result2 = defSoldiers.Remove(obj);
			}
		}
	}

	public bool checkFriendTooNear(GameObject obj, bool isAtk){
		bool isNear = false;
		List<GameObject> checkTeam = new List<GameObject>();
		if(isAtk){
			checkTeam = atkSoldiers;
		}else{
			checkTeam = defSoldiers;
		}
		int myIndex = checkTeam.IndexOf(obj);
		for(int i=0; i<myIndex; i++){
			if(Vector3.Distance(obj.transform.localPosition, checkTeam[i].transform.localPosition)<friendlyNearDistance && Vector3.Distance(obj.transform.localPosition, checkTeam[i].transform.localPosition)>0){
				//Debug.Log("Friend Too Near");
				isNear=true;
				break;
			}
		}
		return isNear;
	}
	public bool checkEnermyTooNear(GameObject obj, bool isAtk){
		bool isNear = false;
		List<GameObject> checkTeam = new List<GameObject>();
		if(isAtk){
			checkTeam = defSoldiers;
		}else{
			checkTeam = atkSoldiers;
		}
		for(int i=0; i<checkTeam.Count; i++){
			if(Vector3.Distance(obj.transform.localPosition, checkTeam[i].transform.localPosition)<enermyNearDistance){
				isNear=true;
				break;
			}
		}
		return isNear;
	}

	public bool getUnderBattle(){
		return underBattle;
	}

	public void makeDamage(bool isAtk, int damage){
		List<GameObject> checkTeam = new List<GameObject>();
		if(isAtk){
			checkTeam = defSoldiers;
		}else{
			checkTeam = atkSoldiers;
		}
		if(checkTeam.Count>0){
			if(checkTeam[0].GetComponent<Mccree>()!=null){
				checkTeam[0].GetComponent<Mccree>().getHurt(damage);
			}else if(checkTeam[0].GetComponent<Reinhart>()!=null){
				checkTeam[0].GetComponent<Reinhart>().getHurt(damage);
			}
		}
	}


	public void useSkill(bool isAtk, int field){
		if(isAtk){
			for(int i=0; i<atkSoldiers.Count; i++){
				if(atkSoldiers[i].GetComponent<Mccree>()!=null){
					if(atkSoldiers[i].GetComponent<Mccree>().getSoldierPath() == field && atkSoldiers[i].GetComponent<Mccree>().isSkillReady()>=0){
						Debug.Log("path: "+field+" Use Skill 0");
						atkSoldiers[i].GetComponent<Mccree>().useSkill();
						break;
					}
				}else if(atkSoldiers[i].GetComponent<Reinhart>()!=null){
					if(atkSoldiers[i].GetComponent<Reinhart>().getSoldierPath() == field && atkSoldiers[i].GetComponent<Reinhart>().isSkillReady()>=0){
						Debug.Log("path: "+field+" Use Skill 1");
						atkSoldiers[i].GetComponent<Reinhart>().useSkill();
						break;
					}
				}
			}
		}else{
			for(int i=0; i<defSoldiers.Count; i++){
				if(defSoldiers[i].GetComponent<Mccree>()!=null){
					if(defSoldiers[i].GetComponent<Mccree>().getSoldierPath() == field && defSoldiers[i].GetComponent<Mccree>().isSkillReady()>=0){
						Debug.Log("path: "+field+" Use Skill 0");
						defSoldiers[i].GetComponent<Mccree>().useSkill();
						break;
					}
				}else if(defSoldiers[i].GetComponent<Reinhart>()!=null){
					if(defSoldiers[i].GetComponent<Reinhart>().getSoldierPath() == field && defSoldiers[i].GetComponent<Reinhart>().isSkillReady()>=0){
						Debug.Log("path: "+field+" Use Skill 1");
						defSoldiers[i].GetComponent<Reinhart>().useSkill();
						break;
					}
				}
			}
		}
	}
}

