﻿using MosaicoSolutions.ViaCep.Modelos;
using NUnit.Framework;

namespace ViaCepTest.MosaicoSolutions.ViaCep.Teste.Modelos
{
    [TestFixture]
    public class EnderecoRequisicaoTest
    {
        [Test]
        public void DeveSerUmaRequisicaoPorEnderecoInvalida()
        {
            //Cidade e Logradouro com menos de 3 caracteres e UF nula.
            var enderecoRequisicao = new EnderecoRequisicao
            {
                Logradouro = "As",
                Cidade = "Oi"
            };

            Assert.False(enderecoRequisicao.EhValido());

            //UF nula.
            enderecoRequisicao = new EnderecoRequisicao
            {
                Logradouro = "Ass",
                Cidade = "Oid"
            };

            Assert.False(enderecoRequisicao.EhValido());

            //Cidade com menos de 3 caracteres.
            enderecoRequisicao = new EnderecoRequisicao
            {
                UF = UF.AC,
                Logradouro = "Ass",
                Cidade = "Oi"
            };

            Assert.False(enderecoRequisicao.EhValido());

            //Logradouro com menos de 3 caracteres.
            enderecoRequisicao = new EnderecoRequisicao
            {
                UF = UF.AC,
                Logradouro = "As",
                Cidade = "Oii"
            };

            Assert.False(enderecoRequisicao.EhValido());
        }

        [Test]
        public void DeveSerUmaRequisicaoPorEnderecoValida()
        {
            var enderecoRequisicao = new EnderecoRequisicao
            {
                UF = UF.RS,
                Cidade = "Porto Alegre",
                Logradouro = "Olavo"
            };

            Assert.True(enderecoRequisicao.EhValido());
        }

        [Test]
        public void DeconstructEnderecoRequisicao()
        {
            var enderecoRequisicao = new EnderecoRequisicao
            {
                UF = UF.RS,
                Cidade = "Porto Alegre",
                Logradouro = "Olavo"
            };

            var (uf, cidade, logradouro) = enderecoRequisicao;

            Assert.AreEqual(uf, enderecoRequisicao.UF);
            Assert.AreEqual(cidade, enderecoRequisicao.Cidade);
            Assert.AreEqual(logradouro, enderecoRequisicao.Logradouro);
        }
    }
}