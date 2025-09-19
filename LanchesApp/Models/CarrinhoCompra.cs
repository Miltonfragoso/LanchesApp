using LanchesApp.Context;
using LanchesApp.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanchesApp.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _contexto;

        public CarrinhoCompra(AppDbContext contexto)
        {
           _contexto = contexto;
        }


        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraIntem> CarrinhoCompraIntems { get; set; }



        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão acessando o contexto atual(tem que registrar em IServices)
            //operador condicional nullo retorna uma sessão se o HttpContextAccessor não for nulo  
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto 
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            //operador de coalescência nula (??) se o valor da esquerda for nulo, atribui o valor da direita
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }


        public void AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            //obttem o lanche do carrinho 
            var carrinhoCompraItem = _contexto.CarrinhoCompraIntems.SingleOrDefault(
                        s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            //se o carrinho for null cria um carrinho novo
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraIntem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _contexto.CarrinhoCompraIntems.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _contexto.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                    _contexto.CarrinhoCompraIntems.SingleOrDefault(
                        s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _contexto.CarrinhoCompraIntems.Remove(carrinhoCompraItem);
                }
            }

            _contexto.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraIntem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraIntems ?? (CarrinhoCompraIntems = _contexto.CarrinhoCompraIntems
                                           .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                           .Include(s => s.Lanche)
                                           .ToList());
                           
        }


        public void LimparCarrinho()
        {
            var carrinhoItens = _contexto.CarrinhoCompraIntems
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _contexto.CarrinhoCompraIntems.RemoveRange(carrinhoItens);

            _contexto.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _contexto.CarrinhoCompraIntems.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }
    }
}
