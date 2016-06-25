using UnityEngine;
using System.Collections;

[AddComponentMenu("Animation/UVAnimation")]
public class Effect_UVAnimation : MonoBehaviour 
{
	public bool pauseTimer = false;	
	public Vector2 uvOffsetSpeed = Vector2.zero;
	
	private Renderer _renderer;
	private Material _mat;
	Vector2 m_offset = new Vector2();
	
	// Use this for initialization
	void Awake () {
		if (GetComponent<Renderer>())
		{
			_renderer = GetComponent<Renderer>();
		}
		else
		{
			enabled = false;
		}
	}

	private int _LastMaterialIndex;
	public int _MaterialIndex;
	
//	void Start()
//	{
//		if (_renderer)
//		{
//			_mat = _renderer.material;
//		}
//	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_LastMaterialIndex != _MaterialIndex) {
			if(_MaterialIndex >= 0 && _MaterialIndex < _renderer.materials.Length) {
				_mat = _renderer.materials[_MaterialIndex];
				_LastMaterialIndex = _MaterialIndex;
			}
		}

		if(_mat == null) return;

		if (uvOffsetSpeed.x != 0 || uvOffsetSpeed.y != 0)
        {
			m_offset.x += uvOffsetSpeed.x * (pauseTimer ? Time.deltaTime : Time.realtimeSinceStartup);
            if (m_offset.x > 1)
            {
                m_offset.x -= 1;
            }
            else if (m_offset.x < -1)
            {
                m_offset.x += 1;
            }
			m_offset.y += uvOffsetSpeed.y * (pauseTimer ? Time.deltaTime : Time.realtimeSinceStartup);
            if (m_offset.y > 1)
            {
                m_offset.y -= 1;
            }
            else if (m_offset.y < -1)
            {
                m_offset.y += 1;
            }
            _mat.mainTextureOffset = m_offset;
        }
	}
	
	void OnDestroy()
	{
		if (_mat != null)
		{
			GameObject.DestroyImmediate(_mat);
		}
	}
	
	
}
