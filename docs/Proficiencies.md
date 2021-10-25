## Proficiencies JSON Structure

| Key                      | Required  | Example                            | Description |
|------------------------- |-----------|------------------------------------|-------------|
| Guid                     | true      | "6d817419f36c4ba7833466e434a7fbd9" | A 32 proficiencies GUID |
| Name                     | true      | "CharlatanProficiencies"           | The name of the proficiencies, used internally |
| From                     | false     | "ref:BARD_PROFICIENCIES"           | Default: nothing; The reference of proficiencies to use as a template for this one |  
| DisplayName              | false (1) | "Charlatan Proficiencies"          | Default: nothing; The name of the proficiencies shown in UI, if none is set, the spellbook will not be shown |

(1) these attributes are required if the spellbook is not cloned from another one

### Example

```
{
  "Guid": "6d817419f36c4ba7833466e434a7fbd9",
  "Name": "CharlatanProficiencies",
  "DisplayName": "Charlatan Proficiencies",
  "Description": "Charlatans are skilled in the use of all simple weapons, short swords, long swords, dueling swords, rapiers, and short bows. They are proficient with light armour and shields.",
  "From": "ref:BARD_PROFICIENCIES",
}
```
