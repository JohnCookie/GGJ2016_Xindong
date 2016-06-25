using UnityEngine;
using System.Collections;

public class TrailCtrl : MonoBehaviour
{
	private TrailRenderer m_Trail = null;
	public float m_StartTime = -1;
	public float m_EndTime = -1;
	public Color m_Color;
	// Use this for initialization
	void Awake () 
	{
		m_Trail = this.GetComponent<TrailRenderer>();
		
		if(null != m_Trail && null != m_Trail.material)
		{
			m_Trail.material.SetColor("_TintColor", m_Color);
		}
		
		if(m_StartTime > 0)
		{
			if(null != m_Trail)
			{
				m_Trail.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_StartTime > 0)
		{
			m_StartTime -= Time.deltaTime;
			if(m_StartTime <= 0)
			{
				if(null != m_Trail)
					m_Trail.enabled = true;
			}
		}

		
		if(m_EndTime > 0)
		{
			m_EndTime -= Time.deltaTime;
			if(m_EndTime <= 0)
			{
				if(null != m_Trail)
					m_Trail.enabled = false;
			}
		}
		
		if(null != m_Trail && null != m_Trail.material)
		{
			m_Trail.material.SetColor("_TintColor", m_Color);
		}
		
	}
}
