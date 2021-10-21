# Pathfinder Classes Wizard

This is a rewrite of the [Eldritch Arcana](https://github.com/jennyem/pathfinder-mods) mod by [jennyem](https://github.com/jennyem/).

You may follow the mod on [Nexus Mods](https://www.nexusmods.com/pathfinderkingmaker/mods/248)

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
| BaseAttackBonus          | false    | "ref:BAB_LOW"                      | Default: BAB_LOW; How the base attack bonus grows when the character level. Use "ref:<IDENTIFIER>" where <IDENTIFIER> has to be one of the base attack bonus identifiers listed below |
| FortitudeSave            | false    | "ref:SAVES_LOW"                    | Default: SAVES_LOW; How the fortidude bonus grows when the character level. Use "ref:<IDENTIFIER>" where <IDENTIFIER> has to be one of the save roll identifiers listed below |
| WillSave                 | false    | "ref:SAVES_HIGH"                   | Default: SAVES_LOW; How the fortidude bonus grows when the character level. Use "ref:<IDENTIFIER>" where <IDENTIFIER> has to be one of the save roll identifiers listed below |
| ReflexSave               | false    | "ref:SAVES_LOW"                    | Default: SAVES_LOW; How the fortidude bonus grows when the character level. Use "ref:<IDENTIFIER>" where <IDENTIFIER> has to be one of the save roll identifiers listed below |
| IsDivineCaster           | false    | false                              | Default: false; Specifies if the character class is a divine caster |
| IsArcaneCaster           | false    | false                              | Default: false; Specifies if the character class is an arcane caster |
| ComponentsArray          | false    | "ref:ROGUE"                        | Default: nothing; Components of the character class, taken from another character class. This is not required and can be left out. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the character class identifiers listed below |
| StartingGold             | false    | 500                                | Default: 411; The amount of gold your character starts with |
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
| UiDeterminatorsGroup     | false    | [ "ref:BARD_PROFICIENCIES" ]       | Default: nothing; The list of features from the first level to highlight. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| UiGroups                 | false    | See below                          | See below; Which features from the level entries should be shown in a row |

Defining the LevelEntries is quite easy, so learn from an example.
You may leave out levels where no features should be added.

```
"LevelEntries": {
  "1": [
    "ref:COMMON_DETECT_MAGIC",
    "ref:INQUISITOR_TACTICAL_LEADER_DIPLOMACY",
    "ref:BARD_BARDIC_KNOWLEDGE"
  ],
  "2": [
    "ref:COMMON_EVASION",
    "ref:ROGUE_TALENT_SELECTION",
    "ref:WIZARD_FEAT_SELECTION"
  ],
  "8": [
    "ref:ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD",
    "ref:ROGUE_TALENT_SELECTION"
  ],
  "20": [
    "ref:ROGUE_TALENT_SELECTION"
  ]
}
```

This UiGroups definition will show the ROGUE_TALENT_SELECTION as one row and in a second row the WIZARD_FEAT_SELECTION together with the ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD. 

```
"UiGroups": [
  [
    "ref:OGUE_TALENT_SELECTION"
  ],[
    "ref:WIZARD_FEAT_SELECTION",
    "ref:ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD"
  ]
]
```

### Spellbook JSON Structure

| Key                      | Required | Example                            | Description |
|------------------------- |----------|------------------------------------|-------------|
| Guid                     | true     | "0fc2fdfb15ec4abd888ef5a7b7e59003" | A 32 spellbook GUID |
| Name                     | true     | "CharlatanSpellbook"               | The name of the spellbook class, used internally |
| CastingAttribute         | true     | "Intelligence"                     | The attribute used by the character to cast spells from this book. The values has to be from the attributes identifiers listed below, usually Intelligence or Charisma |
| IsArcane                 | false    | true                               | Default: false; Determine something |
| IsSpontaneous            | false    | true                               | Default: false; Determine if the character can cast spontaneoulsy |
| CanCopyScrolls           | false    | true                               | Default: false; Determine if the character can copy scrolls |
| AllSpellsKnown           | false    | true                               | Default: false; Determine if the character has access to all spells in the spellbook. Do not use with a SpellsKnown table |
| SpellsPerLevel           | false    | 5                                  | Default: 0; #GBTODO What does this tell us? |
| CasterLevelModifier      | false    | 5                                  | Default: 0; #GBTODO What does this tell us? |
| Cantrips                 | false    | "Orisons"                          | Default: "Cantrips"; The type of cantrips in this book, one of "Orisons" or "Cantrips" |
| SpellList                | true     | See below                          | See below |
| SpellsPerDay             | true     | See below                          | See below |
| SpellsKnown              | false    | See below                          | See below; use only if AllSpellsKnown is false |

#### SpellList

A the SpellList can be referenced from another spellbook or definied individually.
If an existing spellbook should be used, use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the spellbook identifiers:

```
"SpellList": "ref:CLERIC_SPELLBOOK",
```

SpellList contains the list of possible spells to learn per level.
Please note: identifiers are mandatory and reference to the spell levels.
Identifiers need to start with 0 (the cantrips level) and end with the highest level possible in this book.
If there should be no cantrips, define level 0 as an empty array like `"0": [ ]`.
SpellList has a Guid and a Name like all other definitions.

```
"SpellList": {
  "Guid": "8b4fc86d687646648c551a740718118c",
  "Name": "CharlatanSpellList",
  "SpellsByLevel": {
    "0": [
      "Daze"
    ],
    "1": [
      "ref:CURE_LIGHT_WOUNDS_CAST",
      "ref:SUMMON_MONSTER_I_SINGLE"
    ],
    "2": [
      "ref:CURE_MODERATE_WOUNDS_CAST",
      "ref:SUMMON_MONSTER_II_BASE",
      "ref:MAGE_ARMOR",
      "ref:DELAY_POISON"
    ],
    ...
    "9": [
      "ref:SUMMON_MONSTER_IX_BASE"
    ]
  }
}
```

#### SpellsPerDay

A the SpellsPerDay can be referenced from another spellbook or definied individually.
If an existing spellbook should be used, use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the spellbook identifiers.

```
"SpellsPerDay": "ref:SORCERER_SPELLBOOK",
```

SpellsPerDay determines how many spells of this book a character is able to cast per day.
Each line in the Table shows how many spells of a certain spell level the character is able to cast per day.
Identifiers are mandatory and reference to the character levels.
The entry for level 0 is mandatory.
The first entry in each line is for spell level 0, the second for spell level one and so on.
SpellsPerDay has a Guid and a Name like all other definitions.

```
"SpellsPerDay": {
  "Name": "CharlatanSpellsPerDay",
  "Guid": "d9adb154906244f39fd7439a5f4d6ac2",
  "Table": {
    "0": [],
    "1": [0, 3],
    "2": [0, 4],
    "3": [0, 5, 3],
    ...
    "16": [0, 6, 6, 6, 6, 6, 6, 6, 4],
    "17": [0, 6, 6, 6, 6, 6, 6, 6, 5, 3],
    "18": [0, 6, 6, 6, 6, 6, 6, 6, 6, 4],
    "19": [0, 6, 6, 6, 6, 6, 6, 6, 6, 5],
    "20": [0, 6, 6, 6, 6, 6, 6, 6, 6, 6]
  }
}
```

#### SpellsKnown

Do no use SpellsKnown while AllSpellsKnown is set to true!

A the SpellsKnown can be referenced from another spellbook or definied individually.
If an existing spellbook should be used, use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the spellbook identifiers.

```
"SpellsKnown": "ref:SORCERER_SPELLBOOK",
```

SpellsKnown enables you character to learn spells for different spell level depending on the class level.
Each line in the Table shows how many spells of a certain spell level the character is able to know at a certain class level.
Identifiers are mandatory and reference to the character levels.
The entry for level 0 is mandatory.
The first entry in each line is for spell level 0, the second for spell level one and so on.
SpellsKnown has a Guid and a Name like all other definitions.

```
"SpellsKnown": {
  "Name": "CharlatanSpellsKnown",
  "Guid": "69b34210916a46fc8dd031950aa5d9b7",
  "Table": {
    "0": [0, 0],
    "1": [0, 6],
    "2": [0, 6],
    ...
    "15": [0, 8, 7, 6, 6, 4, 4, 2, 2],
    "16": [0, 8, 7, 6, 6, 4, 4, 2, 2],
    "17": [0, 8, 7, 7, 6, 6, 4, 4, 2, 2],
    "18": [0, 8, 7, 7, 6, 6, 4, 4, 4, 2],
    "19": [0, 8, 8, 7, 7, 6, 6, 4, 4, 4],
    "20": [0, 8, 8, 7, 7, 6, 6, 4, 4, 4]
  }
}
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
| Add->Features            | false    | [ "ref:WEAPON_PROFICIENCY_ESTOC" ] | Default: nothing; The list of features to add with the proficiencies. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| Add->WeaponProficiencies | false    | [ "Longbow", "Starknife" ]         | Default: nothing; The list of weapon skills to add with the proficiencies. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the weapon identifiers |
| Add->ArmorProficiencies  | false    | [ "Light", "Buckler" ]             | Default: nothing; The list of armor skills to add with the proficiencies. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the armor identifiers |
| From                     | false    | "ref:BARD_PROFICIENCIES"           | Proficiencies feature to derive these proficiencies. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |

### Character Class Identifiers

Possible values: ALCHEMIST, BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, INQUISITOR, KINETICIST, MAGUS, MONK, PALADIN, RANGER, ROGUE, SLAYER, SORCERER, WIZARD    

### SpellBook Identifiers

Possible values:
```
ALCHEMIST_SPELLBOOK
BARD_SPELLBOOK
CLERIC_SPELLBOOK
CLERIC_CRUSADER_SPELLBOOK
DRUID_SPELLBOOK
DRUID_FEYSPEAKER_SPELLBOOK
INQUISITOR_SPELLBOOK
MAGUS_SPELLBOOK
MAGUS_SWORD_SAINT_SPELLBOOK
MAGUS_MAGUS_ELDRITCH_SCION_SPELLBOOK
PALADIN_SPELLBOOK
RANGER_SPELLBOOK
ROGUE_ELDRITCH_SCOUNDREL_SPELLBOOK
SORCERER_SPELLBOOK
SORCERER_EMPYREAL_SORCERER_SPELLBOOK
SORCERER_SAGE_SORCERER_SPELLBOOK
WIZARD_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_ABJURATION_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_CONJURATION_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_ENCHANTMENT_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_EVOCATIION_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_ILLUSION_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_NECROMANCY_SPELLBOOK
WIZARD_THASSILONIAN_SPECIALIST_TRANSMUTATION_SPELLBOOK
```

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

## JSON Structure for Feature

### Feature Selection JSON Structure

| Key          | Required | Example                                   | Description |
|--------------|----------|-------------------------------------------|-------------|
| Guid         | true     | "b4c9164ec94a47589eeb2a6688b24320"        | A 32 feature selection GUID |
| Name         | true     | "OracleCurseSelection"                    | The name of the feature selection class, used internally |
| DisplayName  | true     | "Curse"                                   | The name of the feature selection that is shown to the users |
| Description  | false    | "Each oracle is cursed - OMG!"            | The description of the feature selection, if not defined DisplayName is used |
| Icon         | false    | "ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY" | Default: nothing; Icon representation of the feature selection, taken from another feature. Use "ref:<IDENTIFIER>" where <IDENTIFIER> is one of the feature identifiers |
| FeatureGroup | false    | "UpdateLevelUpDeterminatorText"           | Default: None; Quite an hack, don't use it -- please! |
