using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova.Data;
using Prova.Models;

[ApiController]
[Route("api/folha")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public FolhaController(AppDataContext ctx) =>

        _ctx = ctx;

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Folha folha)
    {
        Funcionario? funcionario = _ctx.Funcionarios.Find(folha.FuncionarioId);
        if (funcionario == null)
        {
            return NotFound();
        }
        folha.Funcionario = funcionario;
        _ctx.Folhas.Add(folha);
        _ctx.SaveChanges();

        return Created("", folha);
    }


    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {

        var folhas = _ctx.Folhas.Include(f => f.Funcionario).ToList();

        if (folhas.Count == 0)
        {
            return NotFound("Nenhuma folha de pagamento encontrada.");
        }
        return Ok(folhas);
    }

}
