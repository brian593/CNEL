using System;
using System.ComponentModel.DataAnnotations;

namespace LaLuz.Models
{
	public class DataWhitJson
	{
        [Key]
        public int Id { get; set; }

		public DateTime SaveDate { get; set; }
		public string IdCNEL { get; set; }
		public string TypeCNEL { get; set; }
		public string JsonData { get; set; }
	}
}

