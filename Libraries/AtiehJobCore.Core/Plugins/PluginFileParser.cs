using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AtiehJobCore.Core.Plugins
{
    public static class PluginFileParser
    {
        public static IList<string> ParseInstalledPluginsFile(string filePath)
        {
            //read and parse the file
            if (!File.Exists(filePath))
                return new List<string>();

            var text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            var lines = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(str))
                        continue;
                    lines.Add(str.Trim());
                }
            }
            return lines;
        }

        public static void SaveInstalledPluginsFile(IList<string> pluginSystemNames, string filePath)
        {
            //var result = "";
            //foreach (var sn in pluginSystemNames)
            //    result += $"{sn}{Environment.NewLine}";

            var result = pluginSystemNames.Aggregate("", (current, sn) => current + $"{sn}{Environment.NewLine}");

            File.WriteAllText(filePath, result);
        }

        public static PluginDescriptor ParsePluginDescriptionFile(string filePath)
        {
            var descriptor = new PluginDescriptor();
            var text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
                return descriptor;

            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(str))
                        continue;
                    settings.Add(str.Trim());
                }
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(':');
                if (separatorIndex == -1)
                {
                    continue;
                }
                var key = setting.Substring(0, separatorIndex).Trim();
                var value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "Group":
                        descriptor.Group = value;
                        break;
                    case "FriendlyName":
                        descriptor.FriendlyName = value;
                        break;
                    case "SystemName":
                        descriptor.SystemName = value;
                        break;
                    case "Version":
                        descriptor.Version = value;
                        break;
                    case "SupportedVersions":
                        {
                            //parse supported versions
                            descriptor.SupportedVersions = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim())
                                .ToList();
                        }
                        break;
                    case "Author":
                        descriptor.Author = value;
                        break;
                    case "DisplayOrder":
                        {
                            int.TryParse(value, out var displayOrder);
                            descriptor.DisplayOrder = displayOrder;
                        }
                        break;
                    case "FileName":
                        descriptor.PluginFileName = value;
                        break;
                    case "LimitedToStores":
                        {
                            //parse list of store IDs
                            foreach (var str1 in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(x => x.Trim()))
                            {
                                descriptor.LimitedToStores.Add(str1);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return descriptor;
        }

        public static void SavePluginDescriptionFile(PluginDescriptor plugin)
        {
            if (plugin == null)
                throw new ArgumentException("plugin");

            //get the Description.txt file path
            if (plugin.OriginalAssemblyFile == null)
                throw new Exception($"Cannot load original assembly path for {plugin.SystemName} plugin.");
            var filePath = Path.Combine(plugin.OriginalAssemblyFile.Directory?.FullName ?? string.Empty, "Description.txt");
            if (!File.Exists(filePath))
                throw new Exception($"Description file for {plugin.SystemName} plugin does not exist. {filePath}");

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Group", plugin.Group),
                new KeyValuePair<string, string>("FriendlyName", plugin.FriendlyName),
                new KeyValuePair<string, string>("SystemName", plugin.SystemName),
                new KeyValuePair<string, string>("Version", plugin.Version),
                new KeyValuePair<string, string>("SupportedVersions", string.Join(",", plugin.SupportedVersions)),
                new KeyValuePair<string, string>("Author", plugin.Author),
                new KeyValuePair<string, string>("DisplayOrder", plugin.DisplayOrder.ToString()),
                new KeyValuePair<string, string>("FileName", plugin.PluginFileName)
            };
            if (plugin.LimitedToStores.Any())
            {
                var storeList = string.Join(",", plugin.LimitedToStores);
                keyValues.Add(new KeyValuePair<string, string>("LimitedToStores", storeList));
            }

            var sb = new StringBuilder();
            for (var i = 0; i < keyValues.Count; i++)
            {
                var key = keyValues[i].Key;
                var value = keyValues[i].Value;
                sb.AppendFormat("{0}: {1}", key, value);
                if (i != keyValues.Count - 1)
                    sb.Append(Environment.NewLine);
            }
            //save the file
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
