using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilitySpawnFxDelegate : AbstractComponentDelegate
    {
        public static AbilitySpawnFx CreateComponent(Component componentData)
        {
            AbilitySpawnFx c = _componentFactory.CreateComponent<AbilitySpawnFx>();

            if (componentData.Exists("Link"))
                c.PrefabLink = new PrefabLink()
                {
                    AssetId = IdentifierLookup.INSTANCE.lookupBuff(componentData.AsString("Link"))
                };

            c.PositionAnchor = componentData.Exists("PositionAnchor")
                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("PositionAnchor"))
                : AbilitySpawnFxAnchor.None;
            c.OrientationAnchor = componentData.Exists("OrientationAnchor")
                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("OrientationAnchor"))
                : AbilitySpawnFxAnchor.None;
            c.Anchor = componentData.Exists("Anchor")
                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("Anchor"))
                : AbilitySpawnFxAnchor.None;

            return c;
        }
    }
}
