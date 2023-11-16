using Microsoft.AspNetCore.Mvc;
using Prova.Data;
using Prova.Models;


[ApiController]
[Route("api/funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public FuncionarioController(AppDataContext ctx) =>

        _ctx = ctx;


    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Funcionario funcionario)
    {
        _ctx.Funcionarios.Add(funcionario);
        _ctx.SaveChanges();

        return Created("", funcionario);
    }


    [HttpGet]
    [Route("listar")]

    public IActionResult Listar()
    {
        var funcionarios = _ctx.Funcionarios.ToList();
        return Ok(funcionarios);

    }

}
