using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(VLRTK.Outline.OutlineObject))]
public class ShutterUse : MonoBehaviour, IUsable
{
    public bool isOn = false;

    [Header("Animation Settings")]
    public float Max;
    public float Min;
    public float Duration;

    public void Use()
    {
        isOn = !isOn;

        if (isOn)
            StartCoroutine(AnimationPlay(Max, Min, Duration)); // Lock
        else
            StartCoroutine(AnimationPlay(Min, Max, Duration)); // Unlock
    }

    IEnumerator AnimationPlay(float min, float max, float duration)
    {
        Vector3 rot = this.transform.localEulerAngles;

        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            rot.x = Mathf.Lerp(min, max, percent);
            transform.localEulerAngles = rot;

            yield return null;
        }
    }
}
