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
        public float fadeDuration = 1f;
        public bool isDeactivated = false;
    }

    public List<Gate> Gates;

    void Update()
    {
        foreach (Gate group in Gates)
        {
            if (group.isDeactivated)
            {
                continue;
            }

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
                StartCoroutine(FadeOutAndDeactivate(group.objectToDeactivate, group.fadeDuration));
                group.isDeactivated = true; 
            }
        }
    }

    IEnumerator FadeOutAndDeactivate(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            obj.SetActive(false);
            yield break;
        }

        Material material = renderer.material;
        Color originalColor = material.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        obj.SetActive(false);

        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);
    }
}
