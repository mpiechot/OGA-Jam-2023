using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class LVLPlayAllSplines : MonoBehaviour
{
    [SerializeField] private bool destroyOnEnd;
    [SerializeField] private float initialWaitTime = 0.0f;
    [SerializeField] private List<GameObject> deleteAfterFirstPlayed = new();

    private List<SplineAnimate> allSplines;
    private int splineIndex = 0;
    private float waitTime = 0;
    private bool initialWait = true;
    private bool initialPlaythrough = true;

    private int SafeSplineIndex => splineIndex % allSplines.Count;

    private SplineAnimate CurrentSpline => allSplines[SafeSplineIndex];

    // Start is called before the first frame update
    void Start()
    {
        allSplines = GetComponents<SplineAnimate>().ToList();
        if (CurrentSpline.Container.Spline.TryGetFloatData("WaitTime", out SplineData<float> data))
        {
            waitTime = data.DefaultValue;
        }
        StartCoroutine(StartPlaying());
    }

    // Update is called once per frame
    void Update()
    {
        if (initialWait || CurrentSpline.IsPlaying)
        {
            return;
        }

        if (initialPlaythrough && deleteAfterFirstPlayed.Any(go => CurrentSpline.Container.name.Contains(go.name)))
        {
            initialPlaythrough = false;
            Destroy(CurrentSpline);
            allSplines.RemoveAt(SafeSplineIndex);

            // We need to go back one spline, because we don't want to skip the next one
            splineIndex--;
        }

        if (waitTime <= 0)
        {
            splineIndex++;
            if (destroyOnEnd && allSplines.Count <= splineIndex)
            {
                Destroy(gameObject);
                return;
            }
            CurrentSpline.Restart(true);
            if (CurrentSpline.Container.Spline.TryGetFloatData("WaitTime", out SplineData<float> data))
            {
                waitTime = data.DefaultValue;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    IEnumerator StartPlaying()
    {
        yield return new WaitForSeconds(initialWaitTime);
        CurrentSpline.Play();
        initialWait = false;
    }
}
