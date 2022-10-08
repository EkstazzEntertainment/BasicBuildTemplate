namespace PostBuild
{
#if UNITY_EDITOR

    using System.IO;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEditor.iOS.Xcode;
    using UnityEngine;

    public class PostBuildActions
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log(pathToBuiltProject);
            ConductPostBuildActions(target, pathToBuiltProject);
        }

        private static void ConductPostBuildActions(BuildTarget target, string pathToBuiltProject) 
        {
            if (target == BuildTarget.iOS) 
            {
                IOSPostBuildActions(target, pathToBuiltProject);
            }
            else if (target == BuildTarget.Android) 
            {
                AndroidPostBuildActions(target, pathToBuiltProject);
            }
        }

        private static void AndroidPostBuildActions(BuildTarget target, string pathToBuiltProject) 
        {
            
        }
        
        private static void IOSPostBuildActions(BuildTarget target, string pathToBuiltProject) 
        {
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
       
            PlistElementDict rootDict = plist.root;
                
            rootDict.SetBoolean ("ITSAppUsesNonExemptEncryption", false);
       
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
#endif
}
