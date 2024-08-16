using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialougeController : MonoBehaviour
{
    public List<GameObject> dialougeTriggers = new List<GameObject>();
    int closestNPCIndex = 0;
    float closestDistance = -1;
    [SerializeField] GameObject player;

    [SerializeField] Vector2 offset = Vector2.zero;

    private void Update()
    {
        if (dialougeTriggers.Count > 0)
        {
            Transform child = transform.GetChild(0);
            child.gameObject.SetActive(true);

            for (int i = 0; i < dialougeTriggers.Count; ++i)
            {
                float distance = Vector2.Distance(player.transform.position, dialougeTriggers[i].transform.position);

                if (closestDistance >= 0 && closestDistance > distance)
                {
                    closestDistance = distance;
                    closestNPCIndex = i;
                }
            }

            transform.SetParent(dialougeTriggers[closestNPCIndex].transform);

            transform.localPosition = new Vector3(offset.x, offset.y + dialougeTriggers[closestNPCIndex].transform.localScale.y * 0.5f, 0.0f);
        }
        else
        {
            transform.SetParent(null);
            closestDistance = -1;

            Transform child = transform.GetChild(0);
            child.gameObject.SetActive(false);
        }
    }
}
