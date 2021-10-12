using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.Alignments;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class PrerequisitesFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public PrerequisiteAlignment CreatePrerequisiteAlignment(AlignmentMaskType alignment)
        {
            _logger.Debug("Create prerequisite");

            PrerequisiteAlignment prerequisite = _library.Create<PrerequisiteAlignment>();
            prerequisite.Alignment = alignment;

            _logger.Debug("DONE: Create prerequisite");
            return prerequisite;
        }
    }
}
