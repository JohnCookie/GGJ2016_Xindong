using UnityEngine;
using System.Collections;

public class ChaAnimCtrl : MonoBehaviour {
	
	public float m_DelayTime = 3;
	
	
	// Update is called once per frame
	void Update () 
	{
		if(m_DelayTime > 0)
		{
			m_DelayTime -= Time.deltaTime;
			if(m_DelayTime <= 0)
			{
				if(null != this.GetComponent<Animation>())
				{
					this.GetComponent<Animation>().Play();
				}
			}
		}
	}
}
