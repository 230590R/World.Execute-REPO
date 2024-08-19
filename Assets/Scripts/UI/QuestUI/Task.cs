using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : QuestManager
{
    [TextArea(2, 10)]
    public string[] sentences;
    public Animation animator;

    public Color _isActive;
    public Color _isComplete;

    protected override void QuestActive()
    {
        throw new System.NotImplementedException();
    }

    protected override void QuestCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void taskComplete()
    {
        QuestCompleted();

    }

    public void taskActive()
    {
        QuestActive();
    }
}
