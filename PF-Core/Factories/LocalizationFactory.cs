using System;
using System.Collections.Generic;
using Kingmaker.Localization;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class LocalizationFactory
    {
        private static Harmony.FastSetter localizedString_m_Key = Harmony.CreateFieldSetter<LocalizedString>("m_Key");

        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<String, LocalizedString> _textToLocalizedString = new Dictionary<string, LocalizedString>();

        public LocalizedString CreateString(string key, string value)
        {
            Dictionary<String, String> strings = LocalizationManager.CurrentPack.Strings;

            String oldValue;
            if (strings.TryGetValue(key, out oldValue) && value != oldValue)
            {
                _logger.Warning($"Info: duplicate localized string `{key}`, different text.");
            }
            strings[key] = value;

            LocalizedString localized = new LocalizedString();
            localizedString_m_Key(localized, key);
            _textToLocalizedString[value] = localized;

            return localized;
        }
    }
}
