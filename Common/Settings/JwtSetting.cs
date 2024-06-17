namespace clothes.api.Common.Settings
{
    public class JwtSetting
    {
        public const string SectionName = "Logging";
        public string Secret{ get; set; }
        public int ExpiryDays { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
