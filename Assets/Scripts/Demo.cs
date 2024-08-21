using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] List<DialougeSO> dialougeSO2 = new List<DialougeSO>();
    static bool reset = true;

    private void Start()
    {
        if (!reset) return;

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
