using Kingmaker.Blueprints.Classes;

namespace PF_Core.Factories
{
    public class UIGroupFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public UIGroup CreateUIGroup(params BlueprintFeatureBase[] features)
        {
            _logger.Debug($"Create UI group");

            UIGroup uiGroup = new UIGroup();
            uiGroup.Features.AddRange(features);

            return uiGroup;
        }
    }
}
