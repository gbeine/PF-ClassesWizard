namespace PF_Classes.Identifier
{
    public class Items : Identifier
    {
        public static readonly Items INSTANCE = new Items();

        private Items() { }

        public const string WEAPON_BITE_D4 = "35dfad6517f401145af54111be04d6cf";
        public const string WEAPON_BITE_D6 = "a000716f88c969c499a535dadcf09286";
        public const string WEAPON_BITE_D8 = "61bc14eca5f8c1040900215000cfc218";
        public const string WEAPON_BITE_2D6 = "2abc1dc6172759c42971bd04b8c115cb";
        public const string WEAPON_TOUCH = "bb337517547de1a4189518d404ec49d4"; // TouchItem
    }
}
