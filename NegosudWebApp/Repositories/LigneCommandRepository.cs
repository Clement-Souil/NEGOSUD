using NegosudLibrary.DTO;
using NegosudWebApp.Services;

namespace NegosudWebApp.Repositories;

public class LigneCommandRepository
{
    private readonly HttpClientService _httpClientService;


    public LigneCommandRepository(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<List<LigneCommandeDTO>> GetByCommandIdAsync(int id)
    {
        List<LigneCommandeDTO> dto = await _httpClientService.GetLignesCommandeByCommandeId(id);

        return dto;
    }


}
