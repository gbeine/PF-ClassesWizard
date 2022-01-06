using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Facts;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Newtonsoft.Json;

namespace PF_CallOfTheWild.CallOfTheWild
{
    public class AdditiveUnitPart : UnitPart
    {
        [JsonProperty]
        protected List<Fact> buffs = new List<Fact>();

        public virtual void addBuff(Fact buff)
        {
            if (!buffs.Contains(buff))
            {
                buffs.Add(buff);
            }
        }

        public virtual void removeBuff(Fact buff)
        {
            buffs.Remove(buff);
            if (buffs.Empty())
            {
                removePart(this);
            }
        }

        public static void removePart( UnitPart part)
        {
            var owner = part.Owner;
            Type part_type = part.GetType();
            var remove_method = owner.GetType().GetMethod(nameof(UnitDescriptor.Remove));
            remove_method.MakeGenericMethod(part_type).Invoke(owner, null);
        }
    }
}
