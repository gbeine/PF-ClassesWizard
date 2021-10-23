using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SpellFromJson : JsonTransformation
    {
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

            ComponentFromJson.ProcessComponents(spell, spellData);

            _logger.Log("Adding to spell lists");
            foreach (var entry in spellData.SpellLists)
            {
                BlueprintSpellList spellList = _spellbookRepository.GetSpellList(_identifierLookup.lookupSpellList(entry.Key));
                spellList.SpellsByLevel[entry.Value].Spells.Add(spell);
                spell.AddComponent(SpellListComponentDelegate.CreateComponent(spellList, entry.Value));
            }

            // TODO: Replace Progression
            AddSpell(spell);
            AddScroll(spell);

            _logger.Log($"DONE: Creating spell from JSON data {spellData.Name}");
            _identifierRegistry.Register(spell);
            return spell;
        }

        private static void AddSpell(BlueprintAbility spell)
        {
            if (spell.Type == AbilityType.Spell && spell.AvailableMetamagic == default)
            {
                _logger.Error($"Error: spell {spell.name} is missing metamagic (should have heighten, quicken at least)");
            }

            BlueprintFeatureSelection spellSelection = _featuresRepository.GetFeatureSelection(
                _identifierLookup.lookupFeature("ref:SPELL_SPECIALIZATION_SELECTION"));

            BlueprintFeature[] allSpells = spellSelection.AllFeatures;

            // TODO: implement additional specializations
            // Main.library.Get<BlueprintParametrizedFeature>("f327a765a4353d04f872482ef3e48c35"), //spell specialization first
            // Main.library.Get<BlueprintParametrizedFeature>("4a2e8388c2f0dd3478811d9c947bebfb"), //arcane bloodline
            // Main.library.Get<BlueprintParametrizedFeature>("c66e61dea38f3d8479a54eabec20ac99"), //arcane bloodline magus
            // Main.library.Get<BlueprintParametrizedFeature>("ea0ce0aeef8c9e04eadc1ed766455178"), //feyspeaker bonus spells
            // // mt inquisitor spells
            // Main.library.Get<BlueprintParametrizedFeature>("bcd757ac2aeef3c49b77e5af4e510956"),
            // Main.library.Get<BlueprintParametrizedFeature>("4869109802e135e45af20741f9056fd5"),
            // Main.library.Get<BlueprintParametrizedFeature>("e3a9ed781f9093341ac1073f59018e3f"),
            // Main.library.Get<BlueprintParametrizedFeature>("7668fd94a4f943e4f85ee025a0140434"),
            // Main.library.Get<BlueprintParametrizedFeature>("d3d8b837733879848b549189f02f535c"),
            // Main.library.Get<BlueprintParametrizedFeature>("0495474b37304054eaf016016d0002b4")

            foreach (BlueprintParametrizedFeature ss in allSpells)
            {
                ss.BlueprintParameterVariants = ss.BlueprintParameterVariants.AddToArray(spell);
            }
        }

        private static void AddScroll(BlueprintAbility spell)
        {
            // TODO: implement
        }
    }
}
