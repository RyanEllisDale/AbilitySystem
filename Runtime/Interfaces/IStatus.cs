namespace AbilitySystem
{
    public interface IStatus
    {
        public StatusEffectManager StatusManager { get; }

        public void ApplyStatusDamage(int damage);
    }
}
