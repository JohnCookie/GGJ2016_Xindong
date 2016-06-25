using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FlowController))]

public class FlowController_Editor : Editor
{
    FlowController _target;

    static bool bAbout;
    Texture2D icon;

    int[] materialArray;
    string[] str;

    void OnEnable()
    {
        _target = (FlowController)target;

        RebuildMaterialIndexArray();

        if (EditorGUIUtility.isProSkin)
            icon = Resources.Load("Flow_Icon_Editor_Pro") as Texture2D;
        else
            icon = Resources.Load("Flow_Icon_Editor_Free") as Texture2D;

    }

    public override void OnInspectorGUI()
    {
        RebuildMaterialIndexArray();        

        _target.materialIndex = EditorGUILayout.IntPopup("Material index:", _target.materialIndex, str, materialArray);
        _target.speed = EditorGUILayout.Slider("Speed: ", _target.speed, -1.0f, 1.0f);
        _target.phaseLength = EditorGUILayout.Slider("Stretching: ", _target.phaseLength, 0.3f, 5.0f);
        _target.flowType = (FlowController.FLOW_TYPE)EditorGUILayout.EnumPopup("Flow type: ", _target.flowType);

        if (_target.flowType == FlowController.FLOW_TYPE.Special)
        {
            _target.alphaCurve = EditorGUILayout.CurveField("Alpha Curve: ", _target.alphaCurve);
            _target.destroyOnEnd = EditorGUILayout.Toggle("Destroy when finished: ", _target.destroyOnEnd);
        }
        else
        {
            _target.bAnimateFlowRevealing = EditorGUILayout.Toggle("Animate flow revealing", _target.bAnimateFlowRevealing);

            if (_target.bAnimateFlowRevealing)
            {
                _target.revealCurve = EditorGUILayout.CurveField("Reveal Curve: ", _target.revealCurve);
                Repaint();
            }
        }

        About();
                
    }

    void OnDisable()
    {
        _target = null;
    }

    void RebuildMaterialIndexArray()
    {
        materialArray = new int[_target.GetComponent<Renderer>().sharedMaterials.Length];
        str = new string[materialArray.Length];

        for (int i = 0; i < materialArray.Length; i++)
        {
            materialArray[i] = i;
            str[i] = i.ToString() + ". " + _target.GetComponent<Renderer>().sharedMaterials[i].ToString();

            //
            if (str[i].IndexOf("null") == -1)
                str[i] = str[i].Remove(str[i].IndexOf("("));
        }

        if (_target.materialIndex > materialArray.Length - 1)
            _target.materialIndex = materialArray.Length - 1;
    }

    void About()
    {
        GUILayout.Space(5);

        bAbout = EditorGUILayout.Foldout(bAbout, "About");
        if (bAbout)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(10);
            GUILayout.Box(icon, new GUIStyle());

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("version 1.6");
            EditorGUILayout.LabelField("by Davit Naskidashvili");
            EditorGUILayout.LabelField("2013");
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();


            GUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("My assets", GUILayout.Width(100)))
            {
                Application.OpenURL("http://u3d.as/publisher/davit-naskidashvili/2RW");
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Forum", GUILayout.Width(100)))
            {
                Application.OpenURL("http://forum.unity3d.com/threads/156119-Flow");
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
