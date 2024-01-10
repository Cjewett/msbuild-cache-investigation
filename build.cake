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

Task("Restore")
    .Does(() =>
    {
        MSBuild("./Root.sln", settings => settings
            .WithTarget("restore")
            .SetVerbosity(Verbosity.Minimal)
            .SetConfiguration(configuration)
            .SetContinuousIntegrationBuild(Convert.ToBoolean(remoteCache))
            .WithToolPath("C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\amd64\\MSBuild.exe")
            .WithProperty("graphBuild", "true")
            .WithProperty("reportFileAccesses", "true"));
    });

Task("Build")
    .Does(() =>
    {
        MSBuild("./Root.sln", settings => settings
            .WithTarget("build")
            .SetVerbosity(Verbosity.Minimal)
            .SetConfiguration(configuration)
            .SetContinuousIntegrationBuild(Convert.ToBoolean(remoteCache))
            .WithToolPath("C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\amd64\\MSBuild.exe")
            .WithProperty("graphBuild", "true")
            .WithProperty("reportFileAccesses", "true"));
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
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

RunTarget(target);
