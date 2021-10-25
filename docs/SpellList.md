## Spell List JSON Structure

Spell Lists define which spells a character may learn or use at a certain spell level.
Usually there are up to 9 spell levels.
Which spell level a character class is able to learn is specified in the character class' progression.

| Key                      | Required  | Example                            | Description |
|------------------------- |-----------|------------------------------------|-------------|
| Guid                     | true      | "0fc2fdfb15ec4abd888ef5a7b7e59003" | A 32 spelllist GUID |
| Name                     | true      | "CharlatanSpellList"               | The name of the spell list, used internally |
| From                     | false     | "ref:CLERIC_SPELLBOOK"             | Default: nothing; The reference of a spellbook or a spelllist to use as a template for this one |  
| SpellsByLevel            | false (1) | See below                          | Default: nothing; The spells for each spell level, as shown in the example below |

(1) these attributes are required if the spellbook is not cloned from another one

### Example

```
{
  "Guid": "8b4fc86d687646648c551a740718118c",
  "Name": "CharlatanSpellList",
  "SpellsByLevel": {
    "0": [
      "ref:ABJURATION_RESISTANCE",
      "ref:CONJURATION_ACID_SPLASH",
      ...
    ],
    "1": [
      "ref:ABJURATION_MAGE_SHIELD",
      "ref:ABJURATION_REMOVE_FEAR",
      ...
    ],
    ...
    "9": [
      "ref:ABJURATION_MIND_BLANK_COMMUNAL",
      "ref:CONJURATION_SUMMON_MONSTER_IX_BASE",
      ...
    ]
  }
}
```

Use this spell list with:

```
"SpellList": "loc:CharlatanSpellList" 
```