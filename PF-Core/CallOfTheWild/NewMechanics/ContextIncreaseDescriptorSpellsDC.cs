using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics;

namespace PF_Core.CallOfTheWild.NewMechanics
{
    [ComponentName("Increase context spells DC by descriptor")]
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class ContextIncreaseDescriptorSpellsDC : RuleInitiatorLogicComponent<RuleCalculateAbilityParams>
    {
        public ContextValue Value;
        public SpellDescriptorWrapper Descriptor;
        public BlueprintSpellbook spellbook = null;
        public BlueprintCharacterClass specific_class = null;
        public bool only_spells = true;

        private MechanicsContext Context
        {
            get
            {
                MechanicsContext context = (this.Fact as Buff)?.Context;
                if (context != null)
                    return context;
                return (this.Fact as Feature)?.Context;
            }
        }

        public override void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            if (evt.Initiator == null)
            {
                return;
            }

            if (checkSpellbook(spellbook, specific_class, evt.Spellbook, evt.Initiator.Descriptor))
            {
                return;
            }

            if (evt.Spellbook?.Blueprint == null && only_spells)
            {
                return;
            }
            if (this.Descriptor != SpellDescriptor.None)
            {
                bool? nullable = evt.Blueprint.GetComponent<SpellDescriptorComponent>()?.Descriptor.HasAnyFlag((SpellDescriptor)this.Descriptor);
                if (!nullable.HasValue || !nullable.Value)
                    return;
            }
            evt.AddBonusDC(this.Value.Calculate(this.Context));
        }

        public override void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }

        private bool checkSpellbook(BlueprintSpellbook spellbook_blueprint, BlueprintCharacterClass blueprint_class,
            Spellbook spellbook,  UnitDescriptor unit_descriptor)
        {
            if (spellbook_blueprint != null && spellbook_blueprint != spellbook?.Blueprint)
            {
                return false;
            }

            var class_spellbook = blueprint_class == null ? null : unit_descriptor.GetSpellbook(blueprint_class);

            if (blueprint_class != null && (class_spellbook == null || spellbook != class_spellbook))
            {
                return false;
            }
            return true;
        }
    }
}
