using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DoorUI : MonoBehaviour
{
    private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private Sprite activeSprite; 
    private Sprite inactiveSprite;

    private SceneSwitcherV2 sceneSwitcher; 

    private void Awake()
    {

        doorSpriteRenderer = GetComponent<SpriteRenderer>();

        sceneSwitcher = GetComponent<SceneSwitcherV2>();

        inactiveSprite = doorSpriteRenderer.sprite;
    }

    private void Update()
    {
        if (sceneSwitcher != null && sceneSwitcher.enabled)
        {

            doorSpriteRenderer.sprite = activeSprite;
        }
        else
        {

            doorSpriteRenderer.sprite = inactiveSprite;
        }
    }
}
