public class EphemeralMultipliers
{
    private readonly EphemeralList<float> multipliers = new();

    public float Add(float multiplier, float expiryTime)
    {
        multipliers.Add(multiplier, expiryTime);
    }

    public float GetProduct(float operand = 1f)
    {
        float product = operand;
        multipliers.Poll(multiplier =>
        {
            product *= multiplier;
        });
        return product;
    }
}