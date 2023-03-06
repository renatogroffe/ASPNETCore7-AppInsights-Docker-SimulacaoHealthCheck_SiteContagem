using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteContagem.Pages;

public class IndexModel : PageModel
{
    private static readonly Contador _CONTADOR = new();
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        int valorAtual;
        lock (_CONTADOR)
        {
            _CONTADOR.Incrementar(Convert.ToInt32(_configuration["Incremento"]));
            valorAtual = _CONTADOR.ValorAtual;
        }

        _logger.LogInformation($"Contador - Valor atual: {valorAtual}");

        TempData["Contador"] = valorAtual;
        TempData["Local"] = _CONTADOR.Local;
        TempData["NodeK8s"] = _configuration["Kubernetes:Node"];
        TempData["NamespaceK8s"] = _configuration["Kubernetes:Namespace"];
        TempData["Kernel"] = _CONTADOR.Kernel;
        TempData["Framework"] = _CONTADOR.Framework;
        TempData["MensagemVariavel"] = _configuration["MensagemVariavel"];
    }
}