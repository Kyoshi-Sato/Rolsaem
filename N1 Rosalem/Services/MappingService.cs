using BarApp.Models;
using N1_Rosalem.Models;

namespace N1_Rosalem.Services
{
    public class MappingService
    {
        public Receita MapToReceitaViewModel(Receita receita)
        {
            return new Receita
            {
                id = receita.id,
                receita = receita.receita,
                Bebidas = receita.Bebidas?.Select(b => MapToBebidaViewModel(b)).ToList()
            };
        }

        public Origem MapToOrigemViewModel(Origem origem)
        {
            return new Origem
            {
                id = origem.id,
                origem = origem.origem,
                Bebidas = origem.Bebidas?.Select(b => MapToBebidaViewModel(b)).ToList()
            };
        }

        public Bebida MapToBebidaViewModel(Bebida bebida)
        {
            if (bebida == null)
            {
                return null;
            }

            return new Bebida
            {
                Id = bebida.Id,
                Nome = bebida.Nome,
                Preco = bebida.Preco,
                Estoque = bebida.Estoque,
                Descricao = bebida.Descricao,
                ImagemURL = bebida.ImagemURL,
                Origem = bebida.Origem != null ? new Origem { id = bebida.Origem.id, origem = bebida.Origem.origem } : null,
                Receita = bebida.Receita != null ? new Receita { id = bebida.Receita.id, receita = bebida.Receita.receita } : null
            };
        }
    }
}
