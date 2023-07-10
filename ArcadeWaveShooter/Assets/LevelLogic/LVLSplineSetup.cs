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
            sA.MaxSpeed = 5;
            sA.Alignment = SplineAnimate.AlignmentMode.None;
            isFirstSpline = false;
            if (sA.Container.Spline.TryGetIntData("EasingType", out SplineData<int> data))
                sA.Easing = data.DefaultValue == 0 ? SplineAnimate.EasingMode.EaseIn : data.DefaultValue == 1 ? SplineAnimate.EasingMode.EaseOut : data.DefaultValue == 2 ? SplineAnimate.EasingMode.EaseInOut : SplineAnimate.EasingMode.None;
        }
    }
}
