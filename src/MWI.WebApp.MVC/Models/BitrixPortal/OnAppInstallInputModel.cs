namespace MWI.WebApp.MVC.Models.BitrixPortal
{
    public class OnAppInstallInputModel
    {
        public string Event { get; set; } = string.Empty;
        public Data Data { get; set; } = new();
        public string Ts { get; set; } = string.Empty;
        public Auth Auth { get; set; } = new();
    }

    public class Auth
    {
        public string Access_Token { get; set; } = string.Empty;
        public string Expires_In { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string Server_Endpoint { get; set; } = string.Empty;
        public char Status { get; set; }
        public string Client_Endpoint { get; set; } = string.Empty;
        public string Member_Id { get; set; } = string.Empty;
        public string Refresh_Token { get; set; } = string.Empty;
        public string Application_Token { get; set; } = string.Empty;
    }

    public class Data
    {
        public string Version { get; set; } = string.Empty;
        public string Language_Id { get; set; } = string.Empty;
    }
}
