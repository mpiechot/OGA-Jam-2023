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
        SplineAnimate lastSpline = null;
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
            sA.Easing = isFirstSpline? SplineAnimate.EasingMode.EaseIn : SplineAnimate.EasingMode.None;
            lastSpline = sA;
        }
        lastSpline.Easing = SplineAnimate.EasingMode.EaseOut;
    }
}
