using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class LVLPlayAllSplines : MonoBehaviour
{

    private SplineAnimate[] allSplines;
    private int safeSplineIndex { get => splineIndex % allSplines.Length; }
    private int splineIndex = 0;

    [SerializeField] private bool destroyOnEnd;

    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        allSplines = GetComponents<SplineAnimate>();
        if (allSplines[safeSplineIndex].Container.Spline.TryGetFloatData("WaitTime", out SplineData<float> data))
        {
            waitTime = data.DefaultValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!allSplines[safeSplineIndex].IsPlaying)
        {
            if (waitTime <= 0)
            {
                splineIndex++;
                if (destroyOnEnd && allSplines.Length <= splineIndex) { Destroy(gameObject); return; }
                allSplines[safeSplineIndex].Restart(true);
                if(allSplines[safeSplineIndex].Container.Spline.TryGetFloatData("WaitTime", out SplineData<float> data))
                {
                    waitTime = data.DefaultValue;
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
