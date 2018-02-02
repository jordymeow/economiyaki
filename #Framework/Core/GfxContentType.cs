namespace Finance.Framework.Core
{
    /// <summary>
    /// Message type.
    /// </summary>
    public enum GfxType
    {
        /// <summary>
        /// Realtime data (1 stock).
        /// </summary>
        RealtimeData,
        /// <summary>
        /// Realtime data (> 1 stock).
        /// </summary>
        RealtimeDataMulti,
        /// <summary>
        /// Static data (1 stock).
        /// </summary>
        StaticData,
        /// <summary>
        /// Static data (> 1 stock).
        /// </summary>
        StaticDataMulti,
        /// <summary>
        /// History data (1 stock).
        /// </summary>
        HistoryData,
        /// <summary>
        /// History data (> 1 stock).
        /// </summary>
        HistoryDataMulti,
        /// <summary>
        /// Information data.
        /// </summary>
        Information
    }
}