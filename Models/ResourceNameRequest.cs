﻿namespace AzureNamingTool.Models
{
    public class ResourceNameRequest
    {
        public string? ResourceEnvironment { get; set; }
        public string? ResourceFunction { get; set; }
        public string? ResourceInstance { get; set; }
        public string? ResourceLocation { get; set; }
        public string? ResourceOrg { get; set; }
        public string? ResourceProjAppSvc { get; set; }
        public string ResourceType { get; set; }
        public string? ResourceUnitDept { get; set; }
        /// <summary>
        /// Dictionary [Custom Component Type Name],[Custom Component Short Name Value]
        /// </summary>
        public Dictionary<string, string>? CustomComponents { get; set; }
        /// <summary>
        /// String - Resource name (example: ApiManagement/service)
        /// </summary>
        public string? Resource {get; set;}
    }
}
