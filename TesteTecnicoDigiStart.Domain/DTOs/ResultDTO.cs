namespace TesteTecnicoDigiStart.Domain
{
    public class ResultDTO
    {
        public object city { get; set; }
        public string city_ibge_code { get; set; }
        public int confirmed { get; set; }
        public double confirmed_per_100k_inhabitants { get; set; }
        public string date { get; set; }
        public double death_rate { get; set; }
        public int deaths { get; set; }
        public int estimated_population { get; set; }
        public int estimated_population_2019 { get; set; }
        public bool is_last { get; set; }
        public int order_for_place { get; set; }
        public string place_type { get; set; }
        public string state { get; set; }
    }
}
