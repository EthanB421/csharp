namespace DotnetAPI{
    public partial class UserSalary{
        public int UserId {get;set}
        public string FirstName {get;set}
        public string LastName {get;set}
        public string Email{get;set}
        public string Gender {get;set}
        public bool Active {get;set}

        public UserSalary()
        {
            if(FirstName == null)
            {
                FirstName = "";
            }
            if(LastName == null)
            {
                FirstName = "";
}            if(Gender== null)
            {
                FirstName = "";
}            if(Email== null)
            {
                FirstName = "";
            }
        }
    }
}