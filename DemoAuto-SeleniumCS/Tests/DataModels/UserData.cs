using Newtonsoft.Json;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

[Description("userData")]
public class UserData
{

    [JsonIgnore]
    public UserData OriginalUser { get; set; }

    [JsonProperty("loginId")]
    public string LoginId { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    /**
     * Set default password, if force=true, will set to default even if password has value
     */
    public string SetDefaultPassword()
    {
        if (string.IsNullOrEmpty(Password))
            Password = $"Auto{LoginId}1.";
        return Password;
    }

    [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
    public int? UserId { get; set; }

    [JsonProperty("currencyCode")]
    public int CurrencyCode { get; set; }

    [JsonProperty("laborRateCurrencyCode")]
    public int LaborRateCurrencyCode { get; set; }

    [JsonProperty("userType")]
    public int UserType { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("languageId")]
    public int LanguageId { get; set; }

    [JsonProperty("autoSave")]
    public int AutoSave { get; set; }

    [JsonProperty("dateFormat")]
    public int DateFormat { get; set; }

    [JsonProperty("timeFormat")]
    public int TimeFormat { get; set; }

    [JsonProperty("numberFormatId")]
    public int NumberFormatId { get; set; }

    [JsonProperty("timezoneId")]
    public int TimezoneId { get; set; }

    [JsonProperty("htmlEmail")]
    public bool HtmlEmail { get; set; }

    [JsonProperty("analyzeAllDomains")]
    public int AnalyzeAllDomains { get; set; }

    [JsonProperty("isPinReset")]
    public int IsPinReset { get; set; }

    [JsonProperty("passwordSetDate")]
    public DateTime PasswordSetDate { get; set; }

    [JsonProperty("adHocUser")]
    public bool AdHocUser { get; set; }

    [JsonProperty("activeFlag")]
    public int ActiveFlag { get; set; }

    [JsonProperty("applicationUser")]
    public bool ApplicationUser { get; set; }

    [JsonProperty("portalUser")]
    public bool PortalUser { get; set; }

    [JsonProperty("reviewUser")]
    public bool ReviewUser { get; set; }

    [JsonProperty("isOutOfOffice")]
    public bool IsOutOfOffice { get; set; }

    [JsonProperty("outOfOfficeStatus")]
    public int OutOfOfficeStatus { get; set; }

    [JsonProperty("localeId")]
    public int LocaleId { get; set; }

    [JsonProperty("lastLoginDate")]
    public DateTime LastLoginDate { get; set; }

    [JsonProperty("clientLoggingLevel")]
    public int ClientLoggingLevel { get; set; }

    [JsonProperty("groups")]
    public int[] Groups { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("paperSize")]
    public int PaperSize { get; set; }

    [JsonProperty("preferredLandingPage")]
    public int PreferredLandingPage { get; set; }

    [JsonProperty("themeId")]
    public int ThemeId { get; set; }


    public TimeZoneData GetTimezone()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "Framework", "Data", "timezones.json");

        string json = File.ReadAllText(filePath);

        List<TimeZoneData> timeZones = JsonConvert.DeserializeObject<List<TimeZoneData>>(json);

        TimeZoneData timeZone = timeZones.FirstOrDefault(tz => tz.TimeZoneId == this.TimezoneId);

        return timeZone;
    }

}