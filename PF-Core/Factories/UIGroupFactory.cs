using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;

namespace PF_Core.Factories
{
    public class UIGroupFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public UIGroup CreateUIGroup(params BlueprintFeatureBase[] features) => 
            CreateUIGroup((IEnumerable<BlueprintFeatureBase>)features);

        public UIGroup CreateUIGroup(IEnumerable<BlueprintFeatureBase> features)
        {
            var uiGroup = new UIGroup();
            uiGroup.Features.AddRange(features);
            return uiGroup;
        }

    }
}
