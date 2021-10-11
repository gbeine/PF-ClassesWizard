using System;

namespace PF_Classes.Identifier
{
    public class Features : Identifier
    {
        public static readonly Features INSTANCE = new Features();

        private Features() { }

        public const String WEAPON_PROFICIENCY_SIMPLE             = "e70ecf1ed95ca2f40b754f1adb22bbdd"; // Umgang mit einfachen Waffen
        public const String WEAPON_PROFICIENCY_WAR                = "203992ef5b35c864390b4e4a1e200629"; // Umgang mit Kriegswaffen
        public const String WEAPON_PROFICIENCY_DUELLING_SWORD     = "9c37279588fd9e34e9c4cb234857492c"; // Waffenumgang (Duellschwert)
        public const String WEAPON_PROFICIENCY_BASTARD_SWORD      = "57299a78b2256604dadf1ab9a42e2873"; // Waffenumgang (Bastardschwert)
        public const String WEAPON_PROFICIENCY_ELVEN_CURVED_BLADE = "0fca9259e370cd049a1dd50bede687f7"; // Waffenumgang (Gekrümmte Elfenklinge)
        public const String WEAPON_PROFICIENCY_ESTOC              = "9dc64f0b9161a354c9471a631318e16c"; // Waffenumgang (Estoc)
        public const String WEAPON_PROFICIENCY_LONGBOW            = "0978f630fc5d6a6409ac641137bf6659"; // Waffenumgang (Langbogen)
        public const String WEAPON_PROFICIENCY_LONGSWORD          = "62e27ffd9d53e14479f73da29760f64e"; // Waffenumgang (Langschwert)
        public const String WEAPON_PROFICIENCY_RAPIER             = "292d51f3d6a331644a8c29be0614f671"; // Waffenumgang (Rapier)
        public const String WEAPON_PROFICIENCY_SCIMITAR           = "75146ee0b32e5424ab77902bf86f91ee"; // Waffenumgang (Krummsäbel)
        public const String WEAPON_PROFICIENCY_SHORTBOW           = "e8096942d950c8843857c2545f8dc18f"; // Waffenumgang (Kurzbogen)
        public const String WEAPON_PROFICIENCY_SHORTSWORD         = "9e828934974f0fc4bbf7542eb0446e45"; // Waffenumgang (Kurzschwert)
        public const String WEAPON_PROFICIENCY_STARKNIFE          = "7818ba3db79ac064e88fa14a2478b24b"; // Waffenumgang (Sternmesser)
        
        public const String ARMOUR_PROFICIENCY_LIGHT        = "6d3728d4e9c9898458fe5e9532951132"; // Umgang mit Rüstungen (Leichte Rüstung)
        public const String ARMOUR_PROFICIENCY_MEDIUM       = "46f4fb320f35704488ba3d513397789d"; // Umgang mit Rüstungen (Mittelschwere Rüstung)
        public const String ARMOUR_PROFICIENCY_HEAVY        = "1b0f68188dcc435429fb87a022239681"; // Umgang mit Rüstungen (Schwere Rüstung)
        public const String ARMOUR_PROFICIENCY_SHIELD       = "cb8686e7357a68c42bdd9d4e65334633"; // Umgang mit Schilden
        public const String ARMOUR_PROFICIENCY_TOWER_SHIELD = "6105f450bb2acbd458d277e71e19d835"; // Umgang mit Turmschilden

        public const String COMMON_DETECT_MAGIC     = "ee0b69e90bac14446a4cf9a050f87f2e"; // Magie orten
        public const String COMMON_SNEAK_ATTACK     = "9b9eac6709e1c084cb18c3a366e0ec87"; // Hinterhältiger Angriff
        public const String COMMON_ADVANCED_TALENTS = "a33b99f95322d6741af83e9381b2391c"; // Verbesserte Tricks
        public const String COMMON_QUARRY           = "385260ca07d5f1b4e907ba22a02944fc"; // Beute
        public const String COMMON_IMPROVED_QUARRY  = "25e009b7e53f86141adee3a1213af5af"; // Verbesserte Beute
        public const String COMMON_UNCANNY_DODGE    = "3c08d842e802c3e4eb19d15496145709"; // Reflexbewegung
        public const String COMMON_EVASION          = "576933720c440aa4d8d42b0c54b77e80"; // Entrinnen
        public const String COMMON_IMPROVED_EVASION = "ce96af454a6137d47b9c6a1e02e66803"; // Verbessertes Entrinnen

