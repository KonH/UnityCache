UnityCache
Version: 0.31

Simple scripts to cache your components using attributes.
You can do it at runtime with some performance issues and in editor without it.

Runtime Example:
Components loaded using [Cached] attribute and you do not want to call GetComponent() on each one.
See Examples/CacheExample.cs

Editor Example:
Components loaded in Editor (before application start) and saved to instance variables.
See Examples/PreCacheExample.cs

License: MIT (see LICENSE.txt beside)
