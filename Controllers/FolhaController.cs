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
[HttpPut]
[Route("alterar/{id}")]
public IActionResult AlterarFolha(int id, [FromBody] Folha folha)
{
    try
    {
        if (folha == null)
        {
            return BadRequest("O objeto Folha não pode ser nulo.");
        }

        // Verifica se existe uma folha de pagamento com o mesmo ID antes de fazer a alteração
        var existingFolha = _ctx.Folhas.FirstOrDefault(f => f.FolhaId == id);

        if (existingFolha == null)
        {
            return NotFound("Folha de pagamento não encontrada para alteração.");
        }

        // Realiza a alteração nos campos necessários
        existingFolha.Valor = folha.Valor;
        existingFolha.Quantidade = folha.Quantidade;
        existingFolha.Mes = folha.Mes;
        existingFolha.Ano = folha.Ano;
        existingFolha.FuncionarioId = folha.FuncionarioId;
        existingFolha.SalarioBruto = folha.SalarioBruto;

        _ctx.Folhas.Update(existingFolha);
        _ctx.SaveChanges();

        return Created("", existingFolha);
    }
    catch (Exception e)
    {
        return BadRequest($"Ocorreu um erro ao tentar alterar a folha de pagamento: {e.Message}");
    }
}







}
