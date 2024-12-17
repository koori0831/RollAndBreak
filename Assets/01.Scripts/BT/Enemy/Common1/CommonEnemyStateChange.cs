using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/CommonEnemyStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "CommonEnemyStateChange", message: "Enemy Invoked State", category: "Events", id: "6c148439e7eac7d3ae951e71530460c3")]
public partial class CommonEnemyStateChange : EventChannelBase
{
    public delegate void CommonEnemyStateChangeEventHandler();
    public event CommonEnemyStateChangeEventHandler Event; 

    public void SendEventMessage()
    {
        Event?.Invoke();
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        Event?.Invoke();
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        CommonEnemyStateChangeEventHandler del = () =>
        {
            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as CommonEnemyStateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as CommonEnemyStateChangeEventHandler;
    }
}

