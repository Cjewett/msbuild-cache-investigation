var target = Argument("target", "ContinuousIntegration");
var configuration = Argument("configuration", "Release");
var remoteCache = Argument("remote-cache", "false");

Task("Clean")
    .Does(() =>
    {
        CleanDirectories("./**/obj", new CleanDirectorySettings() { Force = true });
        CleanDirectories("./**/bin", new CleanDirectorySettings() { Force = true });
        CleanDirectories("./**/TestResults", new CleanDirectorySettings() { Force = true });
    });

Task("Build")
    .Does(() =>
    {
        DotNetMSBuildSettings dotNetMSBuildSettings = new DotNetMSBuildSettings
        {
            ContinuousIntegrationBuild = Convert.ToBoolean(remoteCache)
        };

        DotNetBuildSettings dotNetBuildSettings = new DotNetBuildSettings
        {
            Configuration = configuration,
            MSBuildSettings = dotNetMSBuildSettings
        };

        DotNetBuild("./Root.sln", dotNetBuildSettings);
    });

Task("Test")
    .Does(() =>
    {
        DotNetTestSettings settings = new DotNetTestSettings
        {
            Configuration = configuration
        };

        DotNetTest("./Root.sln", settings);
    });

Task("ContinuousIntegration")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

RunTarget(target);
