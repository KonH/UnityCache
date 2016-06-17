using System;

namespace UnityCache {
    /// <summary>
	/// Used it for caching in editor (UnityCache/PreCache)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PreCachedAttribute : Attribute {
    }
}
