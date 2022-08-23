public class BalboaRock : Rock
{
    [SerializeField] private float damagePerSecond;

    protected override void OnEnemyStay(Enemy enemy)
    {
        enemy.Damage(Time.deltaTime * damagePerSecond);
    }
}