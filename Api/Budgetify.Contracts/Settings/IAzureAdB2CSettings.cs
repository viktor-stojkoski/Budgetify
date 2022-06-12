namespace Budgetify.Contracts.Settings
{
    public interface IAzureAdB2CSettings
    {
        /// <summary>
        /// TODO
        /// </summary>
        string Instance { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string CallbackPath { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string SignUpSignInPolicyId { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string ResetPasswordPolicyId { get; }

        /// <summary>
        /// TODO
        /// </summary>
        string EditProfilePolicyId { get; }
    }
}
