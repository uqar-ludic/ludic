using Microsoft.Build.BuildEngine;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compil
{
    public static class Compil
    {
        public static void ExecuteCompil(string pathSln, string pathLog)
        {
            string logFilePath = pathLog;
            List<string> OutputHeaderRow = new List<string>();
            List<string> OutputItemRow = new List<string>();
            string artifactPath = pathSln;

            try
            {
                //Create a new FileLogger with the path given as parameter
                FileLogger logger = new FileLogger();
                logger.Parameters = @"logfile=" + logFilePath;

                //Generate the ProjectCollection with the sln's path given as parameter
                ProjectCollection pc = new ProjectCollection();
                Dictionary<string, string> GlobalProperty = new Dictionary<string, string>() { { "Configuration", "Debug" }, { "Platform", "Any CPU" } };
                BuildRequestData buildRequest = new BuildRequestData(artifactPath, GlobalProperty, null, new string[] { "Build" }, null);

                //Set the ProjectCollection as BuildParameter
                BuildParameters bp = new BuildParameters(pc);
                bp.Loggers = new List<Microsoft.Build.Framework.ILogger> { logger }.AsEnumerable();

                //Build the solution
                BuildResult buildResult = BuildManager.DefaultBuildManager.Build(bp, buildRequest);

                pc.UnregisterAllLoggers();

                //Create output for the build with the FileLogger informations
                string[] solutionBuildOutputs = File.ReadAllLines(logFilePath);
                OutputHeaderRow.Add("Artifact;Build Result");
                string[] splitter = { "__________________________________________________" };
                string loggerOutput = File.ReadAllText(logFilePath);
                string[] projectResults = loggerOutput.Split(splitter, StringSplitOptions.None);
                foreach (string projectBuildDetails in projectResults)
                    if (projectBuildDetails.Contains("(default targets):"))
                        if (projectBuildDetails.Contains("Done building project \""))
                        {
                            string[] lines = projectBuildDetails.Split("\n".ToCharArray());
                            string buildFailedProjectName = lines.Where(x => x.Contains("Done building project \"")).FirstOrDefault();
                            buildFailedProjectName = buildFailedProjectName.Replace("Done building project ", string.Empty).Trim();
                            buildFailedProjectName = buildFailedProjectName.Replace("\"", string.Empty);
                            buildFailedProjectName = buildFailedProjectName.Replace(" -- FAILED.", string.Empty);
                            OutputItemRow.Add(buildFailedProjectName + ";FAILED");
                        }
                        else
                        {
                            string[] lines = projectBuildDetails.Split("\n".ToCharArray());
                            string buildSuccededProjectName = lines.Where(x => x.Contains(" (default targets):")).FirstOrDefault().Replace("\" (default targets):", "");
                            string finalProjectName = buildSuccededProjectName.Substring(buildSuccededProjectName.LastIndexOf("\\") + 1);
                            OutputItemRow.Add(finalProjectName + ";SUCCEEDED");
                        }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //File.Delete(logFilePath);
            }
        }
    }
}
