namespace Data
{
    internal class CardData
    {
        public string Name { get; }
        public string Description { get; }
        public int Health { get; private set; }
        public int ManaCost { get; private set; }
        public int AttackDamage { get; private set; }

        public void SetHealth(int value) => Health = value;
        public void SetManaCosth(int value) => ManaCost = value;
        public void SetAttackDamage(int value) => AttackDamage = value;

        public CardData(string name, string description, int healths, int mana, int attack)
        {
            Name = name;
            Description = description;
            Health = healths;
            ManaCost = mana;
            AttackDamage = attack;
        }
    }
}