using UnityEngine;
using System.Collections;

public class AudioControler : MonoBehaviour
{
	public AudioSource m_AudioMgr;
	public AudioSource m_effectMgr;
	public AudioClip m_clipBgm;
	public AudioClip m_clipSoldier;
	public AudioClip m_clipLucio;
	public AudioClip m_clipMccree;
	public AudioClip m_clipReinhart;

	private static AudioControler _instance;
	private AudioControler(){
	}
	public static AudioControler getInstance(){
		if(_instance==null){
			_instance=GameObject.Find("AudioMgr").GetComponent<AudioControler>();
		}
		return _instance;
	}

	public void playBGM(){
		m_AudioMgr.loop=true;
		m_AudioMgr.clip=m_clipBgm;
		m_AudioMgr.volume=0.2f;
		m_AudioMgr.Play();
	}

	public void playMrrcee(){
		m_effectMgr.clip=m_clipMccree;
		m_effectMgr.volume=1.0f;
		m_effectMgr.loop=false;
		m_effectMgr.PlayOneShot(m_clipMccree);
	}

	public void playReinhart(){
		m_effectMgr.clip=m_clipReinhart;
		m_effectMgr.volume=1.0f;
		m_effectMgr.loop=false;
		m_effectMgr.PlayOneShot(m_clipReinhart);
	}

	public void playLucio(){
		m_effectMgr.clip=m_clipLucio;
		m_effectMgr.volume=1.0f;
		m_effectMgr.loop=false;
		m_effectMgr.PlayOneShot(m_clipLucio);
	}

	public void playSoldier(){
		m_effectMgr.clip=m_clipSoldier;
		m_effectMgr.volume=1.0f;
		m_effectMgr.loop=false;
		m_effectMgr.PlayOneShot(m_clipSoldier);
	}
}

