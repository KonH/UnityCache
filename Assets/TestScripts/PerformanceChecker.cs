using UnityEngine;
using UnityEngine.Profiling;
using System;

namespace TestScripts {
    public class PerformanceChecker : MonoBehaviour {
        public bool TestTransform = false;
        public bool TestComponent = false;
        public int Tries = 1000;
        public InnerCache Cache = null;
        public AttributeCache AttrCache = null;
        public TestInheritScript TestInh = null;

        float _counter = 0;
        bool skip = true;

        // Transform
        Transform _transVar = null;

        Transform _transProp = null;
        Transform TransProp {
            get {
                if (!_transProp) {
                    _transProp = transform;
                }
                return _transProp;
            }
        }

        Transform _transPropConv = null;
        Transform TransPropConv {
            get {
                if ((object)_transPropConv == null) {
                    _transPropConv = transform;
                }
                return _transPropConv;
            }
        }

        bool _transPropCached = false;
        Transform _transPropChecked = null;
        Transform TransPropChecked {
            get {
                if (!_transPropCached) {
                    _transPropChecked = transform;
                    _transPropCached = true;
                }
                return _transPropChecked;
            }
        }

        // TestComponent
        TestComponent _testVar = null;

        TestComponent _testProp = null;
        TestComponent TestProp {
            get {
                if (!_testProp) {
                    _testProp = GetComponent<TestComponent>();
                }
                return _testProp;
            }
        }

        TestComponent _testPropConv = null;
        TestComponent TestPropConv {
            get {
                if ((object)_testPropConv == null) {
                    _testPropConv = GetComponent<TestComponent>();
                }
                return _testPropConv;
            }
        }

        bool _testPropCached = false;
        TestComponent _testPropChecked = null;
        TestComponent TestPropChecked {
            get {
                if (!_testPropCached) {
                    _testPropChecked = GetComponent<TestComponent>();
                    _testPropCached = true;
                }
                return _testPropChecked;
            }
        }

        void Awake() {
            _transVar = transform;
            _testVar = GetComponent<TestComponent>();
        }

        void Update() {
            if (skip) {
                skip = false;
                return;
            }

            GC.Collect();

            _counter = 0;

            if (TestTransform) {
                Direct_Trans();

                LocalCacheVariable_Trans();
                LocalCacheProperty_Trans();
                LocalCachePropertyConv_Trans();
                LocalCachePropertyChecked_Trans();

                ExternalCacheSimple_Trans();
                ExternalCacheTemplate_Trans();

                InnerCacheTemplate_Trans();

                //InnerCacheAttribute_Trans();
                //InnerCacheAttributeInherit_Trans();
            }

            if (TestComponent) {
                DirectT_Test();
                DirectT_Null_Test();
                DirectsT_Test();
                DirectsT_Null_Test();
                DirectS_Test();
                DirectS_Null_Test();
                DirectsS_Test();
                DirectsS_Null_Test();

                LocalCacheVariable_Test();
                LocalCacheProperty_Test();
                LocalCachePropertyConv_Test();
                LocalCachePropertyChecked_Test();

                ExternalCacheSimple_Test();
                ExternalCacheTemplate_Test();

                InnerCacheTemplate_Test();

                InnerCacheAttribute_Test();
                InnerCacheAttributeInherit_Test();
            }
        }

