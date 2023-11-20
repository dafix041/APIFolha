using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Funcionario funcionarioCadastrado = _ctx.Funcionarios.Find(id);

            if (funcionarioCadastrado != null)
            {
                _ctx.Funcionarios.Remove(funcionarioCadastrado);
                _ctx.SaveChanges();
        
                List<Funcionario> funcionarioAtualizados = _ctx.Funcionarios.ToList();
                return Ok(funcionarioAtualizados);
            }

            return NotFound("Funcionário não encontrado");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



[HttpPut]
[Route("alterar/{id}")]
public IActionResult Alterar(int id, [FromBody] Funcionario funcionario)
{
    try
    {
        if (funcionario == null)
        {
            return BadRequest("O objeto Funcionario não pode ser nulo.");
        }

        // Verifica se existe um funcionário com o mesmo nome e CPF antes de fazer a alteração
        var existingFuncionario = _ctx.Funcionarios.FirstOrDefault(f => f.Id == id);

        if (existingFuncionario == null)
        {
            return NotFound("Funcionário não encontrado para alteração.");
        }

        // Realiza a alteração nos campos necessários
        existingFuncionario.Nome = funcionario.Nome;
        existingFuncionario.CPF  = funcionario.CPF;

        _ctx.Funcionarios.Update(existingFuncionario);
        _ctx.SaveChanges();

        return Created("", existingFuncionario);
    }
    catch (Exception e)
    {
        return BadRequest($"Ocorreu um erro ao tentar alterar o funcionário: {e.Message}");
    }
}
}