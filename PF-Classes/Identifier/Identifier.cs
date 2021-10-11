using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PF_Classes.Identifier
{
    public abstract class Identifier
    {
        protected List<FieldInfo> _constants;
        protected IReadOnlyDictionary<String, String> _identifier;

        public Identifier()
        {
            Type type = this.GetType();

            _constants = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(
                    fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(String))
                .ToList();
            
            _identifier = _constants.ToDictionary(fi => fi.Name, fi => (String)fi.GetRawConstantValue());
        }

        public String GetGuidFor(String intentifier)
        {
            return _identifier[intentifier];
        }
        
        public IReadOnlyDictionary<String, String> AllIdentifiers
        {
            get { return _identifier; }
        }
    }
}
