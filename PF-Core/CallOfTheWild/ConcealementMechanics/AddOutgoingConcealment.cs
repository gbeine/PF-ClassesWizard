using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.ConcealementMechanics
{
    public class AddOutgoingConcealment : OwnedGameLogicComponent<UnitDescriptor>
    {
        public ConcealmentDescriptor Descriptor;
        public Concealment Concealment;
        public bool CheckWeaponRangeType;
        [ShowIf("CheckWeaponRangeType")]
        public AttackTypeAttackBonus.WeaponRangeType RangeType;
        public bool CheckDistance;
        [ShowIf("CheckDistance")]
        public Feet DistanceGreater;
        public bool OnlyForAttacks;

        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartOutgoingConcealment>().AddConcealment(this.CreateConcealmentEntry());
        }

        public override void OnTurnOff()
        {
            Owner.Ensure<UnitPartOutgoingConcealment>().RemoveConcealement(this.CreateConcealmentEntry());
        }

        private UnitPartConcealment.ConcealmentEntry CreateConcealmentEntry()
        {
            UnitPartConcealment.ConcealmentEntry concealmentEntry = new UnitPartConcealment.ConcealmentEntry()
            {
                Concealment = this.Concealment,
                Descriptor = this.Descriptor
            };
            if (this.CheckDistance)
                concealmentEntry.DistanceGreater = this.DistanceGreater;
            if (this.CheckWeaponRangeType)
                concealmentEntry.RangeType = new AttackTypeAttackBonus.WeaponRangeType?(this.RangeType);
            concealmentEntry.OnlyForAttacks = this.OnlyForAttacks;
            return concealmentEntry;
        }
    }
}
