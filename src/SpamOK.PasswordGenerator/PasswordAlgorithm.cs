namespace SpamOK.PasswordGenerator
{
    /// <summary>
    /// Password generation algorithm.
    /// </summary>
    public enum PasswordAlgorithm
    {
        /// <summary>
        /// Basic algorithm.
        /// </summary>
        Basic,

        /// <summary>
        /// Dictionary algorithm which uses a list of words to generate a passphrase.
        /// </summary>
        Diceware,
    }
}
