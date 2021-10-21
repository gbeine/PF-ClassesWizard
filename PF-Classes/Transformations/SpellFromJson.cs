using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SpellFromJson : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;
        private static readonly SpellFactory _spellFactory = new SpellFactory();
        private static readonly LocalizationFactory _localizationFactory = new LocalizationFactory();

        public static BlueprintAbility GetSpell(Spell spellData)
        {
            _logger.Log($"Creating spell from JSON data {spellData.Name}");

            BlueprintAbility spell = !string.Empty.Equals(spellData.From)
                ? _spellFactory.CreateSpellFrom(spellData.Name, spellData.Guid, _identifierLookup.lookupSpell(spellData.From))
                : _spellFactory.CreateSpell(spellData.Name, spellData.Guid);

            if (!string.Empty.Equals(spellData.DisplayName))
                spell.SetName(spellData.DisplayName);

            if (!string.Empty.Equals(spellData.Description))
                spell.SetDescription(spellData.Description);

            if (!string.Empty.Equals(spellData.Icon))
                spell.SetIcon(SpriteLookup.lookupFor(spellData.Icon));

            spell.SpellResistance = spellData.SpellResistance.HasValue && spellData.SpellResistance.Value;
            spell.CanTargetEnemies = spellData.CanTargetEnemies.HasValue && spellData.CanTargetEnemies.Value;
            spell.CanTargetFriends = spellData.CanTargetFriends.HasValue && spellData.CanTargetFriends.Value;
            spell.CanTargetSelf = spellData.CanTargetSelf.HasValue && spellData.CanTargetSelf.Value;
            spell.CanTargetPoint = spellData.CanTargetPoint.HasValue && spellData.CanTargetPoint.Value;

            if (!string.Empty.Equals(spellData.Type))
                spell.Type = EnumParser.parseAbilityType(spellData.Type);

            if (!string.Empty.Equals(spellData.Range))
                spell.Range = EnumParser.parseAbilityRange(spellData.Range);

            if (!string.Empty.Equals(spellData.ActionType))
                spell.ActionType = EnumParser.parseCommandType(spellData.ActionType);

            if (!string.Empty.Equals(spellData.EffectOnEnemy))
                spell.EffectOnEnemy = EnumParser.parseAbilityEffectOnUnit(spellData.EffectOnEnemy);

            if (!string.Empty.Equals(spellData.EffectOnAlly))
                spell.EffectOnAlly = EnumParser.parseAbilityEffectOnUnit(spellData.EffectOnAlly);

            if (!string.Empty.Equals(spellData.Animation))
                spell.Animation = EnumParser.parseCastAnimation(spellData.Animation);

            if (!string.Empty.Equals(spellData.AnimationStyle))
                spell.AnimationStyle = EnumParser.parseCastAnimationStyle(spellData.AnimationStyle);

            if (!string.Empty.Equals(spellData.Duration))
            {
                if (_identifierLookup.existsSpell(spellData.Duration))
                    spell.LocalizedDuration = _spellbookRepository
                        .GetSpell(_identifierLookup.lookupSpell(spellData.Duration)).LocalizedDuration;
                else
                    spell.LocalizedSavingThrow =
                        _localizationFactory.CreateString($"{spell.name}.Duration", spellData.Duration);
            }

            if (!string.Empty.Equals(spellData.SavingThrow))
            {
                if (_identifierLookup.existsSpell(spellData.SavingThrow))
                    spell.LocalizedSavingThrow = _spellbookRepository
                        .GetSpell(_identifierLookup.lookupSpell(spellData.SavingThrow)).LocalizedSavingThrow;
                else
                    spell.LocalizedSavingThrow =
                        _localizationFactory.CreateString($"{spell.name}.SavingThrow", spellData.SavingThrow);
            }

            if (spellData.AvailableMetamagic.Count > 0)
            {
                Metamagic metamagic = default(Metamagic);
                spellData.AvailableMetamagic.ForEach(a => metamagic |= EnumParser.parseMetamagic(a));
                spell.AvailableMetamagic = metamagic;
            }

            _logger.Log("Adding area effects");
            // TODO: area effects

            _logger.Log("Removing components");
            if (spellData.RemoveComponents.Count > 0)
            {
                foreach (var component in spellData.RemoveComponents)
                {
                    RemoveComponentFromJson.Remove(spell, component);
                }
            }

            _logger.Log("Replacing components");
            foreach (var component in spellData.ReplaceComponents)
            {
                _logger.Debug($"Replacing component {component.Type}");
                ReplaceComponentFromJson.ReplaceComponent(spell, component);
                _logger.Debug($"DONE: Replacing component {component.Type}");
            }

            _logger.Log("Adding components");
            foreach (var component in spellData.Components)
            {
                _logger.Debug($"Adding component {component.Type}");
                ComponentFromJson.AddComponent(spell, component);
                _logger.Debug($"DONE: Adding component {component.Type}");
            }

            _logger.Log("Adding to spell lists");
            foreach (var entry in spellData.SpellLists)
            {
                BlueprintSpellList spellList = _spellbookRepository.GetSpellList(_identifierLookup.lookupSpellList(entry.Key));
                spellList.SpellsByLevel[entry.Value].Spells.Add(spell);
                SpellListComponent spellListComponent = _componentFactory.CreateComponent<SpellListComponent>(c =>
                    {
                        c.SpellLevel = entry.Value;
                        c.SpellList = spellList;
                    });
                spell.AddComponent(spellListComponent);
            }

            // TODO: Replace Progression
            // TODO: add spell
            // TODO: add scroll

            _logger.Log($"DONE: Creating spell from JSON data {spellData.Name}");
            _identifierRegistry.Register(spell);
            return spell;
        }
    }
}
