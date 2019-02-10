﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Controllers
{
    internal class CspPost
    {
        [JsonProperty("csp-report")]
        public CspReport CspReport { get; set; }
    }

    internal class CspReport
    {
        [JsonProperty("document-uri")]
        public string DocumentUri { get; set; }

        [JsonProperty("referrer")]
        public string Referrer { get; set; }

        [JsonProperty("violated-directive")]
        public string ViolatedDirective { get; set; }

        [JsonProperty("effective-directive")]
        public string EffectiveDirective { get; set; }

        [JsonProperty("original-policy")]
        public string OriginalPolicy { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }

        [JsonProperty("blocked-uri")]
        public string BlockedUri { get; set; }

        [JsonProperty("line-number")]
        public int LineNumber { get; set; }

        [JsonProperty("column-number")]
        public int ColumnNumber { get; set; }

        [JsonProperty("source-file")]
        public string SourceFile { get; set; }

        [JsonProperty("status-code")]
        public string StatusCode { get; set; }

        [JsonProperty("script-sample")]
        public string ScriptSample { get; set; }
    }

    [Route("api/[controller]")]
    public class CspReportController : BasePublicController
    {
        private readonly ILogger<CspReportController> _logger;

        public CspReportController(ILogger<CspReportController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Log()
        {
            /* a sample payload:
            {
              "csp-report": {
                "document-uri": "http://localhost:5000/untypedSha",
                "referrer": "",
                "violated-directive": "script-src",
                "effective-directive": "script-src",
                "original-policy": "default-src 'self'; style-src 'self'; script-src 'self'; font-src 'self'; img-src 'self' data:; connect-src 'self'; media-src 'self'; object-src 'self'; report-uri /api/Home/CspReport",
                "disposition": "enforce",
                "blocked-uri": "eval",
                "line-number": 21,
                "column-number": 8,
                "source-file": "http://localhost:5000/scripts.bundle.js",
                "status-code": 200,
                "script-sample": ""
              }
            }
            */

            CspPost cspPost;
            using (var bodyReader = new StreamReader(this.HttpContext.Request.Body))
            {
                var body = await bodyReader.ReadToEndAsync();

                _logger.LogError($"Content Security Policy Error: {body}");

                this.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                cspPost = JsonConvert.DeserializeObject<CspPost>(body);
            }

            return Ok();
        }
    }
}
