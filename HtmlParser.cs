using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Organograma_SEI_SEE.Models;

namespace Organograma_SEI_SEE
{
    public static class HtmlParser
    {
        public static void ProcessarNo(HtmlNode noLi, Setor setorPai)
        {
            var aTag = noLi.SelectSingleNode("./a");
            if (aTag == null) return;

            string innerHtml = aTag.InnerHtml;
            string texto = aTag.InnerText.Trim();

            if (innerHtml.Contains("fa-landmark"))
            {
                var novoSetor = new Setor { Nome = texto };
                setorPai.SubSetores.Add(novoSetor);
                var ulNode = noLi.SelectSingleNode("./ul");
                if (ulNode != null)
                {
                    foreach (var childLi in ulNode.SelectNodes("./li")) 
                        ProcessarNo(childLi, novoSetor);
                }
            }
            else if (innerHtml.Contains("fa-user"))
            {
                bool isAdmin = innerHtml.Contains("fa-user-cog") || innerHtml.Contains("admin") || innerHtml.Contains("adm"); 
                string login = ExtrairLogin(texto);
                string nomeLimpo = FormatarNome(texto);

                if (!setorPai.Usuarios.Any(u => u.Login == login))
                {
                    setorPai.Usuarios.Add(new Usuario { NomeFormatado = nomeLimpo, Login = login, IsAdmin = isAdmin });
                }
            }
        }

        private static string ExtrairLogin(string textoOriginal)
        {
            int idxInicio = textoOriginal.IndexOf('(');
            int idxFim = textoOriginal.IndexOf(')');
            if (idxInicio > 0 && idxFim > idxInicio) 
                return textoOriginal.Substring(idxInicio + 1, idxFim - idxInicio - 1).ToLower().Trim();
            
            return ""; 
        }

        private static string FormatarNome(string textoOriginal)
        {
            if (string.IsNullOrWhiteSpace(textoOriginal)) return textoOriginal;
            int idxParenteses = textoOriginal.IndexOf('(');
            string nomeParte = idxParenteses > 0 ? textoOriginal.Substring(0, idxParenteses).Trim() : textoOriginal.Trim();
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            string nomeFormatado = textInfo.ToTitleCase(nomeParte.ToLower());
            return nomeFormatado.Replace(" Da ", " da ").Replace(" De ", " de ").Replace(" Di ", " di ").Replace(" Do ", " do ").Replace(" Dos ", " dos ").Replace(" Das ", " das ");
        }
    }
}