public interface IDamage
{
    int Health { get; set; }

    void ApplyDamage(int amount);
}
