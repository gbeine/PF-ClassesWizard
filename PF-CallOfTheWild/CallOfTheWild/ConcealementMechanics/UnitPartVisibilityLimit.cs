using System;
using Kingmaker.Blueprints;
using Kingmaker.Utility;

namespace PF_CallOfTheWild.CallOfTheWild.ConcealementMechanics
{
    public class UnitPartVisibilityLimit : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }


        public float getMaxDistance()
        {
            float max_distance = 1000;

            foreach (var b in buffs)
            {
                max_distance = Math.Min(max_distance, b.Blueprint.GetComponent<SetVisibilityLimit>().visibility_limit.Meters);
            }

            return max_distance;
        }
    }
}