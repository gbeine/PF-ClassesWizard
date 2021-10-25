## Spellbook JSON Structure

| Key                      | Required  | Example                            | Description |
|------------------------- |-----------|------------------------------------|-------------|
| Guid                     | true      | "0fc2fdfb15ec4abd888ef5a7b7e59003" | A 32 spellbook GUID |
| Name                     | true      | "CharlatanSpellbook"               | The name of the spellbook, used internally |
| From                     | false     | "ref:CLERIC_SPELLBOOK"             | Default: nothing; The reference of a spellbook to use as a template for this one |  
| DisplayName              | false (1) | "Charlatan"                        | Default: nothing; The name of the spellbook shown in UI, if none is set, the spellbook will be shown as <null> |
| CharacterClass           | false     | "loc:Charlatan"                    | Default: nothing; The chracter class to bind this spellbook to |
| IsArcane                 | false     | true                               | Default: false; Determine something |
| IsSpontaneous            | false     | true                               | Default: false; Determine if the character can cast spontaneoulsy |
| CanCopyScrolls           | false     | true                               | Default: false; Determine if the character can copy scrolls |
| AllSpellsKnown           | false     | true                               | Default: false; Determine if the character has access to all spells in the spellbook. Do not use with a SpellsKnown table |
| SpellsPerLevel           | false     | 5                                  | Default: 0; #TODO |
| CasterLevelModifier      | false     | 5                                  | Default: 0; #TODO |
| CastingAttribute         | false (1) | "Intelligence"                     | The attribute used by the character to cast spells from this book. The values has to be from the attributes identifiers listed below, usually Intelligence or Charisma |
| Cantrips                 | false (1) | "Orisons"                          | Default: "Cantrips"; The type of cantrips in this book, one of "Orisons" or "Cantrips" |
| SpellList                | false (1) | "ref:CLERIC_SPELLBOOK"             | Default: nothing; The reference to an existing spellbook (to copy the spell list) or to a spell list definition |
| SpellsPerDay             | false (1) | "ref:SORCERER_SPELLBOOK"           | Default: nothing; The reference to an existing spellbook (to copy the spells per day table) or to a spell table definition |
| SpellsKnown              | false     | "loc:CharlatanSpellsKnown"         | Default: nothing; Use only if AllSpellsKnown is false; The reference to an existing spellbook (to copy the spells known table) or to a spell table definition |

(1) these attributes are required if the spellbook is not cloned from another one

### Example

```
{
  "Guid": "0fc2fdfb15ec4abd888ef5a7b7e59003",
  "Name": "CharlatanSpellbook",
  "DisplayName: "Charlatan"
  "CharacterClass": "loc:Charlatan",
  "IsArcane": true,
  "IsSpontaneous": true,
  "CastingAttribute": "Intelligence",
  "Cantrips": "Cantrips",
  "SpellList": "loc:CharlatanSpellList",
  "SpellsPerDay": "loc:CharlatanSpellsPerDay",
  "SpellsKnown": "loc:CharlatanSpellsKnown"
}
```
