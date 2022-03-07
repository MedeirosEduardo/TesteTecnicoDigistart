using System.Collections.Generic;

namespace TesteTecnicoDigiStart.Domain
{
    public class GetInformationResponseDTO
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<ResultDTO> results { get; set; }
    }
}
