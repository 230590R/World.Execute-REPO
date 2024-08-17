using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesManager : MonoBehaviour
{
    [System.Serializable]
    public class Gate
    {
        public List<GameObject> objectsToCheck; 
        public GameObject objectToDeactivate;   
    }

    public List<Gate> Gates; 

    void Update()
    {
        foreach (Gate group in Gates)
        {
            bool allDestroyed = true;

            foreach (GameObject obj in group.objectsToCheck)
            {
                if (obj != null)
                {
                    allDestroyed = false;
                    break;
                }
            }

            if (allDestroyed)
            {
                group.objectToDeactivate.SetActive(false);
            }
        }
    }
}
