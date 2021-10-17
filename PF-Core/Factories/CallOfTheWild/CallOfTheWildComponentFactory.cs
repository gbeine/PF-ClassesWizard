using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using PF_Core.CallOfTheWild.BuffMechanics;
using PF_Core.CallOfTheWild.ConcealementMechanics;
using PF_Core.CallOfTheWild.NewMechanics;
using PF_Core.CallOfTheWild.SpellFailureMechanics;
using PF_Core.Facades;

namespace PF_Core.Factories.CallOfTheWild
{
    public class ComponentFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public AddOutgoingConcealment CreateAddOutgoingConcealment(int distanceGreater)
        {
            _logger.Debug($"Create AddOutgoingConcealment");

            AddOutgoingConcealment addOutgoingConcealment = _library.Create<AddOutgoingConcealment>(
                a =>
                {
                    a.CheckDistance = true;
                    a.Descriptor = ConcealmentDescriptor.InitiatorIsBlind;
                    a.DistanceGreater = distanceGreater.Feet();
                    a.Concealment = Concealment.Total;
                });

            _logger.Debug($"DONE: Create AddOutgoingConcealment");
            return addOutgoingConcealment;
        }

        public SetVisibilityLimit CreateSetVisibilityLimit(int visibilityLimit)
        {
            _logger.Debug($"Create SetVisibilityLimit");

            SetVisibilityLimit setVisibilityLimit = _library.Create<SetVisibilityLimit>(
                s => s.visibility_limit = visibilityLimit.Feet()
            );

            _logger.Debug($"DONE: Create SetVisibilityLimit");
            return setVisibilityLimit;
        }

        public Silence CreateSilence()
        {
            _logger.Debug($"Create Silence");
            Silence silence = _library.Create<Silence>();

            _logger.Debug($"DONE: Create Silence");
            return silence;
        }

        public SpellFailureChance CreateSpellFailureChance(int chance, bool ignorePsychic)
        {
            _logger.Debug($"Create SpellFailureChance");
            SpellFailureChance spellFailureChance = _library.Create<SpellFailureChance>();
            spellFailureChance.chance = chance;
            spellFailureChance.ignore_psychic = ignorePsychic;

            _logger.Debug($"DONE: Create SpellFailureChance");
            return spellFailureChance;
        }

        public SuppressBuffsCorrect CreateSuppressBuffsCorrect(SpellDescriptor spellDescriptor, BlueprintBuff[] buffs)
        {
            _logger.Debug($"Create SuppressBuffsCorrect");
            SuppressBuffsCorrect suppressBuffsCorrect = _library.Create<SuppressBuffsCorrect>();
            suppressBuffsCorrect.Descriptor = spellDescriptor;
            suppressBuffsCorrect.Buffs = buffs;

            _logger.Debug($"DONE: Create SuppressBuffsCorrect");
            return suppressBuffsCorrect;
        }

        public WeaponsOnlyAttackBonus CreateWeaponsOnlyAttackBonus(int bonus)
        {
            _logger.Debug($"Create WeaponsOnlyAttackBonus");

            WeaponsOnlyAttackBonus weaponsOnlyAttackBonus = _library.Create<WeaponsOnlyAttackBonus>(
                w => w.Bonus = bonus);

            _logger.Debug($"DONE: Create WeaponsOnlyAttackBonus");
            return weaponsOnlyAttackBonus;
        }
    }
}