        public const String ARCANE_SCHOOL_ILLUSION_BLINDING_RAY = "9be5e050244352d43a1cb50aad8d548f"; // Blendender Strahl
        public const String ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD = "f0585eb111ede2c4ebf00b057d069463"; // Unsichtbarkeitsfeld

        public const String ARCANE_SCHOOL_SPECIAL_ABJURATION = "30f20e6f850519b48aa59e8c0ff66ae9"; // Spezialisierte Schule – Bannzauber
        public const String ARCANE_SCHOOL_OPPOSED_ABJURATION = "7f8c1b838ff2d2e4f971b42ccdfa0bfd"; // Gegensätzliche Schule – Bannzauber
        public const String ARCANE_SCHOOL_SPECIAL_DIVINATION = ""; // Spezialisierte Schule – Erkenntniszauber
        public const String ARCANE_SCHOOL_OPPOSED_DIVINATION = "09595544116fe5349953f939aeba7611"; // Gegensätzliche Schule – Erkenntniszauber
        public const String ARCANE_SCHOOL_SPECIAL_CONJURATION = "cee0f7edbd874a042952ee150f878b84"; // Spezialisierte Schule – Beschwörung
        public const String ARCANE_SCHOOL_OPPOSED_CONJURATION = "ca4a0d68c0408d74bb83ade784ebeb0d"; // Gegensätzliche Schule – Beschwörung
        public const String ARCANE_SCHOOL_SPECIAL_ENCHANTMENT = ""; // Spezialisierte Schule – Verzauberung
        public const String ARCANE_SCHOOL_OPPOSED_ENCHANTMENT = "875fff6feb84f5240bf4375cb497e395"; // Gegensätzliche Schule – Verzauberung
        public const String ARCANE_SCHOOL_SPECIAL_EVOCATION = "c46512b796216b64899f26301241e4e6"; // Spezialisierte Schule – Hervorrufung
        public const String ARCANE_SCHOOL_OPPOSED_EVOCATION = "c3724cfbe98875f4a9f6d1aabd4011a6"; // Gegensätzliche Schule – Hervorrufung
        public const String ARCANE_SCHOOL_SPECIAL_ILLUSION = ""; // Spezialisierte Schule – Illusion
        public const String ARCANE_SCHOOL_OPPOSED_ILLUSION = "6750ead44c0c034428c6509c68110375"; // Gegensätzliche Schule – Illusion
        public const String ARCANE_SCHOOL_SPECIAL_NECROMANCY = "927707dce06627d4f880c90b5575125f"; // Spezialisierte Schule – Nekromantie
        public const String ARCANE_SCHOOL_OPPOSED_NECROMANCY = "a9bb3dcb2e8d44a49ac36c393c114bd9"; // Gegensätzliche Schule – Nekromantie
        public const String ARCANE_SCHOOL_SPECIAL_TRANSMUTATION = "c459c8200e666ef4c990873d3e501b91"; // Spezialisierte Schule – Verwandlung
        public const String ARCANE_SCHOOL_OPPOSED_TRANSMUTATION = "fc519612a3c604446888bb345bca5234"; // Gegensätzliche Schule – Verwandlung
        public const String ARCANE_SCHOOL_SPECIAL_UNIVERSALIST = ""; // Spezialisierte Schule – Universalist

