using CalcMatrizServerApi.Models;
using CalcMatrizServerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalcMatrizServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatrizController : ControllerBase
    {
        private readonly ILogger<MatrizController> _logger;

        public MatrizController(ILogger<MatrizController> logger)
        {
            _logger = logger;
        }

        [RequestSizeLimit(178257920)] // 170 MB
        [HttpPost(Name = "MultiplyMatrix")]
        public MatrixReturn Post(MatrixModel model)
        {
            var service = new MatrixService();

            var result = service.MultiplyMatrixes(model.matrixA, model.matrixB, model.index, model.length);

            return result;
        }
    }
}
