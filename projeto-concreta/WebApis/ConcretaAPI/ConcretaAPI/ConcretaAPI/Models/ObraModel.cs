﻿using System.ComponentModel.DataAnnotations;

namespace ConcretaAPI.Models
{
    public class ObraModel
    {
        [Key]
        public int IdObra { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Construtora { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int IdUf { get; set; }        // Referência à Unidade Federativa (UF)
        public int? IdArquivoFoto { get; set; }   // Referência opcional à foto
        public string UrlFoto { get; set; }   // URL da foto
    }

}
