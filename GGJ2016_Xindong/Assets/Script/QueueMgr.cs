using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueMgr : MonoBehaviour
{
	List<GameObject> atkSoldiers = new List<GameObject>();
	List<GameObject> defSoldiers = new List<GameObject>();

	public Transform charRoot;

	public GameObject m_prefabMccree;
	public GameObject m_prefabReinhart;
	List<GameObject> m_prefabList =  new List<GameObject>();

	float createTimeRemain = 3.0f;
	float createInterval = 3.0f;

	float friendlyNearDistance = 40.0f;

	private static QueueMgr _instance;
	private QueueMgr(){
	}
	public static QueueMgr getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("QueueMgr").GetComponent<QueueMgr>();
		}
		return _instance;
	}

	void Awake(){
		m_prefabList.Add(m_prefabMccree);
		m_prefabList.Add(m_prefabReinhart);
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		createTimeRemain-=Time.deltaTime;
		if(createTimeRemain<=0){
			if(atkSoldiers.Count<15){
				int type = Random.Range(0, m_prefabList.Count);
				createSoldier(true,type);
			}
			if(defSoldiers.Count<15){
				int type = Random.Range(0, m_prefabList.Count);
				createSoldier(false,type);
			}
			createTimeRemain = createInterval;
		}


	}

	void createSoldier(bool isAtk, int type){
		GameObject player = null;
		switch(type){
		case 0:
			player = Instantiate(m_prefabMccree) as GameObject;
			break;
		case 1:
			player = Instantiate(m_prefabReinhart) as GameObject;
			break;
		default:
			player = Instantiate(m_prefabMccree) as GameObject;
			break;
		}

		player.transform.parent = charRoot;
		player.transform.localScale = Vector3.one;
		if(isAtk){
			switch(type){
			case 0:
				player.GetComponent<MccreeSimple>().Init(4, isAtk);
				break;
			case 1:
				player.GetComponent<ReinhartSimple>().Init(4, isAtk);
				break;
			default:
				player.GetComponent<MccreeSimple>().Init(4, isAtk);
				break;
			}
		}else{
			switch(type){
			case 0:
				player.GetComponent<MccreeSimple>().Init(5, isAtk);
				break;
			case 1:
				player.GetComponent<ReinhartSimple>().Init(5, isAtk);
				break;
			default:
				player.GetComponent<MccreeSimple>().Init(5, isAtk);
				break;
			}
		}

		if(isAtk){
			player.name = "QueueSoldierA"+atkSoldiers.Count;
			atkSoldiers.Add(player);
		}else{
			player.name = "QueueSoldierB"+defSoldiers.Count;
			defSoldiers.Add(player);
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

	public int TryGetPrepared(bool isAtk){
		if(isAtk){
			if(atkSoldiers.Count>0){
				bool firstPrepared = false;
				int createType = 0;
				if(atkSoldiers[0].GetComponent<MccreeSimple>()!=null){
					firstPrepared = atkSoldiers[0].GetComponent<MccreeSimple>().isPrepared();
					createType = 0;
				}else if(atkSoldiers[0].GetComponent<ReinhartSimple>()!=null){
					firstPrepared = atkSoldiers[0].GetComponent<ReinhartSimple>().isPrepared();
					createType = 1;
				}
				if(firstPrepared){
					Destroy(atkSoldiers[0].gameObject);
					atkSoldiers.RemoveAt(0);
					return createType;
				}else{
					return -1;
				}
			}else{
				return -1;
			}
		}else{
			if(defSoldiers.Count>0){
				bool firstPrepared = false;
				int createType = 0;
				if(defSoldiers[0].GetComponent<MccreeSimple>()!=null){
					firstPrepared = defSoldiers[0].GetComponent<MccreeSimple>().isPrepared();
					createType = 0;
				}else if(defSoldiers[0].GetComponent<ReinhartSimple>()!=null){
					firstPrepared = defSoldiers[0].GetComponent<ReinhartSimple>().isPrepared();
					createType = 1;
				}
				if(firstPrepared){
					Destroy(defSoldiers[0].gameObject);
					defSoldiers.RemoveAt(0);
					return createType;
				}else{
					return -1;
				}
			}else{
				return -1;
			}
		}
	}
}

