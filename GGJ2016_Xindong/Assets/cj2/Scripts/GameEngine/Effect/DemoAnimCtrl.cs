using UnityEngine;
using System.Collections;

public class DemoAnimCtrl : MonoBehaviour 
{
	public string[] m_AnimNames;
	public float[] m_OffsetTime;
	public string m_IdleAnimName = "";
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < m_AnimNames.Length && i < m_OffsetTime.Length; ++i)
		{
			if(m_OffsetTime[i] >= 0)
			{
				m_OffsetTime[i] -= Time.deltaTime;
				if(m_OffsetTime[i] < 0)
				{
					if(!string.IsNullOrEmpty(m_AnimNames[i]))
					{
						this.GetComponent<Animation>().Play(m_AnimNames[i]);
					}
				}
			}
		}
		if(!string.IsNullOrEmpty(m_IdleAnimName) && !this.GetComponent<Animation>().isPlaying)
		{
			this.GetComponent<Animation>().Play(m_IdleAnimName);
		}
	}
}
