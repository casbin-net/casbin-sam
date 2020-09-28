namespace Casbin.Sam.Core
{
    public class SamRegister
    {
        public string ClientId { get; set; } = string.Empty;
        public string ScopeId { get; set; } = SamConstants.DefaultAuthorizationScopeId;
        public string? ClientUrl { get; set; }
        public string? VersionToken { get; set; }
    }
}
