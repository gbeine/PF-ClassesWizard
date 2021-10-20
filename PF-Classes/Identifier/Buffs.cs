using System;

namespace PF_Classes.Identifier
{
    public class Buffs : Identifier
    {
        public static readonly Buffs INSTANCE = new Buffs();

        private Buffs() { }

        public const String ECHOLOCATION = "cbfd2f5279f5946439fe82570fd61df2"; //
        public const String SHAKEN       = "25ec6cb6ab1845c48a95f9c20b034220"; // Erschüttert
        public const String SICKENED     = "4e42460798665fd4cb9173ffa7ada323"; // Kränkelnd
        public const String SLEEPING     = "5e0cd801bac0e95429bb7e4d1bc61a23"; // Schlaf
        public const String SLOWED       = "488e53ede2802ff4da9372c6a494fb66"; // Verlangsamt
        public const String STAGGERED    = "df3950af5a783bd4d91ab73eb8fa0fd3"; // Wankend
    }
}
