namespace CBParser.Models
{
    public class ParticipantInfo
    {
        public string NameP { get; set; }
        public string CntrCd { get; set; }
        public string Rgn { get; set; }
        public string Ind { get; set; }
        public string Tnp { get; set; }
        public string Nnp { get; set; }
        public string Adr { get; set; }
        public string DateIn { get; set; }
        public string PtType { get; set; }
        public string Srvcs { get; set; }
        public string XchType { get; set; }
        public string UID { get; set; }
        public string ParticipantStatus { get; set; }

    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public string RegulationAccountType { get; set; }
        public string CK { get; set; }
        public string AccountCBRBIC { get; set; }
        public string DateIn { get; set; }
        public string AccountStatus { get; set; }
    }

    public class BICDirectoryEntry
    {
        public string BIC { get; set; }
        public List<Account> Accounts { get; set; }
        public ParticipantInfo ParticipantInfos { get; set; }
    }
}

