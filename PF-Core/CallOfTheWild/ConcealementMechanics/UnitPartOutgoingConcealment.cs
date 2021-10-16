using System.Collections.Generic;
using JetBrains.Annotations;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using Newtonsoft.Json;

namespace PF_Core.CallOfTheWild.ConcealementMechanics
{
    public class UnitPartOutgoingConcealment : UnitPart
    {
        [CanBeNull] [JsonProperty] private List<UnitPartConcealment.ConcealmentEntry> m_Concealments;

        public void AddConcealment(UnitPartConcealment.ConcealmentEntry entry)
        {
            if (this.m_Concealments == null)
                this.m_Concealments = new List<UnitPartConcealment.ConcealmentEntry>();
            this.m_Concealments.Add(entry);
        }


        public void RemoveConcealement(UnitPartConcealment.ConcealmentEntry entry)
        {
            if (this.m_Concealments == null)
                return;
            foreach (UnitPartConcealment.ConcealmentEntry concealment in this.m_Concealments)
            {
                if (concealment.Descriptor == entry.Descriptor && concealment.Concealment == entry.Concealment)
                {
                    AttackTypeAttackBonus.WeaponRangeType? rangeType1 = concealment.RangeType;
                    int valueOrDefault1 = (int) rangeType1.GetValueOrDefault();
                    AttackTypeAttackBonus.WeaponRangeType? rangeType2 = entry.RangeType;
                    int valueOrDefault2 = (int) rangeType2.GetValueOrDefault();
                    if ((valueOrDefault1 != valueOrDefault2
                            ? 0
                            : (rangeType1.HasValue == rangeType2.HasValue ? 1 : 0)) != 0 &&
                        concealment.DistanceGreater == entry.DistanceGreater)
                    {
                        this.m_Concealments.Remove(concealment);
                        if (this.m_Concealments.Count > 0)
                            break;
                        this.m_Concealments = (List<UnitPartConcealment.ConcealmentEntry>) null;
                        break;
                    }
                }
            }
        }
    }
}