        public const String BARD_PROFICIENCIES            = "fa3d3b2211a51994785d85e753f612d3"; // Barden-Kenntnisse
        public const String BARD_CANTRIPS                 = "4f422e8490ec7d94592a8069cce47f98"; // Zaubertricks
        public const String BARD_BARDIC_PERFORMANCE       = "b92bfc201c6a79e49afd0b5cfbfc269f"; // Bardenauftritt
        public const String BARD_INSPIRE_COURAGE          = "acb4df34b25ca9043a6aba1a4c92bc69"; // Lied des Mutes
        public const String BARD_BARDIC_KNOWLEDGE         = "65cff8410a336654486c98fd3bacd8c5"; // Bardenwissen
        public const String BARD_DETECT_MAGIC             = COMMON_DETECT_MAGIC;
        public const String BARD_TALENT                   = "94e2cd84bf3a8e04f8609fe502892f4f"; // Bardentalent
        public const String BARD_WELL_VERSED              = "8f4060852a4c8604290037365155662f"; // Bewandert
        public const String BARD_INSPIRER_COMPETENCE      = "6d3fcfab6d935754c918eb0e004b5ef7"; // Lied des Erfolges
        public const String BARD_FASCINATE                = "ddaec3a5845bc7d4191792529b687d65"; // Faszinieren
        public const String BARD_BARDIC_PERFORMANCE_MOVE  = "36931765983e96d4bb07ce7844cd897e"; // Bardenauftritt (Bewegungsaktion)
        public const String BARD_DIRGE_OF_DOOM            = "1d48ab2bded57a74dad8af3da07d313a"; // Mächtiges Klagelied
        public const String BARD_INSPIRE_GREATNESS        = "9ae0f32c72f8df84dab023d1b34641dc"; // Lied der Größe
        public const String BARD_JACK_OF_ALL_TRADES       = "21fbafd5dc42d4d488c4d6caed46bc99"; // Tausendsassa
        public const String BARD_SOOTHING_PERFORMANCE     = "546698146e02d1e4ea00581a3ea7fe58"; // Erfrischender Auftritt
        public const String BARD_BARDIC_PERFORMANCE_SWIFT = "fd4ec50bc895a614194df6b9232004b9"; // Bardenauftritt (Schnelle Aktion)
        public const String BARD_FRIGHTENING_TUNE         = "cfd8940869a304f4aa9077415f93febe"; // Lied der Furcht
        public const String BARD_INSPIRE_HEROICS          = "199d6fa0de149d044a8ab622a542cc79"; // Lied des Heldenmuts
        public const String BARD_DEADLY_PERFORMANCE       = "a6e13797b0a20d2458a086a8a511fd8c"; // Tödliche Melodie

        public const String FIGHTER_BONUS_COMBAT    = "41c8486641f7d6d4283ca9dae4147a9f"; // Bonus-Kampftalent
        public const String FIGHTER_PROFICIENCIES   = "a23591cc77086494ba20880f87e73970"; // Kämpfer-Kenntnisse
        public const String FIGHTER_BRAVERY         = "f6388946f9f472f4585591b80e9f2452"; // Tapferkeit
        public const String FIGHTER_ARMOR_TRAINING  = "3c380607706f209499d951b29d3c44f3"; // Rüstungstraining
        public const String FIGHTER_WEAPON_TRAINING = "b8cecf4e5e464ad41b79d5b42b76b399"; // Waffentraining
        public const String FIGHTER_ARMOR_MASTERY   = "ae177f17cfb45264291d4d7c2cb64671"; // Rüstungsmeisterschaft
        public const String FIGHTER_WEAPON_MASTERY  = "55f516d7d1fc5294aba352a5a1c92786"; // Waffenmeisterschaft

        public const String FIGHTER_ALDORI_DEFENDER_DEFENSIVE_PARRY  = "faa56353e193e5940a305c60b4412ea5"; // Wehrhafte Parade
        public const String FIGHTER_ALDORI_DEFENDER_DISARMING_STRIKE = "c7ea46f5e1822994ba069c11819844ae"; // Entwaffnender Schlag
        public const String FIGHTER_ALDORI_DEFENDER_STEEL_NET        = "b4202533d1748f84484658491d2ff766"; // Stahlnetz
        public const String FIGHTER_ALDORI_DEFENDER_COUNTERATTACK    = "ff125ecc8b2c1894b879b7bcf34e1b17"; // Gegenangriff
       
