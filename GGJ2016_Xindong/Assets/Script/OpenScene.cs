using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenScene : MonoBehaviour
{
	int index = 0;
	public List<Rigidbody> mList = new List<Rigidbody>();

	// Use this for initialization
	void Start ()
	{
		index=0;
		AudioControler.getInstance().playBGM();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space)){
			FallOneHero();
		}
	}

	void FallOneHero(){
		if(index<mList.Count){
			mList[index].useGravity=true;
			index++;
		}else{
			Application.LoadLevel(1);
		}
	}
}

