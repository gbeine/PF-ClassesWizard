using System;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_CallOfTheWild.CallOfTheWild.MetamagicMechanics
{
    [ComponentName("Apply metamagic for resource")]
    [AllowedOn(typeof(Kingmaker.Blueprints.Facts.BlueprintUnitFact))]
    public class ApplyMetamagicToPersonalSpell : RuleInitiatorLogicComponent<RuleCastSpell>, IInitiatorRulebookSubscriber
    {
        public Metamagic metamagic;
        public int caster_level_increase;
        public int dc_increase;

        public override void OnEventAboutToTrigger(RuleCastSpell evt)
        {
        }

        public override void OnEventDidTrigger(RuleCastSpell evt)
        {
            if (evt.Spell.Blueprint == null
                || !isPersonalSpell(evt.Spell)
                || evt.Spell.Blueprint.Type != AbilityType.Spell)
            {
                return;
            }

            if (evt.Context.MainTarget != evt.Context.MaybeCaster)
            {
                return;
            }

            evt.Context.Params.CasterLevel += caster_level_increase;
            evt.Context.Params.DC += dc_increase;

            if ((evt.Spell.Blueprint.AvailableMetamagic & metamagic) > 0)
            {
                evt.Context.Params.Metamagic = evt.Context.Params.Metamagic | metamagic;
            }

            evt.Context.RecalculateRanks();
            //in case there is no explicit context rank config, it will not be recalcualted by above function, so we should do it manually
            var found_values = new bool[Enum.GetValues(typeof(AbilityRankType)).Cast<int>().Max() + 1];
            evt.Spell.Blueprint.GetComponents<ContextRankConfig>().ForEach(c => found_values[(int) c.Type] = true);
            var ranks = evt.Context.GetRanks();
            for (int i = 0; i < found_values.Length; i++)
            {
                if (!found_values[i])
                {
                    ranks[i] += caster_level_increase;
                }
            }

            evt.Context.RecalculateSharedValues();
        }

        private static bool isPersonalSpell(AbilityData spell)
        {
            if (spell?.Spellbook == null)
            {
                return false;
            }

            return spell.Blueprint.CanTargetSelf && !spell.Blueprint.HasAreaEffect() && !spell.Blueprint.CanTargetPoint;
        }
    }
}
