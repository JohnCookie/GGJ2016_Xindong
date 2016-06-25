using UnityEngine;
using System.Collections;

public class SkillObj : MonoBehaviour
{
	public UITexture m_texMccree;
	public UITexture m_texReinhart;
	public UITexture m_texLucio;
	public UITexture m_texSoldier76;

	public TweenPosition mtween;

	float delay = 3.0f;
	float curr = 0.0f;
	bool startDelay = false;

	// Use this for initialization
	public void Init (int type)
	{
		switch (type) {
		case 0:
			m_texMccree.gameObject.SetActive (true);
			m_texReinhart.gameObject.SetActive (false);
			m_texLucio.gameObject.SetActive (false);
			m_texSoldier76.gameObject.SetActive (false);
			break;
		case 1:
			m_texMccree.gameObject.SetActive (false);
			m_texReinhart.gameObject.SetActive (true);
			m_texLucio.gameObject.SetActive (false);
			m_texSoldier76.gameObject.SetActive (false);
			break;
		case 2:
			m_texMccree.gameObject.SetActive (false);
			m_texReinhart.gameObject.SetActive (false);
			m_texLucio.gameObject.SetActive (true);
			m_texSoldier76.gameObject.SetActive (false);
			break;
		case 3:
			m_texMccree.gameObject.SetActive (false);
			m_texReinhart.gameObject.SetActive (false);
			m_texLucio.gameObject.SetActive (false);
			m_texSoldier76.gameObject.SetActive (true);
			break;
		}

		mtween.enabled = true;
		mtween.ResetToBeginning ();
		mtween.PlayForward ();
	}

	public void endCallback(){
		startDelay = true;
	}

	void Update(){
		if (startDelay) {
			curr += Time.deltaTime;
			if (curr >= delay) {
				Destroy (gameObject);
			}
		}
	}
}

