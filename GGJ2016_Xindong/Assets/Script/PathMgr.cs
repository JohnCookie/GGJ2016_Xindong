using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathMgr : MonoBehaviour
{
	public List<Transform> Path1;
	public List<Transform> Path2;
	public List<Transform> Path3;

	public List<Transform> Path4;
	public List<Transform> Path5;

	private static PathMgr _instance;
	private PathMgr(){
	}
	public static PathMgr getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("PathMgr").GetComponent<PathMgr>();
		}
		return _instance;
	}

	// Use this for initialization
	void Start ()
	{
	}

	public Transform getAtkNextPath(int index, int path){
		List<Transform> findPath = Path1;
		switch(path){
		case 1:
			findPath = Path1;
			break;
		case 2:
			findPath = Path2;
			break;
		case 3:
			findPath = Path3;
			break;
		}
		if(index >= findPath.Count){
			return findPath[findPath.Count-1];
		}
		return findPath[index];
	}

	public Transform getDefNextPath(int index, int path){
		List<Transform> findPath = Path1;
		switch(path){
		case 1:
			findPath = Path1;
			break;
		case 2:
			findPath = Path2;
			break;
		case 3:
			findPath = Path3;
			break;
		}
		int realIndex = findPath.Count-1-index;
		if(realIndex<=0){
			return findPath[0];
		}
		return findPath[realIndex];
	}

	public Transform getAtkPrepareNextPath(int index){
		if(index >= Path4.Count){
			return Path4[Path4.Count-1];
		}
		return Path4[index];
	}

	public Transform getDefPrepareNextPath(int index){
		if(index >= Path5.Count){
			return Path5[Path5.Count-1];
		}
		return Path5[index];
	}
}

