
using NegosudLibrary.DAO;
using NegosudLibrary.DTO;
using NegosudWebApp.Interfaces;
using NegosudWebApp.Services;

namespace NegosudWebApp.Repositories;

public class CommandeRepository /*: IRepository<CommandeDTO>*/
{
    private readonly HttpClientService _httpClientService;
    private readonly LigneCommandRepository _ligneCommandRepository;

    public CommandeRepository(HttpClientService httpClientService, LigneCommandRepository ligneCommandRepository)
    {
        _httpClientService = httpClientService;
        _ligneCommandRepository = ligneCommandRepository;
    }

    public async Task<List<CommandeDTO>> GetAllAsync()
    {
        return await _httpClientService.GetCommandesAsync() ?? new List<CommandeDTO>();
    }

    public async Task<CommandeDTO> GetByIdAsync(int id)
    {
        var dto = await _httpClientService.GetCommandeByIdAsync(id) ?? new CommandeDTO();
        dto.LignesCommandes = await _ligneCommandRepository.GetByCommandIdAsync(id);

        return dto;
    }

    public async Task<bool> AddAsync(CommandeDTO dto)
    {
        Commande commande = new Commande()
        {
            Date = dto.Date,
            UserId = dto.UserId,
            StatutCommandeId = dto.StatutCommandeId,
            FournisseurId = dto.FournisseurId,
            IsClient = dto.IsClient


        };

        return await _httpClientService.AddCommandeAsync(commande);
    }

    public async Task<bool> UpdateAsync(CommandeDTO dto)
    {
        Commande commande = new Commande()
        {
            Id = dto.Id,
            Date = dto.Date,
            UserId = dto.UserId,
            StatutCommandeId = dto.StatutCommandeId,
            FournisseurId = dto.FournisseurId,
            IsClient = dto.IsClient


        };

        return await _httpClientService.ModifyCommandeAsync(commande, dto.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _httpClientService.DeleteCommandeAsync(id);

    }

}
