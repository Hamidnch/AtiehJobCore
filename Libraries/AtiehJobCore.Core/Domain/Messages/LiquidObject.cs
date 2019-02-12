using DotLiquid;
using System.Collections.Generic;

namespace AtiehJobCore.Core.Domain.Messages
{
    /// <summary>
    /// An object that acumulates all DotLiquid Drops
    /// </summary>
    public partial class LiquidObject
    {
        public LiquidObject()
        {
            AdditionalTokens = new Dictionary<string, string>();
        }

        public Drop User { get; set; }

        public Drop PrivateMessage { get; set; }

        public Drop EmailAFriend { get; set; }

        public Drop AskQuestion { get; set; }

        public Drop ContactUs { get; set; }

        public IDictionary<string, string> AdditionalTokens { get; set; }
    }
}
