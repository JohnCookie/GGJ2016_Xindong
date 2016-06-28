using UnityEngine;
using System.Collections;

public class EffectMgr : MonoBehaviour
{
	public GameObject m_fireEffect;
	public GameObject m_aoeEffect;
	public GameObject m_healEffect;

	public Transform root;

	private static EffectMgr _instance;
	private EffectMgr(){
	}
	public static EffectMgr getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("EffectMgr").GetComponent<EffectMgr>();
		}
		return _instance;
	}

	// Use this for initialization
	void Start ()
	{
	
	}

	public void ShowEffect(GameObject obj, int effect){
		switch(effect){
		case 1:
			GameObject eff = Instantiate(m_fireEffect) as GameObject;
			eff.transform.parent = root;
			eff.transform.localScale = Vector3.one;
			eff.transform.rotation = obj.transform.rotation;
			eff.transform.position = obj.transform.position;
			Destroy(eff, 5.0f);
			break;
		case 2:
			GameObject eff2 = Instantiate(m_aoeEffect) as GameObject;
			eff2.transform.parent = root;
			eff2.transform.localScale = Vector3.one;
			eff2.transform.rotation = obj.transform.rotation;
			eff2.transform.position = obj.transform.position;
			Destroy(eff2, 5.0f);
			break;
		case 3:
			GameObject eff3 = Instantiate(m_healEffect) as GameObject;
			eff3.transform.parent = root;
			eff3.transform.localScale = Vector3.one;
			eff3.transform.rotation = obj.transform.rotation;
			eff3.transform.position = obj.transform.position;
			Destroy(eff3, 5.0f);
			break;
			break;
		}
	}

}

