using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpControl : MonoBehaviour, IUsable
{
    public bool isOn = false;

    [Header("Animation Settings")]
    public float Max;
    public float Min;
    public float Duration;

    public void Use()
    {
        if (!Stend.Instance.screen.IsActiveScreen())
            return;

        isOn = !isOn;

        if (isOn)
        {
            this.GetComponent<AudioSource>().Play();
            StartCoroutine(AnimationPlay(Max, Min, Duration)); // Lock
        }
        else
        {
            this.GetComponent<AudioSource>().Stop();
            StartCoroutine(AnimationPlay(Min, Max, Duration)); // Unlock
        }
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
