using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MATSDK
{
    public class MATDelegate : MonoBehaviour
    {
        public static event Action<string> TrackerDidSucceed;
        
        public void trackerDidSucceed (string data)
        {
            #if UNITY_IPHONE
            data = DecodeFrom64 (data);
            printLog ("MATDelegate trackerDidSucceed: " + data);
            
            #endif
            #if (UNITY_ANDROID || UNITY_WP8 || UNITY_METRO)
            printLog ("MATDelegate trackerDidSucceed: " + data);
            #endif
            
            if (TrackerDidSucceed != null)
                TrackerDidSucceed(data);
        }

        public static event Action<string> TrackerDidFail;
        public void trackerDidFail (string error)
        {
            printLog ("MATDelegate trackerDidFail: " + error);
            
            if (TrackerDidFail != null)
                TrackerDidFail(error);
        }
        
        public static event Action<string> TrackerDidEnqueueRequest;
        public void trackerDidEnqueueRequest (string refId)
        {
            printLog ("MATDelegate trackerDidEnqueueRequest: " + refId);
            if (TrackerDidEnqueueRequest != null)
                TrackerDidEnqueueRequest(refId);
        }


        public static event Action<string> TrackerDidReceivedDeeplink;
        public void trackerDidReceiveDeeplink (string url)
        {
            printLog ("MATDelegate trackerDidReceiveDeeplink: " + url);

            // TODO: add your custom code to handle the deferred deeplink url
            if (TrackerDidReceivedDeeplink != null)
                TrackerDidReceivedDeeplink(url);
        }

        public static event Action<string> TrackerDidFailDeeplink;
        public void trackerDidFailDeeplink (string error)
        {
            printLog ("MATDelegate trackerDidFailDeeplink: " + error);
            
            if (TrackerDidFailDeeplink != null)
                TrackerDidFailDeeplink(error);
        }

        public void onAdLoad(String placement)
        {
            printLog ("MATDelegate onAdLoad: placement = " + placement);
        }

        public void onAdLoadFailed(String error)
        {
            printLog ("MATDelegate onAdLoadFailed: " + error);
        }

        public void onAdClick(String empty)
        {
            printLog ("MATDelegate onAdClick");
        }
        
        public void onAdShown(String empty)
        {
            printLog ("MATDelegate onAdShown");
        }

        public void onAdActionStart(String willLeaveApplication)
        {
            printLog ("MATDelegate onAdActionStart: willLeaveApplication = " + willLeaveApplication);
        }
        
        public void onAdActionEnd(String empty)
        {
            printLog ("MATDelegate onAdActionEnd");
        }

        public void onAdRequestFired(String request)
        {
            printLog ("MATDelegate onAdRequestFired: request = " + request);
        }

        public void onAdClosed(String empty)
        {
            printLog ("MATDelegate onAdClosed");
        }

        /// <summary>
        /// The method to decode base64 strings.
        /// </summary>
        /// <param name="encodedData">A base64 encoded string.</param>
        /// <returns>A decoded string.</returns>
        public static string DecodeFrom64 (string encodedString)
        {
            string decodedString = null;

            #if !(UNITY_WP8) && !(UNITY_METRO)
            printLog ("MATDelegate.DecodeFrom64(string)");

            //this line causes the following error when building for Windows 8 phones:
            //Error building Player: Exception: Error: method `System.String System.Text.Encoding::GetString(System.Byte[])` doesn't exist in target framework. It is referenced from Assembly-CSharp.dll at System.String MATDelegateScript::DecodeFrom64(System.String).
            //Because of this, I'm currently choosing to disable it when Windows 8 phones are used. I'll see if I can find 
            //something better later. Until then, I'll probably use an else branch to take care of the UNITY_WP8 case.
            decodedString = System.Text.Encoding.UTF8.GetString (System.Convert.FromBase64String (encodedString));
            #endif

            return decodedString;
        }
        
        private static void printLog(string msg)
        {
            
        }
    }
}
