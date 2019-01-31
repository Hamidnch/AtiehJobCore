using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Utilities;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Runtime.Loader;

namespace AtiehJobCore.Common.Roslyn
{
    public static class RoslynCompiler
    {
        #region Const

        public const string ScriptPath = "/Roslyn";
        public const string ShadowCopyScriptPath = "~/Roslyn/bin";

        #endregion

        #region Fields

        private static DirectoryInfo _shadowCopyScriptPath;

        #endregion

        public static void Initialize(ApplicationPartManager applicationPartManager, AtiehJobConfig config)
        {
            if (applicationPartManager == null)
                throw new ArgumentNullException(nameof(applicationPartManager));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (!config.UseRoslynScripts)
                return;

            var referencedScripts = new List<ResultCompiler>();

            var roslynFolder = new DirectoryInfo(CommonHelper.MapPath(ScriptPath));

            _shadowCopyScriptPath = new DirectoryInfo(CommonHelper.MapPath(ShadowCopyScriptPath));
            Directory.CreateDirectory(_shadowCopyScriptPath.FullName);

            //clear bin files
            var binFiles = _shadowCopyScriptPath.GetFiles("*", SearchOption.AllDirectories);
            foreach (var f in binFiles)
            {
                try
                {
                    //ignore index.htm
                    var fileName = Path.GetFileName(f.FullName);
                    if (fileName.Equals("index.htm", StringComparison.OrdinalIgnoreCase))
                        continue;

                    File.Delete(f.FullName);
                }
                catch (Exception exc)
                {
                    Debug.WriteLine("Error deleting file " + f.Name + ". Exception: " + exc);
                }
            }

            try
            {
                var ctxFiles = roslynFolder.GetFiles("*.csx", SearchOption.TopDirectoryOnly);
                foreach (var file in ctxFiles)
                {
                    var csScript = new ResultCompiler();
                    csScript.OriginalFile = file.FullName;

                    var ctxCode = System.IO.File.ReadAllText(file.FullName);
                    var sourceFileResolver = new SourceFileResolver(ImmutableArray<string>.Empty, AppContext.BaseDirectory);
                    var opts = ScriptOptions.Default.WithSourceResolver(sourceFileResolver);
                    var script = CSharpScript.Create(ctxCode, opts);
                    var compilation = script.GetCompilation();
                    using (var ms = new MemoryStream())
                    {
                        var compilationResult = compilation.Emit(ms);

                        if (compilationResult.Success)
                        {
                            ms.Seek(0, SeekOrigin.Begin);
                            var shadowFileName = Path.Combine(_shadowCopyScriptPath.FullName, Guid.NewGuid().ToString("D") + ".dll");
                            File.WriteAllBytes(shadowFileName, ms.ToArray());
                            var shadowCopiedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(shadowFileName);
                            csScript.DllAssemblyFile = shadowFileName;
                            csScript.ReferencedAssembly = shadowCopiedAssembly;
                            csScript.IsCompiled = true;
                            applicationPartManager.ApplicationParts.Add(new AssemblyPart(shadowCopiedAssembly));
                        }
                        else
                        {
                            foreach (var diagnostic in compilationResult.Diagnostics)
                            {
                                csScript.ErrorInfo.Add(diagnostic.ToString());
                            }
                        }
                    }
                    referencedScripts.Add(csScript);

                }
                ReferencedScripts = referencedScripts;
            }
            catch (Exception ex)
            {
                var msg = $"Roslyn '{ex.Message}'";

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        /// <summary>
        /// Returns a collection of all referenced assemblies 
        /// </summary>
        public static IEnumerable<ResultCompiler> ReferencedScripts { get; set; }

        /// <summary>
        /// Method for compiling the code for testing in admin panel
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResultCompiler ResultCompiledScript(string code)
        {
            var result = new ResultCompiler();
            var sourceFileResolver = new SourceFileResolver(ImmutableArray<string>.Empty, AppContext.BaseDirectory);
            var opts = ScriptOptions.Default.WithSourceResolver(sourceFileResolver);
            var script = CSharpScript.Create(code, opts);
            var compilation = script.GetCompilation();
            using (var ms = new MemoryStream())
            {
                var compilationResult = compilation.Emit(ms);
                if (compilationResult.Success)
                {
                    result.IsCompiled = true;
                }
                else
                {
                    foreach (var diagnostic in compilationResult.Diagnostics)
                    {
                        result.ErrorInfo.Add(diagnostic.ToString());
                    }
                }
            }
            return result;
        }
    }
}
