﻿// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0(the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#if UNITY_ANDROID || UNITY_EDITOR
using UnityEngine;
using System;

public class GvrKeyboardIntent {

  // The Play Store intent is requested via an Android Activity Fragment java object.
  private AndroidJavaObject keyboardFragment = null;

  // Constants used via JNI to access the keyboard fragment.
  private const string FRAGMENT_CLASSNAME =
    "com.google.gvr.keyboardsupport.KeyboardFragment";
  private const string CALLBACK_CLASSNAME = FRAGMENT_CLASSNAME +
    "$KeyboardCallback";

  // Singleton instance.
  private static GvrKeyboardIntent theInstance;

  /// The singleton instance of the PermissionsRequester class,
  /// lazily instantiated.
  public static GvrKeyboardIntent Instance {
    get {
      if (theInstance == null) {
        theInstance = new GvrKeyboardIntent();
        if (!theInstance.InitializeFragment()) {
          Debug.LogError("Cannot initialize fragment!");
          theInstance = null;
        }
      }
      return theInstance;
    }
  }

  /// <summary>
  /// Initializes the fragment via JNI.
  /// </summary>
  /// <returns>True if fragment was initialized.</returns>
  protected bool InitializeFragment() {
#if UNITY_EDITOR
    Debug.LogWarning("GvrKeyboardIntent requires the Android runtime environment");
    return false;
#elif UNITY_ANDROID
    AndroidJavaClass ajc = new AndroidJavaClass(FRAGMENT_CLASSNAME);

    if (ajc != null) {
      // Get the KeyboardFragment object
      keyboardFragment = ajc.CallStatic<AndroidJavaObject>("getInstance",
        GvrActivityHelper.GetActivity());
    }

    return keyboardFragment != null &&
        keyboardFragment.GetRawObject() != IntPtr.Zero;
#endif
  }

  public void LaunchPlayStore() {
    KeyboardCallback cb = new KeyboardCallback();
    keyboardFragment.Call("launchPlayStore", cb);
  }

  /// <summary>
  /// Keyboard callback implementation.
  /// </summary>
  /// <remarks>Instances of this class are passed to the java fragment and then
  /// invoked once the request process is completed by the user.
  /// </remarks>
  class KeyboardCallback : AndroidJavaProxy {

    internal KeyboardCallback() : base(CALLBACK_CLASSNAME) {
    }

    /// <summary>
    /// Called when then flow is completed.
    /// </summary>
    void onPlayStoreResult() {
      Application.Quit();
    }
  }

}
#endif // UNITY_ANDROID || UNITY_EDITOR
