namespace CBParser.Models
{
    public class ParticipantInfo
    {
        public string NameP { get; set; }
        public string Rgn { get; set; }
    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public string RegulationAccountType { get; set; }
        // Другие свойства Account
    }

    public class BICDirectoryEntry
    {
        public string BIC { get; set; }
        public ParticipantInfo ParticipantInfo { get; set; }
        public List<Account> Accounts { get; set; }
    }
}

