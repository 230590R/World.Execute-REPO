using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebuffEnergyManager : MonoBehaviour
{
    public IDebuff debuff;

    public string Scene1;
    public string Scene2;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == Scene1)
        {
            RemoveDebuff(debuff);
        }
        else if (currentScene == Scene2)
        {
            ApplyDebuff(debuff);
        }
    }

    private void ApplyDebuff(IDebuff debuffToApply)
    {
        if (playerStats != null)
        {
            if (!playerStats.debuffs.Contains(debuffToApply))
            {
                playerStats.debuffs.Add(debuffToApply);
            }
        }
    }

    private void RemoveDebuff(IDebuff debuffToRemove)
    {
        if (playerStats != null)
        {
            if (playerStats.debuffs.Contains(debuffToRemove))
            {
                playerStats.debuffs.Remove(debuffToRemove);
            }
        }
    }
}
