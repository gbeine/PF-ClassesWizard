## Spell Table JSON Structure

Spell Table define either how many spells a character class is able to use per day or how many spells can be learned at a new level. 
The number of spell levels should be equal to the spell list's.

| Key                      | Required  | Example                            | Description |
|------------------------- |-----------|------------------------------------|-------------|
| Guid                     | true      | "d9adb154906244f39fd7439a5f4d6ac2" | A 32 spell table GUID |
| Name                     | true      | "CharlatanSpellsPerDay"            | The name of the spell table, used internally |
| From                     | false     | "ref:CLERIC_SPELLBOOK"             | Default: nothing; The reference of a spellbook or a spell table to use as a template for this one |  
| Table                    | false (1) | See below                          | Default: nothing; The spells a character is able to use per day or to learn on a new level |

(1) these attributes are required if the spellbook is not cloned from another one

### Example

```
{
  "Name": "CharlatanSpellsPerDay",
  "Guid": "d9adb154906244f39fd7439a5f4d6ac2",
  "Table": {
    "0": [],
    "1": [0, 3],
    "2": [0, 4],
    "3": [0, 5, 3],
    "4": [0, 6, 4],
    "5": [0, 6, 5, 3],
    "6": [0, 6, 6, 4],
    ...
    "15": [0, 6, 6, 6, 6, 6, 6, 5, 3],
    "16": [0, 6, 6, 6, 6, 6, 6, 6, 4],
    "17": [0, 6, 6, 6, 6, 6, 6, 6, 5, 3],
    "18": [0, 6, 6, 6, 6, 6, 6, 6, 6, 4],
    "19": [0, 6, 6, 6, 6, 6, 6, 6, 6, 5],
    "20": [0, 6, 6, 6, 6, 6, 6, 6, 6, 6]
  }
}
```

The table contains a row for each **character level** (so there are usually 20).
Row 0 is always empty.
Each row contains the number of spells for a **spell level** that the character is able to use per day.
The first entry is the spell level 0 used for cantrips, the second entry is spell level 1 and so on.

So with this table, a charlatan is able to use:

- 3 level 1 spells at character level 1
- 4 level 1 spells at character level 2
- 5 level 1 spells and 3 level 2 spells at charachter level 3
- 6 spells of every level from 1 to 9 at charachter level 20


Use this spell list with:

```
"SpellsPerDay": "loc:CharlatanSpellsPerDay" 
```