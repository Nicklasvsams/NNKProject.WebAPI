namespace NNKProject.BLL.DTO
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = "Secret";
        public string SaveData { get; set; } = string.Empty;
        public bool IsAuthorized { get; set; } = false;
    }
}
