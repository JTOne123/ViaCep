﻿using System.Linq;
using MosaicoSolutions.ViaCep.Modelos;
using NUnit.Framework;

namespace ViaCepTest.MosaicoSolutions.ViaCep.Teste.Modelos
{
    [TestFixture]
    public class UFTest
    {
        [Test]
        public void UFsDevemSerIguais()
        {
            var uf = UF.PE;
            var uf2 = UF.PE;

            Assert.AreEqual(uf, uf2);
            Assert.True(uf.Equals(uf2));
        }

        [Test]
        public void NaoDeveEncontrarUFPeloCodigo()
            => Assert.Throws<UFInexistenteException>(() => UF.PeloCodigo(100));

        [Test]
        public void NaoDeveEncontrarUFPelaSigla()
            => Assert.Throws<UFInexistenteException>(() => UF.PelaSigla("LOL"));

        [Test]
        public void DeveEncontrarUFPeloCodigo() => Assert.DoesNotThrow(() => UF.PeloCodigo(26));

        [Test]
        public void DeveEncontrarUFPelaSigla()  => Assert.DoesNotThrow(() => UF.PelaSigla("PE"));

        [Test]
        public void ComparaPelaSigla()
        {
            var pernambuco = UF.PE;
            var sergipe = UF.SE;
            var ceara = UF.CE;

            var comparer = ComparadoresUF.PelaSigla;

            Assert.AreEqual(comparer(pernambuco, sergipe), -1);
            Assert.AreEqual(comparer(pernambuco, pernambuco), 0);
            Assert.AreEqual(comparer(pernambuco, ceara), 1);
            Assert.AreEqual(comparer(pernambuco, null), 1);
        }
        
        [Test]
        public void ComparaPeloNomeDoEstado()
        {
            var pernambuco = UF.PE;
            var matoGrosso = UF.MT;
            var matoGrossoDoSul = UF.MS;

            var comparer = ComparadoresUF.PeloNomeDoEstado;

            Assert.AreEqual(comparer(pernambuco, matoGrosso), 1);
            Assert.AreEqual(comparer(pernambuco, pernambuco), 0);
            Assert.AreEqual(comparer(matoGrossoDoSul, matoGrosso), 1);
            Assert.AreEqual(comparer(null, matoGrosso), -1);
        }

        [Test]
        public void ComparaPeloCodigo()
        {
            var pernambuco = UF.PE;
            var maranhao = UF.MA;
            var alagoas = UF.AL;

            var comparer = ComparadoresUF.PeloCodigo;
            
            Assert.AreEqual(comparer(pernambuco, maranhao), 1);
            Assert.AreEqual(comparer(pernambuco, pernambuco), 0);
            Assert.AreEqual(comparer(alagoas, pernambuco), 1);
            Assert.AreEqual(comparer(alagoas, null), 1);
        }

        [Test]
        public void DeveRetornarTodasAsUFs()
        {
            var ufs = UF.Todas;
            
            Assert.NotNull(ufs);
            Assert.AreEqual(ufs.Count(), 27);
        }

        [Test]
        public void TestandoOPredicado()
        {
            var ufsComNomeDoEstadoComecandoComALetraP = UF.Onde(uf => uf.NomeEstado.ToLower().StartsWith("p"));
            
            Assert.AreEqual(ufsComNomeDoEstadoComecandoComALetraP.Count(), 5);

            var ufsComNomeDoEstadoMenorDoQue5Caracteres = UF.Onde(uf => uf.NomeEstado.Length < 5);
            
            Assert.AreEqual(ufsComNomeDoEstadoMenorDoQue5Caracteres.Count(), 2);
        }

        [Test]
        public void TestandoOMapeamento()
        {
            var nomeESiglaDosEstados = UF.Map(uf => new {Nome = uf.NomeEstado, uf.Sigla});
            
            Assert.AreEqual(nomeESiglaDosEstados.Count(), UF.Todas.Count());
            
            Assert.AreEqual(nomeESiglaDosEstados.First(uf => uf.Sigla == "PE").Sigla, UF.PelaSigla("PE").Sigla);
        }
    }
}