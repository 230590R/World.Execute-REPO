using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestManager : MonoBehaviour
{
    protected QuestChannel _questsChannel;

    public string UniqueId;
    public string Name;
    public QuestState State;

    public enum QuestState
    {
        Pending,
        Active,
        Completed
    }

    protected virtual void Enable()
    {
        this._questsChannel.QuestActivatedEvent += this.QuestActiveEvent;
        this._questsChannel.QuestCompleteEvent += this.QuestCompletedEvent;

        if (State == QuestState.Active)
        {
            this.QuestActive();
        }
    }

    protected virtual void Disable()
    {
        this._questsChannel.QuestActivatedEvent -= this.QuestActiveEvent;
        this._questsChannel.QuestCompleteEvent -= this.QuestCompletedEvent;
    }

    private void QuestActiveEvent(QuestManager activeQuest)
    {
        if (activeQuest.UniqueId != this.UniqueId) return;

        this.State = QuestState.Active;
        this.QuestActive();
    }

    protected abstract void QuestActive();

    private void QuestCompletedEvent(QuestManager completedQuest)
    {
        if (completedQuest.UniqueId != this.UniqueId) return;

        State = QuestState.Completed;
        this.QuestCompleted();
    }

    protected abstract void QuestCompleted();

    protected void Complete()
    {
        this._questsChannel.CompleteQuest(this);
    }
}
