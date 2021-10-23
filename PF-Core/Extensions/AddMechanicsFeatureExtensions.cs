using Kingmaker.UnitLogic.FactLogic;
using PF_Core.Facades;

namespace PF_Core.Extensions
{
    public static class AddMechanicsFeatureExtensions
    {
        private static readonly Harmony.FastSetter addMechanicsFeature_set_Feature = Harmony.CreateFieldSetter<AddMechanicsFeature>("m_Feature");

        public static void SetFeature(this AddMechanicsFeature addMechanicsFeature, AddMechanicsFeature.MechanicsFeatureType feature) =>
            addMechanicsFeature_set_Feature(addMechanicsFeature, feature);
    }
}
