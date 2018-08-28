using System.Collections.Generic;

namespace FluentQueryBuilder.Application.Features
{
    public static class FeatureToggle
    {
        public const string DO_NOT_TRACK = "DoNotTrack";

        private static readonly Dictionary<string, bool> _features = new Dictionary<string, bool>()
        {
            { DO_NOT_TRACK, false }
        };

        public static bool IsValid(string conditionName)
        {
            if (!_features.ContainsKey(conditionName))
                return true;

            return _features[conditionName];
        }
    }
}
