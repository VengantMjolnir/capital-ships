using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hardpoint))]
public class HardpointEditor : Editor
{
    public float arrowSize = 1;

    void OnSceneGUI()
    {
        Hardpoint t = target as Hardpoint;

        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(Screen.width - 100, Screen.height - 80, 90, 50));

        GUILayout.EndArea();
        Handles.EndGUI();

        Vector3 start = Quaternion.AngleAxis(-0.5f * t.limitAngle, Vector3.forward) * t.transform.up;

        Handles.color = new Color(1, 1, 1, 0.1f);
        Handles.DrawSolidArc(t.transform.position, t.transform.forward, start,
                                t.limitAngle, t.range);

        Handles.color = Color.blue;
        Handles.Label(t.transform.position + Vector3.up,
                             t.transform.position.ToString() + "\nRange: " +
                             t.range.ToString());
    }
}
