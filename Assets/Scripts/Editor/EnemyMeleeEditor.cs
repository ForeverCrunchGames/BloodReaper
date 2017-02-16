using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (EnemyMeleeLogic))]
public class EnemyMeleeEditor : Editor 
{
    void OnSceneGUI() 
    {
        EnemyMeleeLogic enemy = (EnemyMeleeLogic)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc (fow.transform.position, new Vector3(0,0,1), new Vector3(0,1,0), 360, fow.viewRadius);
//        Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
//        Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);
//
//        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
//        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
//
//        Handles.color = Color.red;
//        foreach (Transform visibleTarget in fow.visibleTargets) {
//            Handles.DrawLine (fow.transform.position, visibleTarget.position);
//        }
    }
}
