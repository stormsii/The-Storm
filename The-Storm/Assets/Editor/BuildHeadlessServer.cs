using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildHeadlessServer
{
    [MenuItem("Build/Build Dedicated Server")]
    public static void BuildServer()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[]  // change this to your actual scene
            { 
                "Assets/Scenes/ServerBootstrap.unity",
                "Assets/Scenes/Gameplay.unity"
            },

            locationPathName = "Builds/ServerBuild/Server.exe",   // or "Builds/ServerBuild" for Linux
            target = BuildTarget.StandaloneWindows64,
            subtarget = (int)StandaloneBuildSubtarget.Server,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("✅ Server build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("❌ Server build failed");
        }
    }
}
