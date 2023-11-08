using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos;
using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgosPeriodo;
using ari_ib_calificaciones_api_domain.Enums;
using ari_ib_calificaciones_api_web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ari_ib_calificaciones_api_web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CalificadoraRiesgosController : ControllerBase
    {
        /// <summary>
        /// Actualizar una calificadora por ID
        /// </summary>
        /// <remarks>Actualiza una calificadora por su ID.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <param name="body">Datos actualizados de la calificadora</param>
        /// <response code="200">Calificadora actualizada exitosamente</response>
        /// <response code="400">Solicitud incorrecta</response>
        [HttpPut]
        [Route("/calificadoras-de-riesgo/{id}")]
        [ValidateModelState]
        [SwaggerOperation("ActualizarCalificadora")]
        [SwaggerResponse(statusCode: 200, type: typeof(CalificadorasRiesgos), description: "Calificadora actualizada exitosamente")]
        public virtual IActionResult ActualizarCalificadora([FromRoute][Required] int? id, [FromBody] CalificadorasRiesgos body)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(CalificadorasRiesgos));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"clave\" : \"clave\",\n  \"periodos\" : [ {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  }, {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  } ],\n  \"nombre\" : \"nombre\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadorasRiesgos>(exampleJson)
            : default(CalificadorasRiesgos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Actualizar un periodo de la calificadora por ID
        /// </summary>
        /// <remarks>Actualiza un periodo de la colección de periodos de la calificadora por su ID.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <param name="periodoId">ID del periodo</param>
        /// <param name="body">Datos actualizados del periodo</param>
        /// <response code="200">Periodo actualizado exitosamente</response>
        /// <response code="400">Solicitud incorrecta</response>
        [HttpPut]
        [Route("/calificadoras-de-riesgo/{id}/periodos/{periodoId}")]
        [ValidateModelState]
        [SwaggerOperation("ActualizarPeriodoDeCalificadora")]
        [SwaggerResponse(statusCode: 200, type: typeof(CalificadoraRiesgosPeriodos), description: "Periodo actualizado exitosamente")]
        public virtual IActionResult ActualizarPeriodoDeCalificadora([FromRoute][Required] int? id, [FromRoute][Required] int? periodoId, [FromBody] CalificadoraRiesgosPeriodos body)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(CalificadoraRiesgosPeriodos));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"fechaHasta\" : \"2000-01-23\",\n  \"updatedBy\" : \"updatedBy\",\n  \"fechaNotificacionAlta\" : \"2000-01-23\",\n  \"fechaDesde\" : \"2000-01-23\",\n  \"createdBy\" : \"createdBy\",\n  \"removedBy\" : \"removedBy\",\n  \"fechaNotificacionBaja\" : \"2000-01-23\",\n  \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"version\" : 0,\n  \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"draft\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadoraRiesgosPeriodos>(exampleJson)
            : default(CalificadoraRiesgosPeriodos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Agregar un periodo a la calificadora
        /// </summary>
        /// <remarks>Agrega un nuevo periodo a la colección de periodos de la calificadora.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <param name="body">Datos del nuevo periodo</param>
        /// <response code="201">Periodo agregado exitosamente</response>
        /// <response code="400">Solicitud incorrecta</response>
        [HttpPost]
        [Route("/calificadoras-de-riesgo/{id}/periodos")]
        [ValidateModelState]
        [SwaggerOperation("AgregarPeriodoACalificadora")]
        [SwaggerResponse(statusCode: 201, type: typeof(CalificadoraRiesgosPeriodos), description: "Periodo agregado exitosamente")]
        public virtual IActionResult AgregarPeriodoACalificadora([FromRoute][Required] int? id, [FromBody] CalificadoraRiesgosPeriodos body)
        {
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Periodo));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"fechaHasta\" : \"2000-01-23\",\n  \"updatedBy\" : \"updatedBy\",\n  \"fechaNotificacionAlta\" : \"2000-01-23\",\n  \"fechaDesde\" : \"2000-01-23\",\n  \"createdBy\" : \"createdBy\",\n  \"removedBy\" : \"removedBy\",\n  \"fechaNotificacionBaja\" : \"2000-01-23\",\n  \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"version\" : 0,\n  \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"draft\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadoraRiesgosPeriodos>(exampleJson)
            : default(CalificadoraRiesgosPeriodos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Cambia el estado del periodo de &#x27;draft&#x27; a &#x27;current&#x27;.
        /// </summary>
        /// <param name="periodoId">ID del periodo a actualizar.</param>
        /// <param name="body"></param>
        /// <response code="200">Estado del periodo actualizado con éxito.</response>
        /// <response code="404">Periodo no encontrado.</response>
        [HttpPost]
        [Route("/calificadoras-de-riesgo/periodos/{periodoId}/change-status")]
        [ValidateModelState]
        [SwaggerOperation("ChangePeriodStatus")]
        public virtual IActionResult ChangePeriodStatus([FromRoute][Required] int? periodoId, [FromBody] TipoEstado body)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Crear una nueva calificadora
        /// </summary>
        /// <remarks>Crea una nueva calificadora.</remarks>
        /// <param name="body">Datos de la nueva calificadora</param>
        /// <response code="201">Calificadora creada exitosamente</response>
        /// <response code="400">Solicitud incorrecta</response>
        [HttpPost]
        [Route("/calificadoras-de-riesgo")]
        [ValidateModelState]
        [SwaggerOperation("CrearCalificadora")]
        [SwaggerResponse(statusCode: 201, type: typeof(CalificadorasRiesgos), description: "Calificadora creada exitosamente")]
        public virtual IActionResult CrearCalificadora([FromBody] CalificadorasRiesgos body)
        {
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(CalificadorasRiesgos));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\n  \"clave\" : \"clave\",\n  \"periodos\" : [ {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  }, {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  } ],\n  \"nombre\" : \"nombre\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadorasRiesgos>(exampleJson)
            : default(CalificadorasRiesgos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Eliminar una calificadora por ID
        /// </summary>
        /// <remarks>Elimina una calificadora por su ID.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <response code="204">Calificadora eliminada exitosamente</response>
        /// <response code="404">Calificadora no encontrada</response>
        [HttpDelete]
        [Route("/calificadoras-de-riesgo/{id}")]
        [ValidateModelState]
        [SwaggerOperation("EliminarCalificadora")]
        public virtual IActionResult EliminarCalificadora([FromRoute][Required] int? id)
        {
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Eliminar un periodo de la calificadora por ID
        /// </summary>
        /// <remarks>Elimina un periodo de la colección de periodos de la calificadora por su ID.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <param name="periodoId">ID del periodo</param>
        /// <response code="204">Periodo eliminado exitosamente</response>
        /// <response code="404">Periodo no encontrado</response>
        [HttpDelete]
        [Route("/calificadoras-de-riesgo/{id}/periodos/{periodoId}")]
        [ValidateModelState]
        [SwaggerOperation("EliminarPeriodoDeCalificadora")]
        public virtual IActionResult EliminarPeriodoDeCalificadora([FromRoute][Required] int? id, [FromRoute][Required] int? periodoId)
        {
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtener detalles de una calificadora por ID
        /// </summary>
        /// <remarks>Retorna los detalles de una calificadora por su ID.</remarks>
        /// <param name="id">ID de la calificadora</param>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="404">Calificadora no encontrada</response>
        [HttpGet]
        [Route("/calificadoras-de-riesgo/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetCalificadoraPorId")]
        [SwaggerResponse(statusCode: 200, type: typeof(CalificadorasRiesgos), description: "Respuesta exitosa")]
        public virtual IActionResult GetCalificadoraPorId([FromRoute][Required] int? id)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(CalificadorasRiesgos));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"clave\" : \"clave\",\n  \"periodos\" : [ {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  }, {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  } ],\n  \"nombre\" : \"nombre\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadorasRiesgos>(exampleJson)
            : default(CalificadorasRiesgos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Obtener lista de calificadoras de riesgo
        /// </summary>
        /// <remarks>Retorna una lista de calificadoras de riesgo.</remarks>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="404">No se encontraron calificadoras de riesgo</response>
        [HttpGet]
        [Route("/calificadoras-de-riesgo")]
        [ValidateModelState]
        [SwaggerOperation("GetCalificadorasDeRiesgo")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<CalificadorasRiesgos>), description: "Respuesta exitosa")]
        public virtual IActionResult GetCalificadorasDeRiesgo()
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<CalificadorasRiesgos>));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "[ {\n  \"clave\" : \"clave\",\n  \"periodos\" : [ {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  }, {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  } ],\n  \"nombre\" : \"nombre\"\n}, {\n  \"clave\" : \"clave\",\n  \"periodos\" : [ {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  }, {\n    \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"fechaHasta\" : \"2000-01-23\",\n    \"updatedBy\" : \"updatedBy\",\n    \"fechaNotificacionAlta\" : \"2000-01-23\",\n    \"fechaDesde\" : \"2000-01-23\",\n    \"createdBy\" : \"createdBy\",\n    \"removedBy\" : \"removedBy\",\n    \"fechaNotificacionBaja\" : \"2000-01-23\",\n    \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"version\" : 0,\n    \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n    \"status\" : \"draft\"\n  } ],\n  \"nombre\" : \"nombre\"\n} ]";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<CalificadorasRiesgos>>(exampleJson)
            : default(List<CalificadorasRiesgos>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Obtener un periodo específico por ID.
        /// </summary>
        /// <param name="id">ID de la calificadora de riesgo.</param>
        /// <param name="periodoId">ID del periodo a recuperar.</param>
        /// <response code="200">Período recuperado con éxito.</response>
        /// <response code="404">Período no encontrado.</response>
        [HttpGet]
        [Route("/calificadoras-de-riesgo/{id}/periodos/{periodoId}")]
        [ValidateModelState]
        [SwaggerOperation("GetPeriodoById")]
        [SwaggerResponse(statusCode: 200, type: typeof(CalificadoraRiesgosPeriodos), description: "Período recuperado con éxito.")]
        public virtual IActionResult GetPeriodoById([FromRoute][Required] int? id, [FromRoute][Required] int? periodoId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Periodo));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"createdAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"fechaHasta\" : \"2000-01-23\",\n  \"updatedBy\" : \"updatedBy\",\n  \"fechaNotificacionAlta\" : \"2000-01-23\",\n  \"fechaDesde\" : \"2000-01-23\",\n  \"createdBy\" : \"createdBy\",\n  \"removedBy\" : \"removedBy\",\n  \"fechaNotificacionBaja\" : \"2000-01-23\",\n  \"removedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"version\" : 0,\n  \"updatedAt\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"draft\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CalificadoraRiesgosPeriodos>(exampleJson)
            : default(CalificadoraRiesgosPeriodos);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
