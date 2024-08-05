namespace HelloWorld.Models{
    public class Computer
    {
        public int ComputerId {get; set; }
        public string Motherboard {get; set;} = "";
        public int? cpuCores {get; set;} = 0;
        public bool hasWifi{get; set;}
        public bool hasLTE{get; set;}
        public DateTime? releaseDate{get; set;}
        public decimal price{get; set;}
        public string videocard{get; set;} = "";
    }
}
