using UnityEngine;
using System.Collections;

public class SkillMgr : MonoBehaviour
{
	public GameObject m_skillObj;
	public Transform root;

	private static SkillMgr _instance;
	private SkillMgr(){
	}
	public static SkillMgr getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("SkillMgr").GetComponent<SkillMgr>();
		}
		return _instance;
	}

	// Use this for initialization
	public void ShowSkillCut(int type){
		GameObject obj = Instantiate (m_skillObj) as GameObject;
		obj.transform.parent = root.transform;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = Vector3.zero;

		obj.GetComponent<SkillObj> ().Init (type);
	}
}

