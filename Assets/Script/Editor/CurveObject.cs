using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurveObject : EditorWindow
{
    //public AnimationCurve animationCurve;

    AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    [MenuItem("Custom/Create Curve For Object")]
    static void Init()
    {
        CurveObject window = (CurveObject)EditorWindow.GetWindow(typeof(CurveObject));
        window.Show();
    }

    void OnGUI()
    {
        curve = EditorGUILayout.CurveField("", curve);

        if (GUILayout.Button("Generate Curve"))
            AddCurveToSelectedGameObject();
    }

    void  AddCurveToSelectedGameObject()
    {
        if (Selection.activeGameObject)
        {
            var curveEntity = Selection.activeGameObject.GetComponent<CurveEntity>();
            if(!curveEntity)
            {
                curveEntity = Selection.activeGameObject.AddComponent<CurveEntity>();
            }
            curveEntity.curve = new AnimationCurve(curve.keys);
        }
        else
        {
            Debug.LogError("No Game Object selected for adding an animation curve");
        }
    }

}
