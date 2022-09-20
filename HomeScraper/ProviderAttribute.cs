using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeScraper {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	class ProviderAttribute : Attribute {
		public bool Enabled { get; private set; }

		public ProviderAttribute() {
			Enabled = true;
		}

		public ProviderAttribute(bool enabled) {
			this.Enabled = enabled;
		}
	}
}
