# Pathfinder Classes Wizard

This is a rewrite of the [Eldritch Arcana](https://github.com/jennyem/pathfinder-mods) mod by [jennyem](https://github.com/jennyem/).

It allows you to create new character classes as JSON files and load them automatically without any coding.

See docs/examples for the Charlatan class.

Please be aware that this mod does not contain any of the classes of Eldritch Arcana.

I did this for three reasons:
- add my favorite character class: the charlatan from DSA to Pathfinder
- understand how Pathfinder Kingmaker works internally
- train C# after some years

I used a lot of code from the original mod but did a lot of refactoring to simplify adding more classes.

As the original mod is 3 years old and Wrath of the Righteous was released about a month ago, I've decided to share this.

This mod was build on macOS with Jetbrains Rider.
To build the mod, make sure that the KINGMAKER_DIR variable is set to the root folder where Kingmaker.app and the Mods directory reside.

Have fun with it, but be aware that I will not provide any support for this. 

Gerrit

## JSON Structure for Character Class

| Key                      | Required | Example                            | Description |
|--------------------------|----------|------------------------------------|-------------|
| Guid                     | true     | "4da4a7d55cee43608a64babeb8d3ca73" | A 32 character GUID |
| Name                     | true     | "CharlatanClass"                   | The name of the character class, used internally |
| DisplayName              | true     | "Charlatan"                        | The name of the character class that is shown to the users |
| Description              | false    | "This is a cool character class."  | The description of the character class |
| Icon                     | true     | "ref:ROGUE"                        | Icon representation of the character class, taken from another character class. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the character class identifiers listed below |
| EquipmentEntities        | true     | "ref:ROGUE"                        | Equipment of the character class' doll, taken from another character class. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the character class identifiers listed below |
| MaleEquipmentEntities    | false    | "ref:ROGUE"                        | See EquipmentEntities, default is the same as for EquipmentEntities if not defined otherwise |
| FemaleEquipmentEntities  | false    | "ref:ROGUE"                        | See EquipmentEntities, default is the same as for EquipmentEntities if not defined otherwise |
| PrimaryColor             | true     | 0                                  | The character class' primary color as int, see color table for values |
| SecondaryColor           | true     | 19                                 | The character class' secondary color as int, see color table for values |
| SkillPoints              | true     | 5                                  | The skill points a character recieves on a new level |
| HitDie                   | false    | "D8"                               | Default: D6; The hit die the character uses. The value has to be one of the hit die identifiers listed below |
| BaseAttackBonus          | false    | "BAB_LOW"                          | Default: BAB_LOW; How the base attack bonus grows when the character level. The value has to be one of the base attack bonus identifiers listed below |
| FortitudeSave            | false    | "SAVES_LOW"                        | Default: SAVES_LOW; How the fortidude bonus grows when the character level. The value has to be one of the save roll identifiers listed below |
| WillSave                 | false    | "SAVES_HIGH"                       | Default: SAVES_LOW; How the fortidude bonus grows when the character level. The value has to be one of the save roll identifiers listed below |
| ReflexSave               | false    | "SAVES_LOW"                        | Default: SAVES_LOW; How the fortidude bonus grows when the character level. The value has to be one of the save roll identifiers listed below |
| IsDivineCaster           | false    | false                              | Default: false; Specifies if the character class is a divine caster |
| IsArcaneCaster           | false    | false                              | Default: false; Specifies if the character class is an arcane caster |
| ComponentsArray          | false    | "ref:ROGUE"                        | Default: nothing; Components of the character class, taken from another character class. This is not required and can be left out. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the character class identifiers listed below |
| StartingGold             | false    | 500                                | Default: 500; The amount of gold your character starts with |
| StartingItems            | false    | "ref:ROGUE"                        | Default: nothing; The items your character starts with, taken from another character class. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the character class identifiers listed below |
| Alignment                | false    | [ "NeutralGood", "ChaoticGood" ]   | Default: Any; The possible alignments for the character starts. Not listed alignments are not allowed. The values has to be from the alignment identifiers listed below |
| ClassSkills              | false    | [ "Thievery", "Stealth" ]          | Default: nothing; The skills to use as class skills for the character starts. The values has to be from the skill identifiers listed below |
| RecommendedAttributes    | false    | [ "Dexterity", "Charisma" ]        | Default: nothing; The attributes recommended for the character starts. The values has to be from the attributes identifiers listed below |
| NotRecommendedAttributes | false    | [ "Dexterity", "Charisma" ]        | Default: nothing; The attributes not recommended for the character starts. The values has to be from the attributes identifiers listed below |
| Progression              | true     | See below                          | See below |
| Proficiencies            | false    | See below                          | See below |
| Cantrips                 | false    | See below                          | See below |
| Spellbook                | false    | See below                          | See below |

### Progression JSON Structure

| Key                      | Required | Example                            | Description |
|------------------------- |----------|------------------------------------|-------------|
| Guid                     | true     | "3106acb568bb47a0b3d11adc6c378c14" | A 32 progression GUID |
| Name                     | true     | "CharlatanProgression"             | The name of the progression class, used internally |
| LevelEntries             | true     | "LevelEntries": { }                | See below; The features a character gains on a certain level |
| UiDeterminatorsGroup     | false    | [ "BARD_PROFICIENCIES" ]           | Default: nothing; The list of features from the first level to highlight. Use "<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| UiGroups                 | false    | See below                          | See below; Which features from the level entries should be shown in a row |

Defining the LevelEntries is quite easy, so learn from an example.
You may leave out levels where no features should be added.

```
"LevelEntries": {
  "1": [
    "COMMON_DETECT_MAGIC",
    "INQUISITOR_TACTICAL_LEADER_DIPLOMACY",
    "BARD_BARDIC_KNOWLEDGE"
  ],
  "2": [
    "COMMON_EVASION",
    "ROGUE_TALENT_SELECTION",
    "WIZARD_FEAT_SELECTION"
  ],
  "8": [
    "ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD",
    "ROGUE_TALENT_SELECTION"
  ],
  "20": [
    "ROGUE_TALENT_SELECTION"
  ]
}
```

This UiGroups definition will show the ROGUE_TALENT_SELECTION as one row and in a second row the WIZARD_FEAT_SELECTION together with the ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD. 

```
"UiGroups": [
  [
    "ROGUE_TALENT_SELECTION"
  ],[
    "WIZARD_FEAT_SELECTION",
    "ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD"
  ]
]
```

### Cantrips JSON Structure

Cantrips depends on a spellbook.
The cantrips use the stat from the spellbook and the spells of level 0 from the spellbook's spells list.
The cantrips feature will automatically be added to the features of the first level and to the UiDeterminatorsGroup of the progression.

| Key         | Required | Example                                   | Description |
|-------------|----------|-------------------------------------------|-------------|
| Guid        | true     | "2464f9cfc5a34b608b5a9edb9c5f6e7b"        | A 32 cantrips feature GUID |
| Name        | true     | "CharlatanCantrips"                       | The name of the cantrips feature class, used internally |
| DisplayName | true     | "Cantrips"                                | The name of the cantrips feature that is shown to the users |
| Description | false    | "Charlatans have cantrips ;-)"            | The description of the cantrips feature, if not defined DisplayName is used |
| Icon        | true     | "ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY" | Icon representation of the cantrips feature, taken from another feature. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |

### Proficiencies JSON Structure

Be aware that you need to define at least "from", "Add", or both.
The proficiencies feature will automatically be added to the features of the first level and to the UiDeterminatorsGroup of the progression.

| Key                      | Required | Example                            | Description |
|------------------------- |----------|------------------------------------|-------------|
| Guid                     | true     | "6d817419f36c4ba7833466e434a7fbd9" | A 32 proficiencies feature GUID |
| Name                     | true     | "CharlatanProficiencies"           | The name of the proficiencies feature class, used internally |
| DisplayName              | true     | "Charlatan Proficiencies"          | The name of the proficiencies feature that is shown to the users |
| Description              | false    | "Cool proficiencies"               | The description of the proficiencies feature, if not defined DisplayName is used |
| Icon                     | false    | "ref:BARD_PROFICIENCIES"           | Default: nothingn; Icon representation of the proficiencies feature, taken from another feature. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| Add                      | false    | See below                          | Features, weapon proficiencies, and armor proficiencies to add with this proficiencies feature. |
| Add->Features            | false    | [ "WEAPON_PROFICIENCY_ESTOC" ]     | Default: nothing; The list of features to add with the proficiencies. Use "<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| Add->WeaponProficiencies | false    | [ "Longbow", "Starknife" ]         | Default: nothing; The list of weapon skills to add with the proficiencies. Use "<IDENTIFIER>" where <IDENTIFIER> is one of the weapon identifiers |
| Add->ArmorProficiencies  | false    | [ "Light", "Buckler" ]             | Default: nothing; The list of armor skills to add with the proficiencies. Use "<IDENTIFIER>" where <IDENTIFIER> is one of the armor identifiers |
| From                     | false    | "BARD_PROFICIENCIES"               | Proficiencies feature to derive these proficiencies. Use "<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |

### Character Class Identifiers

Possible values: ALCHEMIST, BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, INQUISITOR, KINETICIST, MAGUS, MONK, PALADIN, RANGER, ROGUE, SLAYER, SORCERER, WIZARD    

### Hit Die Identifiers

Possible values: D2, D3, D4, D6, D8, D10, D12, D20, D100

(Only D6, D8, D10 make sense ;-)

### Base Attack Bonus Identifiers

Possible values: BAB_LOW, BAB_MEDIUM, BAB_FULL

### Save Roll Identifiers

Possible values: SAVES_LOW, SAVES_HIGH

### Alignment Identifiers

Possible values: LawfulGood, NeutralGood, ChaoticGood, LawfulNeutral, TrueNeutral, ChaoticNeutral, LawfulEvil, NeutralEvil, ChaoticEvil, Good, Evil, Lawful, Chaotic, Any

### Skill Identifiers

Possible values: Athletics, Perception, Thievery, Stealth, KnowledgeArcana, KnowledgeWorld, LoreNature, LoreReligion, Persuasion, UseMagicDevice

### Attribute Identifiers

Possible values: Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma

### Weapon Identifiers

Possible values: Bardiche, BastardSword, Battleaxe, Bite, Bomb, Claw, Club, Dagger, Dart, DoubleAxe, DoubleSword, DuelingSword, DwarvenWaraxe, EarthBreaker, ElvenCurvedBlade, Estoc, Falcata, Falchion, Fauchard, Flail, Glaive, Gore, Greataxe, Greatclub, Greatsword, Handaxe, HandCrossbow, HeavyCrossbow, HeavyFlail, HeavyMace, HeavyPick, HeavyRepeatingCrossbow, HookedHammer, Javelin, Kama, KineticBlast, Kukri, LightCrossbow, LightHammer, LightMace, LightPick, LightRepeatingCrossbow, Longbow, Longspear, Longsword, Nunchaku, OtherNaturalWeapons, PunchingDagger, Quarterstaff, Rapier, Ray, Sai, Scimitar, Scythe, Shortbow, Shortspear, Shortsword, Shuriken, Siangham, Sickle, Sling, SlingStaff, Spear, SpikedHeavyShield, SpikedLightShield, Starknife, ThrowingAxe, Tongi, Touch, Trident, UnarmedStrike, Urgrosh, Warhammer, WeaponHeavyShield, WeaponLightShield

### Armor Identifiers

Possible values: Light, Medium, Heavy, Buckler, LightShield, HeavyShield, TowerShield