        public const String INQUISITOR_PROFICIENCIES      = "e59db96fa83cefd4a9a8f211500d9522"; // Inquisitor-Kenntnisse
        public const String INQUISITOR_DEITY              = "59e7a76987fe3b547b9cce045f4db3e4"; // Wahl einer Gottheit
        public const String INQUISITOR_ORISONS            = "4f898e6a004b2a84686c1fbd0ffe950e"; // Gebete
        public const String INQUISITOR_STERN_GAZE         = "a6d917fd5c9bee0449bd01c92e3b0308"; // Durchdringender Blick
        public const String INQUISITOR_DOMAIN             = "48525e5da45c9c243a343fc6545dbdb9"; // Domänen-Auswahl
        public const String INQUISITOR_JUDGMENT           = "981def910b98200499c0c8f85a78bde8"; // Richtspruch
        public const String INQUISITOR_DETECT_MAGIC       = COMMON_DETECT_MAGIC;
        public const String INQUISITOR_CUNNING_INITIATIVE = "6be8b4031d8b9fc4f879b72b5428f1e0"; // Gewiefte Initiative
        public const String INQUISITOR_SOLO_TACTICS       = "5602845cd22683840a6f28ec46331051"; // Einzelkämpfertaktik
        public const String INQUISITOR_TEAMWORK           = "d87e2f6a9278ac04caeb0f93eff95fcb"; // Gemeinschaftstalent
        public const String INQUISITOR_BANE               = "7ddf7fbeecbe78342b83171d888028cf"; // Verderben
        public const String INQUISITOR_SECOND_JUDGMENT    = "33bf0404b70d65f42acac989ec5295b2"; // Zweiter Richtspruch
        public const String INQUISITOR_STALWART           = "ec9dbc9a5fa26e446a54fe5df6779088"; // Standhaft
        public const String INQUISITOR_GREATER_BANE       = "6e694114b2f9e0e40a6da5d13736ff33"; // Mächtiges Verderben
        public const String INQUISITOR_EXPLOIT_WEAKNESS   = "374a73288a36e2d4f9e54c75d2e6e573"; // Schwachstelle ausnutzen
        public const String INQUISITOR_THIRD_JUDGMENT     = "490c7e92b22cc8a4bb4885a027b355db"; // Dritter Richtspruch
        public const String INQUISITOR_TRUE_JUDGMENT      = "f069b6557a2013544ac3636219186632"; // Wahrer Richtspruch
        
        public const String INQUISITOR_TACTICAL_LEADER_LEADERS_WORDS       = "1aa5b1074a6e3834dbc11b58332a6e99"; // Überzeugender Anführer
        public const String INQUISITOR_TACTICAL_LEADER_SWIFT_TACTICIAN     = "93e78cad499b1b54c859a970cbe4f585"; // Schneller Taktiker
        public const String INQUISITOR_TACTICAL_LEADER_SWIFT_TACTICIAN_2ND = "4ca47c023f1c158428bd55deb44c735f"; // Schneller Taktiker
        public const String INQUISITOR_TACTICAL_LEADER_BATTLE_ACUMEN       = "959a8bdc83b0a3948a3902f9882c522a"; // Kriegerisches Können

        public const String KINETICIST_BURN                     = "57e3577a0eb53294e9d7cc649d5239a3"; // Zehrung
        public const String KINETICIST_WEAPON_KINETIC_BLAST     = "31ad04e4c767f5d4b96c13a71fd7ff15"; // Kinetisches Geschoss
        public const String KINETICIST_GATHER_POWER             = "71f526b1d4b50b94582b0b9cbe12b0e0"; // Energie sammeln
        public const String KINETICIST_ELEMENTAL_FOCUS          = "1f3a15a3ae8a5524ab8b97f469bf4e3d"; // Elementarfokus
        public const String KINETICIST_INFUSION                 = "58d6f8e9eea63f6418b107ce64f315ea"; // Infusion
        public const String KINETICIST_KINETIC_BLAST            = "30a5b8cf728bd4a4d8d90fc4953e322e"; // Kinetisches Geschoss
        public const String KINETICIST_ELEMENTAL_OVERFLOW       = "86beb0391653faf43aec60d5ec05b538"; // Elementare Aufladung
        public const String KINETICIST_INFUSION_SPECIALIST      = "1f86ce843fbd2d548a8d88ea1b652452"; // Infusionsspezialist
        public const String KINETICIST_WILD_TALENT              = "5c883ae0cd6d7d5448b7a420f51f8459"; // Wilde Gaben
        public const String KINETICIST_META_EMPOWERED           = "70322f5a2a294e54a9552f77ee85b0a7"; // Metakinese – Verstärkt
        public const String KINETICIST_EXPANDED_ELEMENT         = "4204bc10b3d5db440b1f52f0c375848b"; // Erweiterter Elementarfokus
        public const String KINETICIST_META_MAXIMISED           = "0306bc7c6930a5c4b879c7dea78208c2"; // Metakinese – Maximiert
        public const String KINETICIST_SUPERCHARGE              = "5a13756fb4be25f46951bc3f16448276"; // Überlasten
        public const String KINETICIST_META_QUICKENED           = "4bb9d2328a3fdca419243d6116b337ac"; // Metakinese – Beschleunigen
        public const String KINETICIST_EXPANDED_ELEMENT_2ND     = "e2c1718828fc843479f18ab4d75ded86"; // Erweiterter Elementarfokus
        public const String KINETICIST_KINETIC_BLAST_SPECIALIST = "df8897708983d4846871ca72c4cbfc52"; // Kinetischer Geschossspezialist
        public const String KINETICIST_META_MASTERY             = "8c33002186eb2fd45a140eed1301e207"; // Metakinetische Meisterschaft
        
