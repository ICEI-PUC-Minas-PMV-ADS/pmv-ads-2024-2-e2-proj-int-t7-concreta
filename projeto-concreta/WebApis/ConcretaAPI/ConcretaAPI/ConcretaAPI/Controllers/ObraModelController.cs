﻿using ConcretaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ConcretaAPI.Data;
using ConcretaAPI.DTOs;
using Microsoft.EntityFrameworkCore; // Para acessar o DbContext

namespace ConcretaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraModelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObraModelController(AppDbContext context)
        {
            _context = context;
        }
        //Método para adicionar uma nova Obra com mapeamento entre DTO e entidade
        [HttpPost("cadastrar")]
        public async Task<ActionResult<ObraDTO>> PostObras(ObraDTO obraDto)
        {
            //Verificar na model (e por onde passa) o campo UrlFoto (ArquivoFoto) esta com o tipo invalido gerando erro 400 bad request
            //O ajuste abaixo faz com que o banco salve porem ocorre um erro interno de servidor (error 500)
            //O erro se originou na mudanca desse metodo para o ObraDTO com o novo campo Construtora
            var obra = new ObraModel
            {
                Nome = obraDto.Nome,
                Construtora = obraDto.Construtora, // Adicionando o campo Construtora
                Localizacao = obraDto.Localizacao,
                Descricao = obraDto.Descricao,
                DataInicio = obraDto.DataInicio,
                DataFim = obraDto.DataFim,
                IdUf = obraDto.IdUf,
                UrlFoto = obraDto.ArquivoFoto,
                IdArquivoFoto = obraDto.IdArquivoFoto,
            };

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            obraDto.IdObra = obra.IdObra;

            return CreatedAtAction("GetObra", new { id = obraDto.IdObra }, obraDto);
        }

        [HttpGet("em-andamento")]
        public async Task<ActionResult<IEnumerable<ObraModel>>> GetObrasEmAndamento()
        {
            var obrasEmAndamento = await _context.Obras
                .Where(obra => obra.DataFim == null || obra.DataFim > DateTime.Now)
                .ToListAsync();
            return obrasEmAndamento;
        }

        [HttpGet("obra-finalizada")]
        public async Task<ActionResult<IEnumerable<ObraModel>>> GetObraFinalizada()
        {
            var obraFinalizada = await _context.Obras
                .Where(obra => obra.DataFim != null && obra.DataFim <= DateTime.Now)
                .ToListAsync();
            return obraFinalizada;
        }
  
    }
}



