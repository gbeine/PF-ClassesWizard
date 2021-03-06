using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Newtonsoft.Json;

namespace PF_CallOfTheWild.CallOfTheWild.BuffMechanics
{
    public class UnitPartBuffSuppressSaved : UnitPart
    {
        [JsonProperty]
        private readonly List<SpellDescriptor> m_SpellDescriptors = new List<SpellDescriptor>();
        [JsonProperty]
        private readonly List<BlueprintBuff> m_Buffs = new List<BlueprintBuff>();
        [JsonProperty]
        private readonly List<SpellSchool> m_SpellSchools = new List<SpellSchool>();

        private static IEnumerable<SpellDescriptor> GetValues(
          SpellDescriptor spellDescriptor)
        {
            return EnumUtils.GetValues<SpellDescriptor>().Where<SpellDescriptor>((Func<SpellDescriptor, bool>)(v =>
            {
                if (v != SpellDescriptor.None)
                    return (ulong)(spellDescriptor & v) > 0UL;
                return false;
            }));
        }

        public void Suppress(SpellSchool[] spellSchools)
        {
            foreach (SpellSchool spellSchool in spellSchools)
                this.m_SpellSchools.Add(spellSchool);
        }

        public void Suppress(SpellDescriptor spellDescriptor)
        {
            foreach (SpellDescriptor spellDescriptor1 in UnitPartBuffSuppressSaved.GetValues(spellDescriptor))
                this.m_SpellDescriptors.Add(spellDescriptor1);
            this.Update();
        }

        public void Suppress(BlueprintBuff buff)
        {
            this.m_Buffs.Add(buff);
            this.Update();
        }

        public void Release(SpellSchool[] spellSchools)
        {
            foreach (SpellSchool spellSchool in spellSchools)
                this.m_SpellSchools.Remove(spellSchool);
            this.Update();
            this.TryRemovePart();
        }

        public void Release(SpellDescriptor spellDescriptor)
        {
            foreach (SpellDescriptor spellDescriptor1 in UnitPartBuffSuppressSaved.GetValues(spellDescriptor))
                this.m_SpellDescriptors.Remove(spellDescriptor1);
            this.Update();
            this.TryRemovePart();
        }

        public void Release(BlueprintBuff buff)
        {
            this.m_Buffs.Remove(buff);
            this.Update();
            this.TryRemovePart();
        }

        private void TryRemovePart()
        {
            if (this.m_Buffs.Any<BlueprintBuff>() || this.m_SpellDescriptors.Any<SpellDescriptor>() || this.m_SpellSchools.Any<SpellSchool>())
                return;
            this.Owner.Remove<UnitPartBuffSuppress>();
        }

        public bool IsSuppressed(Buff buff)
        {
            if (!this.m_Buffs.Contains(buff.Blueprint) && !UnitPartBuffSuppressSaved.GetValues(buff.Context.SpellDescriptor).Any<SpellDescriptor>((Func<SpellDescriptor, bool>)(d => this.m_SpellDescriptors.Contains(d))))
                return this.m_SpellSchools.Contains(buff.Context.SpellSchool);
            return true;
        }

        private void Update()
        {
            foreach (Buff buff in this.Owner.Buffs)
            {
                bool flag = this.IsSuppressed(buff);
                if (buff.IsSuppressed != flag)
                {
                    if (flag && buff.Active)
                        buff.Deactivate();
                    buff.IsSuppressed = flag;
                    if (!flag && !buff.Active)
                        buff.Activate();
                }
            }
        }
    }
}
