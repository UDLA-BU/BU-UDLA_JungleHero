using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventName
{
    updateColliders,
    allMusclesInvisible,
    allMusclesVisible,
    disabletools,
    enablePointer,
    disablePointer,
    enableAnnotations,
    disableAnnotations,
    enableMaterials,
    disableMaterials,
    enableMovement,
    disableMovement,
    annotationColor,
    selectMaterial,
    enableCamera,
    disableCamera,
    destroyAllAnnotations,
    destroyAllSnapShoots,
    resetAvatarPosition

}

//Re-usable structure/ Can be a class to. Add all parameters you need inside it
public enum HandType
{
    Left,
    Right
}
public struct EventParam
{
    public HandType handType;
    public int colorIndex;
    public int materialID;
}

public class EventManager : MonoBehaviour
{

    private Dictionary<EventName, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    //Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventName, Action<EventParam>>();
        }
    }

    public static void StartListening(EventName eventName, Action<EventParam> listener)
    {
        if (instance == null) { return; }
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] += listener;
        }
        else
        {
            instance.eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(EventName eventName, Action<EventParam> listener)
    {
        if (instance == null) { return; }

        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(EventName eventName, EventParam eventParam)
    {
        if (instance == null) { return; }
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}

