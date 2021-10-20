using System;

namespace PF_Classes.Identifier
{
    public class AbilityAreaEffects : Identifier
    {
        public static readonly AbilityAreaEffects INSTANCE = new AbilityAreaEffects();

        private AbilityAreaEffects() { }

        public const String ACID        = "2a9cebe780b6130428f3bf4b18270021"; // Acid
        public const String COLD        = "608d84e25f42d6044ba9b96d9f60722a"; // Cold
        public const String ELECTRICITY = "2175d68215aa61644ad1d877d4915ece"; // Electricity
        public const String FIRE        = "ac8737ccddaf2f948adf796b5e74eee7"; // Fire
    }
}
