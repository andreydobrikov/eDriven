﻿/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

using eDriven.Core.Managers;
using UnityEngine;

namespace eDriven.Core
{
    /// <summary>
    /// Main eDriven framework class
    /// </summary>
    public sealed class Framework
    {
#if DEBUG
        /// <summary>
        /// Debug mode on
        /// </summary>
        public static bool DebugMode;
#endif

        /// <summary>
        /// The name of the auto-generated framework object
        /// </summary>
        public static string FrameworkObjectName = "$_eDriven_Framework";

        /// <summary>
        /// Do not write onfo messages to log
        /// </summary>
        public static bool EnableInfoMessages = true;

        /// <summary>
        /// Parent object for the auto-generated framework object
        /// </summary>
        public static GameObject FrameworkObjectParent;

// ReSharper disable InconsistentNaming
        private static GameObject _frameworkObject;
// ReSharper restore InconsistentNaming
        /// <summary>
        /// Returns a framework object
        /// </summary>
        internal static GameObject FrameworkObject
        {
            get
            {
                //int levelId = Application.loadedLevel;
                //Debug.Log("levelId: " + levelId);

                /**
                 * A little caching :)
                 * */
                if (null != _frameworkObject)
                    return _frameworkObject;

                /**
                 * 1) Try to find a framework object
                 * */
                GameObject fo = GameObject.Find("/" + FrameworkObjectName);

                /**
                 * 2) If not found, instantiate it
                 * */
                if (null == fo) // no instance present
                {
                    /**
                     * 2a) Instantiate
                     * */

                    fo = new GameObject { hideFlags = HideFlags.NotEditable | HideFlags.HideInHierarchy | HideFlags.HideInInspector };

                    fo.AddComponent(typeof (FrameworkMonoBehaviour));

                    /**
                     * Please don't remove this line
                     * This is the only line I'd like to have written in Your app log :)
                     * It's interesting to see if application uses the framework
                     * */
                    if (EnableInfoMessages)
                        Log(string.Format(@"instantiated {0}", new Info()));

                    fo.name = FrameworkObjectName;

                    //fo.hideFlags = HideFlags.DontSave;

                    /**
                     * 2b) Allow setting a parent
                     * */
                    if (null != FrameworkObjectParent)
                        fo.transform.parent = FrameworkObjectParent.transform;
                }

                _frameworkObject = fo;

#if DEBUG
                if (DebugMode)
                    Debug.Log("Framework object initialized");
#endif
                return fo;
            }
        }

        /// <summary>
        /// Gets the component by its type
        /// </summary>
        /// <param name="instantiateIfNotFound">If component not found on the framework object, should I instantiate a new component and stick it in?</param>
        /// <returns></returns>
        public static Component GetComponent<T>(bool instantiateIfNotFound) where T : Component
        {
            Component component = FrameworkObject.GetComponent(typeof(T));

            if (null == component && instantiateIfNotFound)
                component = CreateComponent<T>(true);

            return component;
        }

        /// <summary>
        /// Creates a component and sticks it to the framework object
        /// If the framework object has not been created, it creates it now
        /// </summary>
        /// <param name="exclusive">Should only one instance of this script exist in the application?</param>
        public static Component CreateComponent<T>(bool exclusive) where T:Component
        {
            Component component = FrameworkObject.GetComponent<T>();

            if (null == component) // no instance present
            {
                component = FrameworkObject.AddComponent(typeof(T));

#if DEBUG
                if (DebugMode)
                    Log(string.Format(@"added component ""{0}""", typeof(T)));
#endif
            }
            //else if (retValue.Length > 1 && exclusive)
            //{
            //    throw new ApplicationException(string.Format("More than one {0} object exists on the scene!", scriptType.Name));
            //}

            return component;
        }

//        public static void Reset()
//        {
//            _frameworkObject = null;

//#if DEBUG
//            if (DebugMode)
//                Debug.Log("Framework object destroyed");
//#endif
//        }

        public static void LoadLevel(int id)
        {
            SystemManager.Instance.Dispose();
            Application.LoadLevel(id);
            //Timer t = new Timer(1, 1);
            //t.Start();
            //t.Complete += delegate { Application.LoadLevel(id); };
        }

        public static void LoadLevelAdditive(int id)
        {
            LoadLevelAdditive(id, false);
        }
        
        public static void LoadLevelAdditive(int id, bool disposeSystemManager)
        {
            if (disposeSystemManager)
                SystemManager.Instance.Dispose();
            Application.LoadLevelAdditive(id);
        }

        private static void Log(string text)
        {
            Debug.Log(string.Format(@"eDriven framework: {0}", text));
        }
    }
}
