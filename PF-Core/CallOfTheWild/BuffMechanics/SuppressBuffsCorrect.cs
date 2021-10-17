using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.BuffMechanics
{
    public class SuppressBuffsCorrect : OwnedGameLogicComponent<UnitDescriptor>
    {
        public BlueprintBuff[] Buffs = new BlueprintBuff[0];
        public SpellSchool[] Schools = new SpellSchool[0];
        public SpellDescriptorWrapper Descriptor;

        public override void OnFactActivate()
        {
            var partBuffSuppress = this.Owner.Ensure<UnitPartBuffSuppressSaved>();
            if (this.Descriptor != SpellDescriptor.None)
                partBuffSuppress.Suppress((SpellDescriptor)this.Descriptor);
            if (!((IList<SpellSchool>)this.Schools).Empty<SpellSchool>())
                partBuffSuppress.Suppress(this.Schools);
            foreach (BlueprintBuff buff in this.Buffs)
                partBuffSuppress.Suppress(buff);
        }

        public override void OnFactDeactivate()
        {
            var partBuffSuppress = this.Owner.Get<UnitPartBuffSuppressSaved>();
            if (!(bool)((UnitPart)partBuffSuppress))
            {
                UberDebug.LogError((object)"UnitPartSuppressBuff is missing", (object[])Array.Empty<object>());
            }
            else
            {
                if (this.Descriptor != SpellDescriptor.None)
                    partBuffSuppress.Release((SpellDescriptor)this.Descriptor);
                if (!((IList<SpellSchool>)this.Schools).Empty<SpellSchool>())
                    partBuffSuppress.Release(this.Schools);
                foreach (BlueprintBuff buff in this.Buffs)
                    partBuffSuppress.Release(buff);
            }
        }
    }
}
