using System;

namespace UnityCache {
	/// <summary>
	/// Used with Cache.CacheAll() to init instance members with GetComponent
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class CachedAttribute : Attribute {
	}
}
