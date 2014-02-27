using Microsoft.Build.BuildEngine;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Compilation
{
    public static class CompilationLudic
    {
        #region Variables
        #endregion

        public static string[] Compile(string pathSolution, string logPath)
        {
            string logFilePath = logPath;
            List<string> OutputHeaderRow = new List<string>();
            List<string> OutputItemRow = new List<string>();
            string artifactPath = pathSolution;

            try
            {
                FileLogger logger = new FileLogger();
                logger.Parameters = @"logfile=" + logFilePath;

                ProjectCollection pc = new ProjectCollection();
                Dictionary<string, string> GlobalProperty = new Dictionary<string, string>() { { "Configuration", "Debug" }, { "Platform", "Any CPU" } };
                BuildRequestData buildRequest = new BuildRequestData(artifactPath, GlobalProperty, null, new string[] { "Build" }, null);

                BuildParameters bp = new BuildParameters(pc);
                bp.Loggers = new List<Microsoft.Build.Framework.ILogger> { logger }.AsEnumerable();

                BuildResult buildResult = BuildManager.DefaultBuildManager.Build(bp, buildRequest);

                pc.UnregisterAllLoggers();

                string[] solutionBuildOutputs = File.ReadAllLines(logFilePath);
                OutputHeaderRow.Add("Artifact;Build Result");
                string[] splitter = { "__________________________________________________" };
                string loggerOutput = File.ReadAllText(logFilePath);
                string[] projectResults = loggerOutput.Split(splitter, StringSplitOptions.None);
                //return projectResults;
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
            return null;
        }
    }
}
