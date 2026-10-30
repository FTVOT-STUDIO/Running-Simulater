using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Reposition : MonoBehaviour
{
    public UnityEvent onChange;
    public UnityEvent[] onCoinState;

    public float rePosCoordinate;
    public float limitsPos;
    private void LateUpdate()
    {
        if (!GameManager.isLive)
            return;
        if (transform.position.x > limitsPos)
            return;
        transform.Translate(rePosCoordinate, 0, 0, Space.Self);
        onChange.Invoke();
        for (int i = 0; i < onCoinState.Length; i++)
        {
            onCoinState[i].Invoke();
        }

    }
}
