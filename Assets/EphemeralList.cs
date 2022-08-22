using UnityEngine;

public class EphemeralList<T>
{
    private readonly float defaultExpiryTime;
    public List<(T reference, float expiryTime)> References { get; } = new();

    public EphemeralList(float defaultExpiryTime = 0)
    {
        this.defaultExpiryTime = defaultExpiryTime;
    }

    public Add(T reference, float expiryTime = 0)
    {
        if (expiryTime == 0)
        {
            expiryTime = defaultExpiryTime;
        }

        references.Add((reference, expiryTime));
    }

    public void Poll(Action<T> expiryCallback)
    {
        float time = Time.time;
        references.RemoveAll((var reference, var expiryTime) =>
        {
            if (time >= expiryTime)
            {
                expiryCallback?.Invoke(reference);
                return true;
            }
            return false;
        });
    }
}