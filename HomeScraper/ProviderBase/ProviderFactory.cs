using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeScraper.ProviderBase {
    static class ProviderFactory {
        public static IEnumerable<IProvider> GetProviders() {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t =>
                t.IsDefined(typeof(ProviderAttribute))
                && typeof(IProvider).IsAssignableFrom(t)
                && !t.IsAbstract
                && t.GetCustomAttribute<ProviderAttribute>().Enabled
                )
            );
            return types.Select(t => (IProvider)Activator.CreateInstance(t));
        }
    }
}
