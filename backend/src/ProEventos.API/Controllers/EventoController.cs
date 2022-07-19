using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public EventoController()
        {
        }

        public IEnumerable<Evento> _evento = new Evento[]
        {
            new Evento() {
                    EventoId = 1,
                    Tema = "Angular 14 DotNet",
                    Local = "Castelo",
                    Lote = "1º Lote",
                    QtdPessoas = 10,
                    DataEvento = DateTime.Now.AddDays(2).ToString(),
                    ImagemURL = "imagem.png"
                },
                new Evento() {
                    EventoId = 2,
                    Tema = "Vue Js",
                    Local = "Stoky",
                    Lote = "1º Lote",
                    QtdPessoas = 15,
                    DataEvento = DateTime.Now.AddDays(5).ToString(),
                    ImagemURL = "imagem2.png"
                }
        };

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }
        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(p => p.EventoId == id);
        }
        [HttpPost]
        public string Post()
        {
            return "value post";
        }
        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"value post {id}";
        }
    }
}