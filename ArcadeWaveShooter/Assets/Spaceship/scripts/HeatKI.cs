using UnityEngine;

public class HeatKI : MonoBehaviour
{
    public float heat = 0;
    public float heatIncreaseRate = 1;
    public float heatThreshold = 10;
    private bool limitReached = false;

    public bool IsOverHeated { get => heat >= heatThreshold; }

    private void Update()
    {
        heat += heatIncreaseRate * Time.deltaTime;
        if (heat > heatThreshold) heat = heatThreshold;

        if (heat >= heatThreshold && !limitReached)
        {
            limitReached = true;
            Mailbox.InvokeSubscribers(new HeatLimitReachedMail());
        }
        else if (heat < heatThreshold && limitReached)
        {
            limitReached = false;
        }
    }

    public void DecreaseHeat(float heatDecreaseAmount)
    {
        heat -= heatDecreaseAmount;
        if (heat < 0) heat = 0;

        if (heat < heatThreshold && limitReached)
        {
            limitReached = false;
        }
    }

    public void DecreaseHeat(HeatDecreaseMail heatDecreaseMail)
    {
        heat -= heatDecreaseMail.decreaseAmount;
        if (heat < 0) heat = 0;

        if (heat < heatThreshold && limitReached)
        {
            limitReached = false;
        }

    }

    private void OnEnable()
    {
        Mailbox.AddSubscriber<HeatDecreaseMail>(DecreaseHeat);
    }

    private void OnDisable()
    {
        Mailbox.RemoveSubscriber<HeatDecreaseMail>(DecreaseHeat);
    }
}