namespace ArkNet.Utils.Enum
{
    /// <summary>
    /// Represents an Ark transaction type.
    /// </summary>
    public enum TransactionType : byte
    {
        /// <summary>
        /// Indicates that Ark was expended to a recepient on the Ark network.
        /// </summary>
        SendArk,

        /// <summary>
        /// Indicates that Ark was expended to register a delegeate on the Ark network.
        /// </summary>
        CreateDelegate,

        /// <summary>
        /// Indicates that Ark was expended to issue a vote for a candidate deligate.
        /// </summary>
        VoteDelegate,

        /// <summary>
        /// Indicates that Ark was expended to acquire multi-signature transaction signing.
        /// </summary>
        MultiSignature
    }
}
