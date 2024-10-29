using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoHorario(IRepositorioHorario repositorio, IMemoryCache memoryCache) : IServicoHorario
    {
        private readonly IRepositorioHorario _repositorio = repositorio;
        private readonly IMemoryCache _memoryCache = memoryCache;


        public async Task<Response<HorarioFuncionamento?>> Atualizar(AtualizarHorarioRequest request)
        {
            var horario = await _repositorio.Buscar(request.IdDiaSemana) ?? throw new NotFoundException("Dia de funcionamento nao encontrado");

            horario.HoraAbertura = TimeSpan.Parse(request.HoraAbertura);
            horario.HoraFechamento = TimeSpan.Parse(request.HoraFechamento);
            horario.Funcionando = request.Funcionando;

            var result = await _repositorio.Atualizar(horario);

            if (!result)
            {
                return new Response<HorarioFuncionamento?>(null, 500, "Nao foi possivel atualizar o funcionamento do restaurante");
            }

            _memoryCache.Remove("cachehorarios");

            return new Response<HorarioFuncionamento?>(null, 200, "Funcionamento do restaurante foi atualizado com sucesso");

        }

        public async Task<Response<HorarioFuncionamento?>> Buscar(BuscarHorarioRequest request)
        {
            var horario = await _repositorio.Buscar(request.Dia) ?? throw new NotFoundException("Dia de funcionamento nao encontrado");
            return new Response<HorarioFuncionamento?>(horario);
        }

        public async Task<Response<List<HorarioFuncionamento>>> BuscarTodos()
        {
            var cacheHorarios = await _memoryCache.GetOrCreateAsync("cachehorarios", async entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(240);
                var horarios = await _repositorio.BuscarTodos();
                return horarios;
            });


            return new Response<List<HorarioFuncionamento>>(cacheHorarios);
        }
    }
}
