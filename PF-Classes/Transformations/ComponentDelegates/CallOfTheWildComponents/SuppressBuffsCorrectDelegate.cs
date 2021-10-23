using System;
using System.Linq;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.JsonTypes;
using PF_Core.CallOfTheWild.BuffMechanics;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class SuppressBuffsCorrectDelegate : AbstractComponentDelegate
    {
        public static SuppressBuffsCorrect CreateComponent(Component componentData)
        {
            SuppressBuffsCorrect c = _componentFactory.CreateComponent<SuppressBuffsCorrect>();

            c.Descriptor = componentData.Exists("Descriptor")
                ? EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"))
                : SpellDescriptor.None;
            c.Buffs = componentData.Exists("Buffs")
                ? componentData.AsArray("Buffs")
                    .Select(b => getBuff(b))
                    .ToArray()
                : Array.Empty<BlueprintBuff>();

            return c;
        }
    }
}