        public const String KINETICIST_DARK_ELEMENTALIST_DARK_STUDIES   = "abec06c7bf762274ca6f7e6013746595"; // Dunkle Studien
        public const String KINETICIST_DARK_ELEMENTALIST_SOUL_POWER     = "42c5a9a8661db2f47aedf87fb8b27aaf"; // Seelenmacht
        public const String KINETICIST_DARK_ELEMENTALIST_ELEMENTAL_LOAD = "a5da720ab9f2fcd43807226a5146e4c6"; // Elementare Aufladung

        public const String KINETICIST_KINETIC_KNIGHT_BLADE             = "9ff81732daddb174aa8138ad1297c787"; // Kinetische Klinge
        public const String KINETICIST_KINETIC_KNIGHT_ELEMENTAL_FOCUS   = "b1f296f0bd16bc242ae35d0638df82eb"; // Elementarfokus
        public const String KINETICIST_KINETIC_KNIGHT_ELEMENTAL_BLADE   = "22a6db57adc348b458cb224f3047b3b2"; // Elementarklinge
        public const String KINETICIST_KINETIC_KNIGHT_KINETIC_WARRIOR   = "ff14cb2bfab1c0547be66d8aaa7e4ada"; // Kinetischer Krieger
        public const String KINETICIST_KINETIC_KNIGHT_ELEMENTAL_BASTION = "82fbdd5eb5ac73b498c572cc71bda48f"; // Elementarbastion
        public const String KINETICIST_KINETIC_KNIGHT_WHIRLWIND         = "80fdf049d396c33408a805d9e21a42e1"; // Klingenwirbel
        public const String KINETICIST_KINETIC_KNIGHT_RESOLVE           = "e65a711b3fd6e604b8edd03c08902f7e"; // Entschlossenheit des Ritters

        public const String RANGER_PROFICIENCIES     = "c5e479367d07d62428f2fe92f39c0341"; // Waldläufer-Kenntnisse
        public const String RANGER_FAVORED_ENEMY     = "16cc2c937ea8d714193017780e7d4fc6"; // Erzfeind
        public const String RANGER_COMBAT_STYLE      = "c6d0da9124735a44f93ac31df803b9a9"; // Kampfstiltalent
        public const String RANGER_FAVORED_TERRAIN   = "a6ea422d7308c0d428a541562faedefd"; // Bevorzugtes Gelände
        public const String RANGER_ENDURANCE         = "54ee847996c25cd4ba8773d7b8555174"; // Ausdauer
        public const String RANGER_HUNTERS_BOND      = "b705c5184a96a84428eeb35ae2517a14"; // Bund des Jägers
        public const String RANGER_FAVORED_ENEMY_2ND = "c1be13839472aad46b152cf10cf46179"; // Erzfeind
        public const String RANGER_COMBAT_STYLE_2ND  = "61f82ba786fe05643beb3cd3910233a8"; // Kampfstiltalent
        public const String RANGER_EVASION           = COMMON_EVASION;
        public const String RANGER_COMBAT_STYLE_3RD  = "78177315fc63b474ea3cbb8df38fafcd"; // Kampfstiltalent
        public const String RANGER_QUARRY            = COMMON_QUARRY;
        public const String RANGER_CAMOUFLAGE        = "ff1b5aa8dcc7d7d4d9aa85e1cb3f9e88"; // Tarnung
        public const String RANGER_IMPROVED_EVASION  = COMMON_IMPROVED_EVASION;
        public const String RANGER_IMPROVED_QUARRY   = COMMON_IMPROVED_QUARRY;
        public const String RANGER_MASTER_HUNTER     = "9d53ef63441b5d84297587d75f72fc17"; // Meisterjäger

