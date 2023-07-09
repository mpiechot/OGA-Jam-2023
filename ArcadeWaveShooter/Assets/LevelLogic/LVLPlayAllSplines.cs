using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class LVLPlayAllSplines : MonoBehaviour
{

    private SplineAnimate[] allSplines;
    private int safeSplineIndex { get => splineIndex % allSplines.Length; }
    private int splineIndex = 0;

    [SerializeField] private bool destroyOnEnd;

    // Start is called before the first frame update
    void Start()
    {
        allSplines = GetComponents<SplineAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allSplines[safeSplineIndex].IsPlaying)
        {
            splineIndex++;
            if (destroyOnEnd && allSplines.Length <= splineIndex) { Destroy(gameObject); return; }
            allSplines[safeSplineIndex].Restart(true);
        }
    }
}
