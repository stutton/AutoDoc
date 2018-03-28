namespace Stutton.AppExtensionClient.Contracts
{
    public enum AppServiceResponseStatus
    {
        Success,
        Failure,
        ResourceLimitsExceeded,
        Unknown,
        RemoteSystemUnavailable,
        MessageSizeTooLarge
    }
}