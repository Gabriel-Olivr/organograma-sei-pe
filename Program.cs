using System;
using System.IO;
using HtmlAgilityPack;
using Organograma_SEI_SEE.Models;

namespace Organograma_SEI_SEE
{
    class Program
    {
        static void Main(string[] args)
        {
            string pastaHtml = "html's";
            string htmlPath = Path.Combine(pastaHtml, "pagina_sei.html");
            
            if (!File.Exists(htmlPath))
            {
                Console.WriteLine($"ERRO: Arquivo '{htmlPath}' não encontrado.");
                return;
            }

            Console.WriteLine("Lendo o arquivo HTML local...");
            string htmlData = File.ReadAllText(htmlPath);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlData);

            var secretaria = new Setor { Nome = "SEE - Secretaria de Educação do Estado de Pernambuco" };
            var nosPrincipais = doc.DocumentNode.SelectNodes("//li[contains(@class, 'jstree-node')]");

            if (nosPrincipais != null)
            {
                foreach (var no in nosPrincipais) 
                {
                    HtmlParser.ProcessarNo(no, secretaria);
                }
            }

            Console.WriteLine("Gerando Tela de Organograma...");
            
            HtmlGenerator.GerarIndexLogin();
            HtmlGenerator.GerarOrganogramaHtml(secretaria);

            Console.WriteLine("Todas as etapas concluídas!");
        }
    }
}