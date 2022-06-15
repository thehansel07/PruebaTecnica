using Aerolinea.Datos;
using Aerolinea.Entidades.Usuarios;
using Aerolinea.Servicio.Interfaces;
using API_Aerolinea.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Aerolinea.Entidades.ViewModels;
using API_Aerolinea.Models.ManejoExcepciones;

namespace API_Aerolinea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepositorio _usuariosRepository;
        private readonly IConfiguration _config;
        private readonly AerolineaDbContext _context;

        public UsuariosController(IUsuariosRepositorio usuariosRepository, IConfiguration config, AerolineaDbContext context)
        {
            _usuariosRepository = usuariosRepository;
            _config = config;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/Usuarios/Listar
        // Metodo Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _usuariosRepository.ObtenerTodoAsync();


            return usuario.Select(u => new UsuarioViewModel
            {
                IdUsuario = u.IdUsuario,
                RolesRolId = u.RolesRolId,
                DescricionRol = u.Roles.RolDescripcion,
                UsuNombre = u.UsuNombre,
                UsuNumDocumento = u.UsuNumDocumento,
                UsuDireccion = u.UsuDireccion,
                Usutelefono = u.Usutelefono,
                UsuEmail = u.UsuEmail,
                UsuApellidos = u.UsuApellidos,
                Usucondicion = u.Usucondicion,
                Rowguid = u.Rowguid
            });

        }


        // POST: api/Usuarios/Agregar
        //Metodo Agregar
        [HttpPost("agregar")]
        public async Task<IActionResult> Agregar([FromBody] UsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var correo = await _usuariosRepository.ObtenerAsync(a => a.UsuEmail.ToLower() == modelo.UsuEmail.ToLower());

            try
            {
                if (correo != null)
                {
                    return BadRequest("El email ya existe");

                }

                CrearPasswordHash(modelo.UsuContraseña, out byte[] passwordHash, out byte[] passwordSalt);

                Usuario usuario = new Usuario
                {
                    IdUsuario = modelo.IdUsuario,
                    UsuNombre = modelo.UsuNombre,
                    UsuApellidos = modelo.UsuApellidos,
                    UsuNumDocumento = modelo.UsuNumDocumento,
                    UsuDireccion = modelo.UsuDireccion,
                    Usutelefono = modelo.Usutelefono,
                    UsuContraseña = passwordHash,
                    UsuContraseñaConfirma = passwordSalt,
                    Usucondicion = true,
                    UsuEmail = modelo.UsuEmail,
                    RolesRolId = 2,
                    Rowguid = Guid.NewGuid(),
                };
                bool existo = true;

                existo = await _usuariosRepository.AgregarAsync(usuario);

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


        // POST: api/Usuarios/Actualizar
        //Metodo Agregar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] UserViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (modelo.idUsuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _usuariosRepository.ObtenerAsync(a => a.IdUsuario == modelo.idUsuario);
            _context.Entry(usuario).State = EntityState.Detached;

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            try
            {

                CrearPasswordHash(modelo.usuContraseña, out byte[] passwordHash, out byte[] passwordSalt);

                Usuario current = new Usuario
                {
                    IdUsuario = usuario.IdUsuario,
                    UsuNombre = modelo.usuApellido,
                    UsuApellidos = modelo.usuApellido,
                    UsuNumDocumento = modelo.usuNumDocumento,
                    UsuDireccion = modelo.usuDireccion,
                    Usutelefono = modelo.usuTelefono,
                    UsuContraseña = passwordHash ?? usuario.UsuContraseña,
                    UsuContraseñaConfirma = passwordSalt ?? usuario.UsuContraseña,
                    Usucondicion = modelo.usuCondicion,
                    UsuEmail = modelo.usuEmail,
                    RolesRolId = usuario.RolesRolId,
                    Rowguid = usuario.Rowguid

                };
                bool existo = true;

                existo = await _usuariosRepository.ActualizarAsync(current);

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

        // Delete: api/Usuarios/Eliminar
        //Metodo Eliminar
        [Authorize(Roles = "Administrador")]
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
                var usuario = await _usuariosRepository.ObtenerAsync(f => f.IdUsuario == Id);
                existo = await _usuariosRepository.EliminarAync(usuario);
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


        // Delete: api/Usuarios/ObtenerPorIdUsuario
        //Metodo Obtener por Id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerPorIdUsuario([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Usuario con el ID:{id}, no existe.");
            }
            bool existo = true;
            try
            {
                var usuario = await _usuariosRepository.ObtenerAsync(f => f.IdUsuario == id);
                var rol = await _context.Roles.FirstOrDefaultAsync(x => x.RolId == usuario.RolesRolId);


                UsuarioViewModel modelo = new UsuarioViewModel
                {
                    IdUsuario = usuario.IdUsuario,
                    RolesRolId = usuario.RolesRolId,
                    DescricionRol =rol.RolDescripcion,
                    UsuNombre = usuario.UsuNombre,
                    UsuNumDocumento = usuario.UsuNumDocumento,
                    UsuDireccion = usuario.UsuDireccion,
                    Usutelefono = usuario.Usutelefono,
                    UsuEmail = usuario.UsuEmail,
                    UsuApellidos = usuario.UsuApellidos,
                    Usucondicion = usuario.Usucondicion,
                    Rowguid = usuario.Rowguid


                };
                if (existo == true)
                {
                    return Json(new { result = modelo });

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return Ok();

        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private List<ExcepcionesViewModel> ManejoExcepciones(bool existo, string respuesta, int Id)
        {
            ExcepcionesViewModel excepciones = new ExcepcionesViewModel();
            List<ExcepcionesViewModel> manejoExcepciones = new List<ExcepcionesViewModel>();
            if (existo == false)
            {
                excepciones.Estado = "Error";
            }
            else
            {
                excepciones.Estado = "Ok";


            }
            excepciones.Resultado = respuesta;
            excepciones.Id = Id;
            manejoExcepciones.Add(excepciones);

            return manejoExcepciones;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            bool existo = true;
            var usuario = await _usuariosRepository.IniciarSesion(modelo);
            var roles = await _context.Roles.FirstOrDefaultAsync(a => a.RolId == usuario.RolesRolId);

            if (usuario == null)
            {
                existo = false;
                string respuesta = $"Usuario {modelo.Correo} no existe";
                return Json(new { result = ManejoExcepciones(existo, respuesta, usuario.IdUsuario) });

            }

            try
            {
                if (!VerificarPasswordHash(modelo.Contraseña, usuario.UsuContraseña, usuario.UsuContraseñaConfirma))
                {
                    return NotFound();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Email, modelo.Correo),
                    new Claim(ClaimTypes.Role, usuario.Roles.RolDescripcion ),
                    new Claim("IdUsuario", usuario.IdUsuario.ToString() ),
                    new Claim("Roles", usuario.Roles.RolDescripcion ),
                    new Claim("UsuNombre", usuario.UsuNombre )
                };

                string respuesta = $"Proceso Ejecutado Correctamente";
                return Ok(new { token = GenerarToken(claims), result = ManejoExcepciones(existo, respuesta,usuario.IdUsuario), TipoUsu=roles.RolDescripcion });

            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error {ex}");
            }
        }


        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }


    }
}
