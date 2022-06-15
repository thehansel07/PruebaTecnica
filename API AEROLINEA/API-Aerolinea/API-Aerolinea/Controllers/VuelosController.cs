using Aerolinea.Datos;
using Aerolinea.Entidades.PaisA;
using Aerolinea.Entidades.VuelosA;
using Aerolinea.Servicio.Interfaces;
using API_Aerolinea.Models.Mantenimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aerolinea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VuelosController : Controller
    {
        private readonly IVuelosRepositorio _vuelosRepositorio;
        private readonly IPaisRepositorio _paisRepositorio;
        private readonly AerolineaDbContext _context;


        public VuelosController(IVuelosRepositorio vuelosRepositorio, IPaisRepositorio paisRepositorio, AerolineaDbContext context)
        {
            _vuelosRepositorio = vuelosRepositorio;
            _paisRepositorio = paisRepositorio;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: api/Usuarios/Listar
        // Metodo Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<VuelosViewModel>> Listar()
        {
            var vuelos = await _vuelosRepositorio.ObtenerTodoAsync();
            List<VuelosViewModel> lista = new List<VuelosViewModel>();

            if (vuelos != null)
            {

                foreach (var item in vuelos)
                {
                    VuelosViewModel model = new VuelosViewModel();
                    var paisOrigen = await _paisRepositorio.ObtenerAsync(a => a.idPais == item.idPaisOrigenVuelo);
                    var paisDestino = await _paisRepositorio.ObtenerAsync(a => a.idPais == item.idPaisDestinoVuelo);

                    model.idVuelos = item.idVuelos;
                    model.idVuelos = item.idVuelos;
                    model.idPaisDestinoVuelo = item.idPaisDestinoVuelo;
                    model.idPaisOrigenVuelo = item.idPaisOrigenVuelo;
                    model.fechaInicial = item.fechaFinal.ToShortDateString();
                    model.fechaFinal = item.fechaFinal.ToShortDateString();
                    model.cantPersonasVuelo = item.cantPersonasVuelo;
                    model.Rowguid = item.Rowguid;
                    model.PaisDestinoVuelo = paisOrigen.paisNombre;
                    model.PaisOrigenVuelo = paisDestino.paisNombre;
                    lista.Add(model);
                }
            }

            return lista.AsQueryable();

        }



        [HttpGet("[action]")]
        public async Task<IEnumerable<Pais>> ListarPaises()
        {
            var listadoPaises = await _paisRepositorio.ObtenerTodoAsync();
            return listadoPaises.AsQueryable();
        }


        // POST: api/Vuelos/Agregar
        //Metodo Agregar
        [HttpPost("[action]")]
        public async Task<IActionResult> Agregar([FromBody] VuelosStartViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                Vuelos vuelo = new Vuelos
                {
                    cantPersonasVuelo = modelo.cantPersonasVuelo,
                    fechaFinal = modelo.fechaFinal,
                    fechaInicial = modelo.fechaInicial,
                    idPaisDestinoVuelo = int.Parse(modelo.idPaisDestinoVuelo),
                    idPaisOrigenVuelo = int.Parse(modelo.idPaisOrigenVuelo),
                    Rowguid = Guid.NewGuid(),

                };

                bool existo = true;
                existo = await _vuelosRepositorio.AgregarAsync(vuelo);

                if (existo == true)
                {
                    return Json("Se agrego correctamente");

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return Ok();

        }

        // Delete: api/Usuarios/ObtenerPorIdVuelos
        //Metodo Obtener por Id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerPorIdVuelos([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest($"El vuelo con el ID:{id}, no existe.");
            }
            bool existo = true;
            try
            {
                var usuario = await _vuelosRepositorio.ObtenerAsync(f => f.idVuelos == id);
                if (existo == true)
                {
                    return Json(new { result = usuario });

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return Ok();

        }



        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] VuelosStartViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (modelo.idVuelos <= 0)
            {
                return BadRequest("");
            }

            var vuelos = await _vuelosRepositorio.ObtenerAsync(a => a.idVuelos == modelo.idVuelos);
            _context.Entry(vuelos).State = EntityState.Detached;

            if (vuelos == null)
            {
                return NotFound($"Vuelo con el ID{modelo.idVuelos}");
            }

            try
            {


                Vuelos vuelo = new Vuelos
                {
                    cantPersonasVuelo = modelo.cantPersonasVuelo,
                    fechaFinal = modelo.fechaFinal,
                    fechaInicial = modelo.fechaInicial,
                    idPaisDestinoVuelo = int.Parse(modelo.idPaisDestinoVuelo),
                    idPaisOrigenVuelo = int.Parse(modelo.idPaisOrigenVuelo),
                };

                bool existo = true;
                existo = await _vuelosRepositorio.ActualizarAsync(vuelo);

                if (existo == true)
                {
                    return Json("Se Actualizo correctamente");

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return Ok();
        }

        // Delete: api/Vuelos/Eliminar
        //Metodo Eliminar
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int Id)
        {
            if (Id <= 0)
            {
                return BadRequest($"Usuario con el ID:{Id}, no existe.");
            }
            bool existo = true;
            try
            {
                var usuario = await _vuelosRepositorio.ObtenerAsync(f => f.idVuelos == Id);
                existo = await _vuelosRepositorio.EliminarAync(usuario);
                if (existo == true)
                {
                    return Json("Se han eliminado correctamente");

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return Ok();

        }




    }
}
