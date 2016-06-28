using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenScene : MonoBehaviour
{
	int index = 0;
	public List<Rigidbody> mList = new List<Rigidbody>();
	public GameObject fakePart;

	bool fake = true;

	// Use this for initialization
	void Start ()
	{
		index=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space)){
			if (!fake) {
				FallOneHero ();
			} else {
				fakePart.SetActive (false);
				AudioControler.getInstance().playBGM();
				fake = false;
			}
		}
	}

	void FallOneHero(){
		if(index<mList.Count){
			mList[index].useGravity=true;
			index++;
		}else{
			if (index == 7) {
				AudioControler.getInstance ().playMrrcee ();
				index++;
			} else {
				Application.LoadLevel(1);
			}
		}
	}
}

