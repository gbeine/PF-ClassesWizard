using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.Extensions;

namespace PF_Classes.Transformations
{
    public class CantripsFromJson : JsonTransformation
    {
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

            ComponentDelegate.Remove("AddFacts", cantrips);
            ComponentDelegate.Remove("LearnSpells", cantrips);
            ComponentDelegate.Remove("BindAbilitiesToClass", cantrips);

            cantrips.AddComponent(
                AddFactsDelegate.CreateComponent(spellbook));
            cantrips.AddComponent(
                LearnSpellsDelegate.CreateComponent(spellbook, characterClass));
            cantrips.AddComponent(
                BindAbilitiesToClassDelegate.CreateComponent(spellbook, characterClass, true));

            _logger.Log("DONE: Creating cantrips");
            return cantrips;
        }
    }
}
