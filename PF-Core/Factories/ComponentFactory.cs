using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class ComponentFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly ComponentFactory __instance = new ComponentFactory();

        private ComponentFactory() { }

        public static ComponentFactory INSTANCE
        {
            get { return __instance;  }
        }

        public T CreateComponent<T>(Action<T> init) where T : BlueprintComponent
        {
            _logger.Debug($"Create Component for {typeof(T)}");
            T component = _library.Create<T>(init);

            _logger.Debug($"DONE: Create Component for {typeof(T)}");
            return component;
        }

        public T CreateComponent<T>() where T : BlueprintComponent
        {
            _logger.Debug($"Create Component for {typeof(T)}");
            T component = _library.Create<T>();

            _logger.Debug($"DONE: Create Component for {typeof(T)}");
            return component;
        }
    }
}
