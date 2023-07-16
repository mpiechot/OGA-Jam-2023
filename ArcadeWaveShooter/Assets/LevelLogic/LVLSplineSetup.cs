using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class LVLSplineSetup : MonoBehaviour
{

    [SerializeField] private GameObject splineContainer;

    private bool isFirstSpline;

    private void Awake()
    {
        SplineContainer[] allSplines = splineContainer.GetComponentsInChildren<SplineContainer>();
        isFirstSpline = true;
        foreach (SplineContainer spline in allSplines)
        {
            SplineAnimate sA = gameObject.AddComponent<SplineAnimate>();
            sA.Container = spline;
            sA.PlayOnAwake = isFirstSpline;
            sA.Loop = SplineAnimate.LoopMode.Once;
            sA.AnimationMethod = SplineAnimate.Method.Speed;
            if (sA.Container.Spline.TryGetFloatData("Speed", out SplineData<float> speedData))
                sA.MaxSpeed = speedData.DefaultValue;
            else
                sA.MaxSpeed = 2;
            sA.Alignment = SplineAnimate.AlignmentMode.None;
            isFirstSpline = false;
            if (sA.Container.Spline.TryGetIntData("EasingType", out SplineData<int> easingData))
                sA.Easing = easingData.DefaultValue == 0 ? SplineAnimate.EasingMode.EaseIn : easingData.DefaultValue == 1 ? SplineAnimate.EasingMode.EaseOut : easingData.DefaultValue == 2 ? SplineAnimate.EasingMode.EaseInOut : SplineAnimate.EasingMode.None;
        }
    }
}