        // Transform
        void Direct_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += transform.position.x;
            }
        }

        void LocalCacheVariable_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += _transVar.position.x;
            }
        }

        void LocalCacheProperty_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += TransProp.position.x;
            }
        }

        void LocalCachePropertyConv_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += TransPropConv.position.x;
            }
        }

        void LocalCachePropertyChecked_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += TransPropChecked.position.x;
            }
        }

        void ExternalCacheSimple_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += gameObject.GetCachedTransfom().position.x;
            }
        }

        void ExternalCacheTemplate_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += gameObject.GetCachedComponent<Transform>().position.x;
            }
        }

        void InnerCacheTemplate_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += Cache.Get<Transform>().position.x;
            }
        }

        /*void InnerCacheAttribute_Trans() {
            for ( int i = 0; i < Tries; i++ ) {
                _counter += AttrCache.Trans.position.x;
            }
        }

        void InnerCacheAttributeInherit_Trans() {
            for (int i = 0; i < Tries; i++) {
                _counter += TestInh.Trans.position.x;
            }
        }*/

        // Test Component
        void DirectT_Test() {
            Profiler.BeginSample("Get Component<T>");
            for (int i = 0; i < Tries; i++) {
                _counter += GetComponent<TestComponent>().Vector.x;
            }
            Profiler.EndSample();
        }

        void DirectT_Null_Test() {
            Profiler.BeginSample("Get Component<T> Null");
            for (int i = 0; i < Tries; i++) {
                var item = GetComponent<Test1>();
            }
            Profiler.EndSample();
        }

        void DirectsT_Test() {
            Profiler.BeginSample("Get Components<T>");
            for (int i = 0; i < Tries; i++) {
                _counter += GetComponents<TestComponent>()[0].Vector.x;
            }
            Profiler.EndSample();
        }

        void DirectsT_Null_Test() {
            Profiler.BeginSample("Get Components<T> Null");
            for (int i = 0; i < Tries; i++) {
                var items = GetComponents<Test1>();
            }
            Profiler.EndSample();
        }

        void DirectS_Test() {
            Profiler.BeginSample("Get Component By Type");
            for (int i = 0; i < Tries; i++) {
                _counter += (GetComponent(typeof(TestComponent)) as TestComponent).Vector.x;
            }
            Profiler.EndSample();
        }

        void DirectS_Null_Test() {
            Profiler.BeginSample("Get Component By Type Null");
            for (int i = 0; i < Tries; i++) {
                var item = GetComponent(typeof(Test1)) as Test1;
            }
            Profiler.EndSample();
        }

        void DirectsS_Test() {
            Profiler.BeginSample("Get Components By Type");
            for (int i = 0; i < Tries; i++) {
                _counter += (GetComponents(typeof(TestComponent))[0] as TestComponent).Vector.x;
            }
            Profiler.EndSample();
        }

        void DirectsS_Null_Test() {
            Profiler.BeginSample("Get Components By Type Null");
            for (int i = 0; i < Tries; i++) {
                var items = GetComponents(typeof(Test1));
            }
            Profiler.EndSample();
        }

        void LocalCacheVariable_Test() {
            Profiler.BeginSample("Local Variable");
            for (int i = 0; i < Tries; i++) {
                _counter += _testVar.Vector.x;
            }
            Profiler.EndSample();
        }

        void LocalCacheProperty_Test() {
            Profiler.BeginSample("Cached Property");
            for (int i = 0; i < Tries; i++) {
                _counter += TestProp.Vector.x;
            }
            Profiler.EndSample();
        }

        void LocalCachePropertyConv_Test() {
            Profiler.BeginSample("Cached Property Converted");
            for (int i = 0; i < Tries; i++) {
                _counter += TestPropConv.Vector.x;
            }
            Profiler.EndSample();
        }

        void LocalCachePropertyChecked_Test() {
            Profiler.BeginSample("Cached Property Checked");
            for (int i = 0; i < Tries; i++) {
                _counter += TestPropChecked.Vector.x;
            }
            Profiler.EndSample();
        }

        void ExternalCacheSimple_Test() {
            Profiler.BeginSample("Static Extension Cache Concrete");
            for (int i = 0; i < Tries; i++) {
                _counter += gameObject.GetCachedTestComponent().Vector.x;
            }
            Profiler.EndSample();
        }

        void ExternalCacheTemplate_Test() {
            Profiler.BeginSample("Static Extension Cache<T>");
            for (int i = 0; i < Tries; i++) {
                _counter += gameObject.GetCachedComponent<TestComponent>().Vector.x;
            }
            Profiler.EndSample();
        }

        void InnerCacheTemplate_Test() {
            Profiler.BeginSample("CacheComponent Cache<T>");
            for (int i = 0; i < Tries; i++) {
                _counter += Cache.Get<TestComponent>().Vector.x;
            }
            Profiler.EndSample();
        }

        void InnerCacheAttribute_Test() {
            Profiler.BeginSample("Attribute Cache Cocrete");
            for (int i = 0; i < Tries; i++) {
                _counter += AttrCache.Test.Vector.x;
            }
            Profiler.EndSample();
        }

        void InnerCacheAttributeInherit_Test() {
            Profiler.BeginSample("Attribute Cache Inherit");
            for (int i = 0; i < Tries; i++) {
                _counter += TestInh.Test.Vector.x;
            }
            Profiler.EndSample();
        }
    }
}
