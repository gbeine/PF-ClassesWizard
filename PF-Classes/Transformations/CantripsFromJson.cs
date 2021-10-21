using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Newtonsoft.Json.Linq;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class CantripsFromJson : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        public static BlueprintFeature GetCantrips(Cantrips cantripsData)
        {
            _logger.Log($"Creating cantrips from JSON data {cantripsData.Guid}");

            BlueprintFeature cantrips = FeatureFromJson.GetFeature(cantripsData);
            BlueprintCharacterClass characterClass =
                _characterClassesRepository.GetCharacterClass(
                    _identifierLookup.lookupCharacterClass(cantripsData.Class));

            BlueprintSpellbook spellbook = !string.Empty.Equals(cantripsData.Spellbook)
                ? _spellbookRepository.GetSpellbook(_identifierLookup.lookupSpellbook(cantripsData.Spellbook))
                : characterClass.Spellbook;

            cantrips.Groups = new[] { FeatureGroup.None };

            _logger.Log($"Adding components for cantrips {cantripsData.Guid}");

            cantrips.RemoveComponents<AddFacts>();
            cantrips.RemoveComponents<LearnSpells>();
            cantrips.RemoveComponents<BindAbilitiesToClass>();

            cantrips.AddComponent(_componentFactory
                .CreateComponent<AddFacts>(c =>
                {
                    c.name = $"AddFacts${spellbook.name}";
                    c.Facts = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                }));

            cantrips.AddComponent(_componentFactory
                .CreateComponent<LearnSpells>(c =>
                {
                    c.CharacterClass = characterClass;
                    c.Spells = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                }));

            cantrips.AddComponent(_componentFactory
                .CreateComponent<BindAbilitiesToClass>(c =>
                {
                    c.CharacterClass = characterClass;
                    c.LevelStep = 1;
                    c.Cantrip = true;
                    c.Abilites = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                    c.Stat = spellbook.CastingAttribute;
                }));

            _logger.Log($"DONE: Adding components for cantrips {cantripsData.Guid}");

            _logger.Log("DONE: Creating cantrips");
            return cantrips;
        }
    }
}
