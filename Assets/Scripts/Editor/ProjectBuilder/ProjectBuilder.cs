namespace Editor.ProjectBuilder
{
    using System.Collections.Generic;
    using App.DataBase;
    using App.DataBase.Structures;
    using UnityEditor;


    public class ProjectBuilder
    {
        [MenuItem("Build/Android Build")]
        public static void BuildAndroid()
        {
            GetParamsFile<BuildParams>(out var buildParams);
            string[] levels = GetAllScenes();

            PlayerSettings.applicationIdentifier = buildParams.id;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, buildParams.id);

            PlayerSettings.Android.keystoreName = buildParams.keystorePath;
            PlayerSettings.keyaliasPass = buildParams.keystorePass;
            PlayerSettings.keystorePass = buildParams.keystorePass;
 
            if (buildParams.development)
            {
                BuildDevelopmentApk();
            }
            else
            {
                BuildReleaseApk();
                BuildReleaseAAb();
            }

            void BuildDevelopmentApk()
            {
                EditorUserBuildSettings.development = true;
                EditorUserBuildSettings.buildAppBundle = false;
                BuildPipeline.BuildPlayer(levels,$"Build/development/APK/{buildParams.name}{buildParams.buildNumber}DEV.apk", BuildTarget.Android, BuildOptions.Development);
            }
            
            void BuildReleaseApk()
            {
                EditorUserBuildSettings.development = false;
                EditorUserBuildSettings.buildAppBundle = false;
                BuildPipeline.BuildPlayer(levels,$"Build/release/APK/{buildParams.name}{buildParams.buildNumber}REL.apk", BuildTarget.Android, BuildOptions.None);
            }

            void BuildReleaseAAb()
            {
                EditorUserBuildSettings.development = false;
                EditorUserBuildSettings.buildAppBundle = true;
                BuildPipeline.BuildPlayer(levels,$"Build/release/AAB/{buildParams.name}{buildParams.buildNumber}REL.aab", BuildTarget.Android, BuildOptions.None);
            }
        }
        
        [MenuItem("Build/iOS Build")]
        public static void BuildIOS()
        {
            
        }

        private static string[] GetAllScenes()
        {
            List<string> scenes = new List<string>();
            foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    scenes.Add(scene.path);
                }
            }

            return scenes.ToArray();
        }

        private static void GetParamsFile<T>(out T buildParams)
        {
            var dataBaseHelper = new DataBaseHelper();
            dataBaseHelper.TextIntoType("buildparams.txt", out T parsedResult);
            buildParams = parsedResult;
        }
    }
    
}