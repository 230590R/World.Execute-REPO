using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Demo : MonoBehaviour
{
    List<DialougeSO> dialougeSO2 = new List<DialougeSO>();
    static bool reset = true;

    private void Start()
    {
        if (!reset) return;

        NPCDialougeTrigger[] allDialougeGO = GameObject.FindObjectsOfType<NPCDialougeTrigger>();

        for (int i = 0; i < allDialougeGO.Length; i++)
        {
            for (int j = 0; j < allDialougeGO[i].dialougeSOs.Length; j++)
            {
                dialougeSO2.Add(allDialougeGO[i].dialougeSOs[j]);
            }
        }

        for (int i = 0; i < dialougeSO2.Count; i++)
        {
            if (dialougeSO2[i].dialougeDone)
            {
                dialougeSO2[i].dialougeDone = false;
            }
        }
        reset = false;
    }
}
