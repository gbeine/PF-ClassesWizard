using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PF_Classes.Identifier
{
    public abstract class Identifier
    {
        protected List<FieldInfo> _constants;
        protected IReadOnlyDictionary<string, string> _identifier;

        public Identifier()
        {
            Type type = this.GetType();

            _constants = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(
                    fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .ToList();

            _identifier = _constants.ToDictionary(fi => fi.Name, fi => (string)fi.GetRawConstantValue());
        }

        public bool Contains(string identifier)
        {
            return _identifier.ContainsKey(identifier);
        }

        public string GetGuidFor(string intentifier)
        {
            return _identifier[intentifier];
        }

        public IReadOnlyDictionary<string, string> AllIdentifiers
        {
            get { return _identifier; }
        }
    }
}
