using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This could of been using System.Timers but we have to use Coroutines becuase WebGL doesn't support threads :/
/// </summary> <summary>
///
/// </summary>
public class CoroutineTimerHandler : MonoBehaviour
{
    private static CoroutineTimerHandler _instance;
    public static CoroutineTimerHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CoroutineTimerHandler");
                _instance = go.AddComponent<CoroutineTimerHandler>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private Dictionary<string, Coroutine> activeTimers = new Dictionary<string, Coroutine>();

    public static void StartTimer(string timerId, float delay, System.Action callback)
    {
        Coroutine coroutine = Instance.StartCoroutine(Instance.RunTimer(delay, callback));
        Instance.activeTimers[timerId] = coroutine;
    }

    public static void StopTimer(string timerId)
    {
        if (Instance.activeTimers.TryGetValue(timerId, out Coroutine coroutine))
        {
            Instance.StopCoroutine(coroutine);
            Instance.activeTimers.Remove(timerId);
        }
    }

    private IEnumerator RunTimer(float delay, System.Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
