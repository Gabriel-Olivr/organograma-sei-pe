using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Organograma_SEI_SEE
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class Setor
    {
        public string Nome { get; set; }
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Setor> SubSetores { get; set; } = new List<Setor>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string htmlPath = "pagina_sei.html";
            
            if (!File.Exists(htmlPath))
            {
                Console.WriteLine("ERRO: Arquivo 'pagina_sei.html' não encontrado na pasta.");
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
                    ProcessarNo(no, secretaria);
                }
            }
            else
            {
                Console.WriteLine("Tentando extração alternativa...");
                var links = doc.DocumentNode.SelectNodes("//a[contains(@class, 'jstree-anchor')]");
                if (links != null)
                {
                    var setorAtual = secretaria;
                    foreach (var a in links)
                    {
                        var innerHtml = a.InnerHtml;
                        string texto = a.InnerText.Trim();

                        if (innerHtml.Contains("fa-landmark"))
                        {
                            setorAtual = new Setor { Nome = texto };
                            secretaria.SubSetores.Add(setorAtual);
                        }
                        else if (innerHtml.Contains("fa-user"))
                        {
                            bool isAdmin = innerHtml.Contains("fa-user-cog") || innerHtml.Contains("admin") || innerHtml.Contains("adm");
                            setorAtual.Usuarios.Add(new Usuario { Nome = FormatarNome(texto), IsAdmin = isAdmin });
                        }
                    }
                }
            }

            Console.WriteLine("Gerando os sites com os nomes padronizados...");
            GerarIndexLogin();
            GerarOrganogramaHtml(secretaria);

            Console.WriteLine("SUCESSO! Os arquivos index.html e organograma.html foram gerados e padronizados.");
        }

        static void ProcessarNo(HtmlNode noLi, Setor setorPai)
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
                    {
                        ProcessarNo(childLi, novoSetor);
                    }
                }
            }
            else if (innerHtml.Contains("fa-user"))
            {
                bool isAdmin = innerHtml.Contains("fa-user-cog") || innerHtml.Contains("admin") || innerHtml.Contains("adm"); 
                // AQUI USAMOS A NOVA FUNÇÃO DE FORMATAR NOME
                setorPai.Usuarios.Add(new Usuario { Nome = FormatarNome(texto), IsAdmin = isAdmin });
            }
        }

        // --- NOVA FUNÇÃO QUE ARRUMA AS LETRAS MAIÚSCULAS ---
        static string FormatarNome(string textoOriginal)
        {
            if (string.IsNullOrWhiteSpace(textoOriginal)) return textoOriginal;

            // Separa o nome do login "(usuario.login)"
            int idxParenteses = textoOriginal.IndexOf('(');
            string nomeParte = textoOriginal;
            string loginParte = "";

            if (idxParenteses > 0)
            {
                nomeParte = textoOriginal.Substring(0, idxParenteses).Trim();
                loginParte = " " + textoOriginal.Substring(idxParenteses).ToLower(); // Garante que o login fique minúsculo
            }

            // Transforma o nome para Title Case (ex: ANDRÉA LUIZA -> Andréa Luiza)
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            string nomeFormatado = textInfo.ToTitleCase(nomeParte.ToLower());

            // Corrige as preposições que o Title Case deixa maiúsculas
            nomeFormatado = nomeFormatado.Replace(" Da ", " da ")
                                         .Replace(" De ", " de ")
                                         .Replace(" Di ", " di ")
                                         .Replace(" Do ", " do ")
                                         .Replace(" Dos ", " dos ")
                                         .Replace(" Das ", " das ");

            // Junta o nome arrumado com o login minúsculo
            return nomeFormatado + loginParte;
        }

        static void GerarIndexLogin()
        {
            string html = @"<!DOCTYPE html>
<html lang='pt-BR'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Login - Portal SEE/PE</title>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f0f2f5; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .login-container { background: white; padding: 40px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); width: 100%; max-width: 400px; text-align: center; }
        h2 { color: #0088cc; margin-bottom: 20px; }
        input { width: 90%; padding: 12px; margin: 10px 0; border: 1px solid #ccc; border-radius: 4px; font-size: 16px; box-sizing: border-box; }
        button { width: 100%; padding: 12px; background-color: #0088cc; color: white; border: none; border-radius: 4px; font-size: 16px; cursor: pointer; font-weight: bold; margin-top: 10px; }
        button:hover { background-color: #006699; }
        #erro-msg { color: red; margin-top: 10px; display: none; font-size: 14px; }
    </style>
</head>
<body>
    <div class='login-container'>
        <h2>Acesso Restrito - SEI</h2>
        <input type='email' id='email' placeholder='E-mail do Administrador' required>
        <input type='password' id='senha' placeholder='Senha' required>
        <button id='btn-login'>Entrar no Painel</button>
        <p id='erro-msg'>Usuário ou senha incorretos.</p>
    </div>

    <script type='module'>
        import { initializeApp } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-app.js';
        import { getAuth, signInWithEmailAndPassword } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-auth.js';

        const firebaseConfig = {
            apiKey: 'AIzaSyD7j4SZhtFBjPeAuFEMEK6fnzP9r_HTYT8',
            authDomain: 'organograma-see-pe.firebaseapp.com',
            projectId: 'organograma-see-pe',
            storageBucket: 'organograma-see-pe.firebasestorage.app',
            messagingSenderId: '471766110154',
            appId: '1:471766110154:web:a2154af1fb9096fd384933'
        };

        const app = initializeApp(firebaseConfig);
        const auth = getAuth(app);

        document.getElementById('btn-login').addEventListener('click', () => {
            const email = document.getElementById('email').value;
            const senha = document.getElementById('senha').value;
            const erroMsg = document.getElementById('erro-msg');

            signInWithEmailAndPassword(auth, email, senha)
                .then(() => { window.location.href = 'organograma.html'; })
                .catch((error) => {
                    erroMsg.style.display = 'block';
                    console.error(error.message);
                });
        });
    </script>
</body>
</html>";
            File.WriteAllText("index.html", html);
        }

        static void GerarOrganogramaHtml(Setor setorRaiz)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html lang='pt-BR'><head><meta charset='UTF-8'><title>Organograma SEI</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, sans-serif; background-color: #f4f7f6; padding: 20px; display: none; }");
            sb.AppendLine(".btn-sair { position: absolute; top: 20px; right: 20px; padding: 10px 20px; background: #ff4d4d; color: white; border: none; border-radius: 4px; cursor: pointer; font-weight: bold; }");
            sb.AppendLine(".tree ul { padding-top: 20px; position: relative; transition: all 0.5s; display: flex; justify-content: center; padding-left: 0; }");
            sb.AppendLine(".tree li { float: left; text-align: center; list-style-type: none; position: relative; padding: 20px 5px 0 5px; transition: all 0.5s; }");
            sb.AppendLine(".tree li::before, .tree li::after { content: ''; position: absolute; top: 0; right: 50%; border-top: 2px solid #ccc; width: 50%; height: 20px; }");
            sb.AppendLine(".tree li::after { right: auto; left: 50%; border-left: 2px solid #ccc; }");
            sb.AppendLine(".tree li:only-child::after, .tree li:only-child::before { display: none; }");
            sb.AppendLine(".tree li:only-child { padding-top: 0; }");
            sb.AppendLine(".tree li:first-child::before, .tree li:last-child::after { border: 0 none; }");
            sb.AppendLine(".tree li:last-child::before { border-right: 2px solid #ccc; border-radius: 0 5px 0 0; }");
            sb.AppendLine(".tree li:first-child::after { border-radius: 5px 0 0 0; }");
            sb.AppendLine(".tree ul ul::before { content: ''; position: absolute; top: 0; left: 50%; border-left: 2px solid #ccc; width: 0; height: 20px; }");
            sb.AppendLine(".sector-box { border: 2px solid #0088cc; padding: 10px 15px; color: #333; font-weight: bold; font-size: 12px; display: inline-block; border-radius: 5px; background-color: white; transition: all 0.3s; position: relative; box-shadow: 0 4px 6px rgba(0,0,0,0.1); cursor: pointer; max-width: 200px; word-wrap: break-word; }");
            sb.AppendLine(".sector-box:hover { background-color: #e6f7ff; border-color: #005580; transform: scale(1.05); z-index: 10; }");
            sb.AppendLine(".tooltip-content { visibility: hidden; width: 300px; background-color: #2c3e50; color: #fff; text-align: left; border-radius: 6px; padding: 10px; position: absolute; z-index: 100; bottom: 125%; left: 50%; margin-left: -150px; opacity: 0; transition: opacity 0.3s; box-shadow: 0px 8px 16px rgba(0,0,0,0.3); max-height: 250px; overflow-y: auto; font-weight: normal; font-size: 11px; }");
            sb.AppendLine(".tooltip-content::after { content: ''; position: absolute; top: 100%; left: 50%; margin-left: -5px; border-width: 5px; border-style: solid; border-color: #2c3e50 transparent transparent transparent; }");
            sb.AppendLine(".sector-box:hover .tooltip-content { visibility: visible; opacity: 1; }");
            sb.AppendLine(".user-list { list-style-type: none; padding: 0; margin: 0; }");
            sb.AppendLine(".user-item { padding: 4px 0; border-bottom: 1px solid #4a6278; }");
            sb.AppendLine(".user-item:last-child { border-bottom: none; }");
            sb.AppendLine(".admin-badge { background-color: #ff4d4d; color: white; padding: 2px 4px; border-radius: 3px; font-size: 9px; margin-left: 5px; font-weight: bold; }");
            sb.AppendLine("</style></head><body>");

            sb.AppendLine("<button id='btn-sair' class='btn-sair'>Sair do Sistema</button>");
            sb.AppendLine("<h1 style='text-align:center; color:#0088cc;'>Estrutura Organizacional SEE-PE</h1>");
            sb.AppendLine("<p style='text-align:center; color:#555;'>Passe o mouse sobre as caixas para visualizar as lotações.</p>");
            sb.AppendLine("<div class='tree'><ul>");
            
            RenderizarSetorHtml(setorRaiz, sb);
            
            sb.AppendLine("</ul></div>");

            sb.AppendLine(@"<script type='module'>
        import { initializeApp } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-app.js';
        import { getAuth, onAuthStateChanged, signOut } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-auth.js';

        const firebaseConfig = {
            apiKey: 'AIzaSyD7j4SZhtFBjPeAuFEMEK6fnzP9r_HTYT8',
            authDomain: 'organograma-see-pe.firebaseapp.com',
            projectId: 'organograma-see-pe',
            storageBucket: 'organograma-see-pe.firebasestorage.app',
            messagingSenderId: '471766110154',
            appId: '1:471766110154:web:a2154af1fb9096fd384933'
        };

        const app = initializeApp(firebaseConfig);
        const auth = getAuth(app);

        onAuthStateChanged(auth, (user) => {
            if (user) { document.body.style.display = 'block'; } 
            else { window.location.replace('index.html'); }
        });

        document.getElementById('btn-sair').addEventListener('click', () => {
            signOut(auth).then(() => { window.location.replace('index.html'); });
        });
    </script>");
            sb.AppendLine("</body></html>");

            File.WriteAllText("organograma.html", sb.ToString());
        }

        static void RenderizarSetorHtml(Setor setor, StringBuilder html)
        {
            html.AppendLine("<li><div class='sector-box'>");
            html.AppendLine($"<span>{setor.Nome}</span>");
            
            if (setor.Usuarios.Any())
            {
                html.AppendLine("<div class='tooltip-content'>");
                html.AppendLine($"<div style='margin-bottom:5px; font-weight:bold; border-bottom:1px solid #fff; padding-bottom:3px;'>Total: {setor.Usuarios.Count} pessoa(s)</div>");
                html.AppendLine("<ul class='user-list'>");
                foreach (var user in setor.Usuarios.OrderByDescending(u => u.IsAdmin).ThenBy(u => u.Nome))
                {
                    string badge = user.IsAdmin ? "<span class='admin-badge'>ADM</span>" : "";
                    html.AppendLine($"<li class='user-item'>{user.Nome} {badge}</li>");
                }
                html.AppendLine("</ul></div>");
            }
            
            html.AppendLine("</div>");

            if (setor.SubSetores.Any())
            {
                html.AppendLine("<ul>");
                foreach (var subSetor in setor.SubSetores)
                {
                    RenderizarSetorHtml(subSetor, html);
                }
                html.AppendLine("</ul>");
            }
            html.AppendLine("</li>");
        }
    }
}