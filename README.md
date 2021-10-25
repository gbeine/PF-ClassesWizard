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

## JSON files structuring

All the JSON files have been moved to the [PF-Greenprints GitHub Repository](https://github.com/gbeine/PF-Greenprints).

The documentation of the JSON structures for different items can be found here:

* [Character Class](docs/CharacterClass.md)
* [Archetype](docs/Archtetype.md)
* [Feature](docs/Feature.md)
* [Feature Selection](docs/FeatureSelection.md)
* [Proficiencies](docs/Proficiencies.md)
* [Progression](docs/Progression.md)
* [Cantrips and Orisons](docs/Cantrips.md)
* [Spell](docs/Spell.md)
* [Spellbook](docs/Spellbook.md)
* [Spell List](docs/SpellList.md)
* [Spell Table](docs/SpellTable.md)
* [Using Components](docs/Components.md)
* [Greenprints Directory Structure](docs/DirectoryStructure.md)
