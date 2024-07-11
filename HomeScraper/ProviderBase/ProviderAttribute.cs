using System;

namespace HomeScraper.ProviderBase {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    class ProviderAttribute : Attribute {
        public bool Enabled { get; private set; }

        public ProviderAttribute() {
            Enabled = true;
        }

        public ProviderAttribute(bool enabled) {
            Enabled = enabled;
        }
    }
}
