namespace PF_Classes.Identifier
{
    public class StatProgession: Identifier
    {
        public static readonly StatProgession INSTANCE = new StatProgession();

        private StatProgession() { }

        public const string BAB_FULL            = "b3057560ffff3514299e8b93e7648a9d"; // Base Attack Bonus
        public const string BAB_LOW             = "0538081888b2d8c41893d25d098dee99"; // Base Attack Bonus
        public const string BAB_MEDIUM          = "4c936de4249b61e419a3fb775b9f2581"; // Base Attack Bonus
        public const string BASE_CR_TABLE       = "c0bed557c59c0394498253d90489bc27"; //
        public const string CR_TABLE            = "19b09eaa18b203645b6f1d5f2edcb1e4"; //
        public const string DC_TO_CR_TABLE      = "674a4e0eb86cc89478119de258f602a5"; //
        public const string SAVES_HIGH          = "ff4662bde9e75f145853417313842751"; // Fortitude, Reflex, Will
        public const string SAVES_LOW           = "dc0c7c1aba755c54f96c089cdf7d14a3"; // Fortitude, Reflex, Will
        public const string SAVES_PRESTIGE_HIGH = "1f309006cd2855e4e91a6c3707f3f700"; // Fortitude, Reflex, Will
        public const string SAVES_PRESTIGE_LOW  = "dc5257e1100ad0d48b8f3b9798421c72"; // Fortitude, Reflex, Will
        public const string XP_TABLE            = "87c24ce6bcf1a994296f3c582c1a632b"; //
    }
}
