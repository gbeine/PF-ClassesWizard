using System;

namespace PF_Classes.Identifier
{
    public class Progressions : Identifier
    {
        public static readonly Progressions INSTANCE = new Progressions();

        private Progressions() { }

        public const String BLOODLINE_UNDEAD_PROGRESSION = "a1a8bf61cadaa4143b2d4966f2d1142e"; // Blutlinie des Grabes
        public const String FIRE_DOMAIN_PROGRESSION = "881b2137a1779294c8956fe5b497cc35"; // Domäne des Feuers
    }
}
