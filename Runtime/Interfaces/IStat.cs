namespace AbilitySystem
{
    public interface IStats
    {   
        public BuffManager buffManager { get; }

        float GetStat(string statId);
        void ModifyStat(string statId, float amount, BuffModifierType type);
    }
}
