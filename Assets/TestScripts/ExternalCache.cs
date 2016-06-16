using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestScripts {
    public static class ExternalCache {
        static Dictionary<GameObject, Transform> trans = new Dictionary<GameObject, Transform>();

        public static Transform GetCachedTransfom(this GameObject owner) {
            if (!trans.ContainsKey(owner)) {
                trans.Add(owner, owner.GetComponent<Transform>());
            }
            return trans[owner];
        }

        static Dictionary<GameObject, TestComponent> test = new Dictionary<GameObject, TestComponent>();

        public static TestComponent GetCachedTestComponent(this GameObject owner) {
            if (!test.ContainsKey(owner)) {
                test.Add(owner, owner.GetComponent<TestComponent>());
            }
            return test[owner];
        }

        static Dictionary<GameObject, Dictionary<Type, Component>> cache = new Dictionary<GameObject, Dictionary<Type, Component>>();

        public static T GetCachedComponent<T>(this GameObject owner) where T : Component {
            var type = typeof(T);

            if (!cache.ContainsKey(owner)) {
                cache.Add(owner, new Dictionary<Type, Component>());
                cache[owner].Add(type, owner.GetComponent<T>());
            }
            if (!cache[owner].ContainsKey(type)) {
                cache[owner].Add(type, owner.GetComponent<T>());
            }

            return cache[owner][type] as T;
        }
    }
}