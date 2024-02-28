using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// This could of been using System.Timers but we have to use Coroutines because WebGL doesn't support threads :/
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

    private List<Coroutine> activeTimers = new List<Coroutine>();

    public static void StartTimer(float delay, System.Action callback)
    {
        Instance.StartCoroutine(Instance.RunTimer(delay, callback));
    }


    private IEnumerator RunTimer(float delay, System.Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
