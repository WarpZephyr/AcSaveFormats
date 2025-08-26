namespace AcSaveFormats.ArmoredCoreForAnswer.Emblems
{
    /// <summary>
    /// A type which determines how the emblem is stored.
    /// </summary>
    public enum EmblemType : byte
    {
        /// <summary>
        /// There is no emblem data.
        /// </summary>
        None = 0,

        /// <summary>
        /// The emblem is preset.
        /// </summary>
        Preset = 1,

        /// <summary>
        /// The emblem is custom.
        /// </summary>
        Custom = 2
    }
}