        public const String ROUGE_SNEAK_ATTACK           = COMMON_SNEAK_ATTACK;
        public const String ROUGE_PROFICIENCIES          = "33e2a7e4ad9daa54eaf808e1483bb43c"; // Schurken-Kenntnisse
        public const String ROUGE_WEAPON_FINESSE         = "90e54424d682d104ab36436bd527af09"; // Waffenfinesse
        public const String ROUGE_TRAPFINDING            = "dbb6b3bffe6db3547b31c3711653838e"; // Fallen finden
        public const String ROUGE_EVASION                = COMMON_EVASION;
        public const String ROUGE_TALENT                 = "c074a5d615200494b8f2a9c845799d93"; // Schurkentrick
        public const String ROUGE_FINESSE_TRAINING       = "b78d146cea711a84598f0acef69462ea"; // Waffenfinessetraining
        public const String ROUGE_DANGER_SENSE           = "0bcbe9e450b0e7b428f08f66c53c5136"; // Gefahrengespür
        public const String ROUGE_DEBILITATING_INJURY    = "def114eb566dfca448e998969bf51586"; // Schwächende Verletzung
        public const String ROUGE_UNCANNY_DODGE          = COMMON_UNCANNY_DODGE;
        public const String ROUGE_IMPROVED_UNCANNY_DODGE = "485a18c05792521459c7d06c63128c79"; // Verbesserte Reflexbewegung
        public const String ROUGE_ADVANCED_TALENTS       = COMMON_ADVANCED_TALENTS;
        public const String ROUGE_MASTER_STRIKE          = "72dcf1fb106d5054a81fd804fdc168d3"; // Meisterhafter Angriff

        public const String SLAYER_STUDIED_TARGET       = "09bdd9445ac38044389476689ae8d5a1"; // Feind studieren
        public const String SLAYER_TALENT               = "04430ad24988baa4daa0bcd4f1c7d118"; // Attentätertricks
        public const String SLAYER_SNEAK_ATTACK         = COMMON_SNEAK_ATTACK;
        public const String SLAYER_TALENT_2ND           = "43d1b15873e926848be2abf0ea3ad9a8"; // Attentätertricks
        public const String SLAYER_STUDIED_TARGET_SWIFT = "40d4f55a5ac0e4f469d67d36c0dfc40b"; // Feind studieren (schnell)
        public const String SLAYER_TALENT_3RD           = "913b9cf25c9536949b43a2651b7ffb66"; // Attentätertricks
        public const String SLAYER_ADVANCED_TALENTS     = COMMON_ADVANCED_TALENTS;
        public const String SLAYER_ADVANCE              = "758e04f4568ec3f4c8d1fd83aed06fb9"; // Schneller Schritt
        public const String SLAYER_QUARRY               = COMMON_QUARRY;
        public const String SLAYER_ADVANCE_2ND          = "f4df3231977753d4ebd2ceb2ef5901ed"; // Schneller Schritt
        public const String SLAYER_IMPROVED_QUARRY      = COMMON_IMPROVED_QUARRY;
        public const String SLAYER_MASTERY              = "a26c0279a423fc94cabeea898f4d9f8a"; // Meisterattentäter

        public const String SLAYER_VANGUARD_TACTICIAN     = "a14ff5eeeec5f434eb5c7fb8201afefb"; // Taktischer Vorhutkämpfer
        public const String SLAYER_VANGUARD_TEAMWORK      = "90b882830b3988446ae681c6596460cc"; // Gemeinschaftstalent
        public const String SLAYER_VANGUARD_BOND          = "4b4d4cae104579a4186070f3f62210fd"; // Bund mit den Gefährten
        public const String SLAYER_VANGUARD_UNCANNY_DODGE = COMMON_UNCANNY_DODGE;

        public const String WIZARD_BONUS             = "8c3102c2ff3b69444b139a98521a4899"; // Magier-Bonustalent
        public const String WIZARD_CANTRIPS          = "44d19b62d00179e4bad7afae7684f2e2"; // Zaubertricks
        public const String WIZARD_SPECIALIST_SCHOOL = "5f838049069f1ac4d804ce0862ab5110"; // Spezialisierte Schule
        public const String WIZARD_PROFICIENCIES     = "a98d7cc4e30fe6c4bb3a2c2f69acc3fe"; // Magier-Kenntnisse
        public const String WIZARD_SPELLCASTING      = "9fc9813f569e2e5448ddc435abf774b3"; // Vollwertiger Zauberwirker
        public const String WIZARD_ARCANE_BOND       = "03a1781486ba98043afddaabf6b7d8ff"; // Arkane Verbindung
        public const String WIZARD_DETECT_MAGIC      = COMMON_DETECT_MAGIC;
        
        public const String WIZARD_THASSILONIAN_SPECIALIST = "f431178ec0e2b4946a34ab504bb46285"; // Thassilonspezialist
    }
}
