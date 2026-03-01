using System;
using System.IO;
using System.Linq;
using System.Text;
using Organograma_SEI_SEE.Models;

#region HTML
namespace Organograma_SEI_SEE
{
    public static class HtmlGenerator
    {
        private const string PastaHtml = "html's";

        public static void GerarIndexLogin()
        {
            GarantirPastaExiste();

            string iconEyeOpen = "<svg viewBox='0 0 24 24' width='18' height='18' stroke='currentColor' stroke-width='2' fill='none'><path d='M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z'></path><circle cx='12' cy='12' r='3'></circle></svg>";

            string html = @"<!DOCTYPE html>
<html lang='pt-BR'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Acesso Restrito - SEE/PE</title>
    <style>
        html, body { margin: 0; padding: 0; width: 100%; height: 100vh; overflow: hidden; font-family: 'Segoe UI', Tahoma, sans-serif; background-color: #e9ecef; }
        .split-layout { display: flex; height: 100vh; width: 100vw; overflow: hidden; }
        .left-panel { flex: 7; position: relative; overflow: hidden; background: #000; display: flex; align-items: flex-end; }
        .carousel { position: absolute; top: 0; left: 0; width: 100%; height: 100%; z-index: 1; background-color: #000; } 
        
        .carousel img { 
            position: absolute; 
            width: 100%; 
            height: 100%; 
            object-fit: cover; 
            object-position: center center; 
            opacity: 0; 
            animation: fade 35s infinite; 
            image-rendering: -webkit-optimize-contrast; 
            image-rendering: crisp-edges;
        }

        .carousel img:nth-child(1) { animation-delay: 0s; }
        .carousel img:nth-child(2) { animation-delay: 5s; }
        .carousel img:nth-child(3) { animation-delay: 10s; }
        .carousel img:nth-child(4) { animation-delay: 15s; }
        .carousel img:nth-child(5) { animation-delay: 20s; }
        .carousel img:nth-child(6) { animation-delay: 25s; }
        .carousel img:nth-child(7) { animation-delay: 30s; }
        
        @keyframes fade {
            0% { opacity: 0; }
            5% { opacity: 1; }
            14.28% { opacity: 1; }
            19.28% { opacity: 0; }
            100% { opacity: 0; }
        }

        .overlay-gradient { position: absolute; bottom: 0; left: 0; width: 100%; height: 30%; background: linear-gradient(to top, rgba(0,0,0,0.7), transparent); z-index: 2; pointer-events: none; }
        .footer-text { position: relative; z-index: 3; color: white; font-size: 8.5px; padding: 15px 20px; line-height: 1; font-weight: 500; text-shadow: 1px 1px 2px rgba(0,0,0,0.8); user-select: none; white-space: nowrap; width: 100%; text-align: center;}

        .right-panel { flex: 3; background-color: #fff; display: flex; flex-direction: column; align-items: center; justify-content: flex-start; min-width: 420px; box-shadow: -5px 0 20px rgba(0,0,0,0.1); z-index: 10; overflow-y: auto; overflow-x: hidden; padding: 20px 0; border-left: 1px solid #ddd; user-select: none; cursor: default;}
        
        .header-logos { width: 100%; display: flex; flex-direction: column; align-items: center; justify-content: center; gap: 10px; margin-bottom: 30px; pointer-events: none; }
        .umct-logo-container { width: 95%; display: flex; align-items: center; justify-content: center; overflow: visible; padding: 10px 0; }
        .umct-logo-img { width: 100%; height: auto; max-height: 400px; object-fit: contain; }
        .see-logo { height: 75px; max-width: 85%; object-fit: contain; margin-top: 10px; }

        .login-box { background: #f8f9fa; padding: 40px; border-radius: 12px; width: 80%; max-width: 320px; border: 1px solid #eee; box-shadow: 0 4px 15px rgba(0,0,0,0.05); text-align: center; }
        
        .input-group { margin-bottom: 20px; text-align: left; position: relative; }
        .input-group label { display: block; color: #555; font-size: 11px; font-weight: bold; margin-bottom: 8px; letter-spacing: 1px; cursor: default; }
        .input-group input { width: 100%; padding: 12px 15px; border: 1px solid #ccc; border-radius: 6px; box-sizing: border-box; font-size: 14px; background: white; color: #333; outline: none; transition: 0.2s; user-select: text; cursor: text !important; }
        .input-group input:focus { border-color: #0088cc; box-shadow: 0 0 0 3px rgba(0,136,204,0.1); }
        
        .pass-wrapper .eye-icon { position: absolute; right: 12px; top: 32px; cursor: pointer !important; color: #999; display: flex; }

        button { width: 100%; padding: 14px; background-color: #0088cc; color: white; border: none; border-radius: 6px; font-size: 15px; cursor: pointer !important; font-weight: bold; margin-top: 10px; transition: 0.3s; }
        button:hover { background-color: #005580; transform: translateY(-1px); }
        
        #erro-msg { color: #d9534f; margin-top: 15px; display: none; font-size: 13px; font-weight: bold; }
        .forgot-pass { margin-top: 30px; color: #0088cc; text-decoration: none; font-size: 13px; font-weight: 500; transition: 0.2s; cursor: pointer !important; }
        .forgot-pass:hover { text-decoration: underline; color: #005580; }

        @media (max-width: 900px) {
            .left-panel { display: none; }
            .right-panel { width: 100%; min-width: 100%; background-color: #e9ecef; }
            .login-box { background: white; }
        }
    </style>
</head>
<body>
    <div class='split-layout'>
        <div class='left-panel'>
            <div class='carousel'>
                <img src='https://wallpapercave.com/wp/wp4201716.jpg' alt='Imagem 1'>
                <img src='https://www.evoyconsorcios.com.br/images/uploads/posts/shutterstock-1115757746.jpg' alt='Imagem 2'>
                <img src='https://c1.wallpaperflare.com/preview/534/434/431/hat-culture-brazil-northeastern.jpg' alt='Imagem 3'>
                <img src='https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Pinturas_Rupestres_-_Parque_Nacional_do_Catimbau_-_Pernambuco_-_Brasil.jpg/960px-Pinturas_Rupestres_-_Parque_Nacional_do_Catimbau_-_Pernambuco_-_Brasil.jpg?_=20120124171925' alt='Imagem 4'> 
                <img src='https://p2.piqsels.com/preview/978/159/742/ground-zero-recife-pernambuco-ancient-reef.jpg' alt='Imagem 5'>
                <img src='https://cdn.folhape.com.br/img/pc/1100/1/dn_arquivo/2022/06/fotografia-paulo-romao1.jpg' alt='Imagem 6'>
                <img src='https://www.bwallpaperhd.com/wp-content/uploads/2018/06/CaruaruClayDolls.jpg' alt='Imagem 7'>
            </div>
            <div class='overlay-gradient'></div>
            <div class='footer-text'>As informações pertencem às suas fontes originais. A arquitetura, metodologia e design desta plataforma são propriedade intelectual de Gabriel Estevam, protegidos pela Lei de Direitos Autorais (Lei 9.610/98). © 2026, Gabriel Estevam. Todos os direitos reservados.</div>
        </div>

        <div class='right-panel'>
            <div class='header-logos'>
                <div class='umct-logo-container'>
                    <img src='Nova Logo_UMCT.png' alt='Logo UMCT' class='umct-logo-img'>
                </div>
                <img src='https://portal.educacao.pe.gov.br/wp-content/uploads/2025/11/logo_Secretaria-de-Educacao_CMYK_para-fundo-branco-2-scaled.png' alt='Logo SEE PE' class='see-logo'>
            </div>

            <div class='login-box'>
                <div class='input-group'>
                    <label>E-MAIL INSTITUCIONAL</label>
                    <input type='email' id='email' placeholder='usuário@educacao.pe.gov.br' autocomplete='email'>
                </div>
                
                <div class='input-group pass-wrapper'>
                    <label>SENHA DE ACESSO</label>
                    <input type='password' id='senha' placeholder='••••••••'>
                    <span class='eye-icon' id='toggle-eye'>" + iconEyeOpen + @"</span>
                </div>
                
                <button id='btn-login'>LOGIN</button>
                <p id='erro-msg'>Usuário ou senha incorretos.</p>
            </div>

            <a href='https://wa.me/+5581900000000?text=Olá Gabriel, tudo bem? Esqueci minha senha, pode me ajudar?' target='_blank' class='forgot-pass'>Esqueceu sua senha? Solicite ajuda.</a>
        </div>
    </div>

    <script type='module'>
        import { initializeApp } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-app.js';
        import { getAuth, signInWithEmailAndPassword } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-auth.js';

        const firebaseConfig = {
            apiKey: atob('QUl6YVN5RDdqNFNaaHRGQmpQZUF1RkVNRUs2Zm56UDlyX0hUWVQ4'),
            authDomain: 'organograma-see-pe.firebaseapp.com',
            projectId: 'organograma-see-pe',
            storageBucket: 'organograma-see-pe.firebasestorage.app',
            messagingSenderId: '471766110154',
            appId: atob('MTo0NzE3NjYxMTAxNTQ6d2ViOmEyMTU0YWYxZmI5MDk2ZmQzODQ5MzM=')
        };

        const app = initializeApp(firebaseConfig);
        const auth = getAuth(app);

        const eyeIcon = document.getElementById('toggle-eye');
        const senhaInput = document.getElementById('senha');
        
        eyeIcon.addEventListener('click', () => {
            if(senhaInput.type === 'password') {
                senhaInput.type = 'text';
                eyeIcon.innerHTML = `<svg viewBox='0 0 24 24' width='18' height='18' stroke='currentColor' stroke-width='2' fill='none'><path d='M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24'></path><line x1='1' y1='1' x2='23' y2='23'></line></svg>`;
            } else {
                senhaInput.type = 'password';
                eyeIcon.innerHTML = `"+ iconEyeOpen + @"`;
            }
        });

        function fazerLogin() {
            const email = document.getElementById('email').value.trim();
            const senha = senhaInput.value;
            if(!email || !senha) return;

            const btn = document.getElementById('btn-login');
            btn.innerText = 'AUTENTICANDO...';
            btn.disabled = true;

            signInWithEmailAndPassword(auth, email, senha)
                .then(() => { window.location.href = 'organograma.html'; })
                .catch((error) => {
                    btn.innerText = 'LOGIN';
                    btn.disabled = false;
                    document.getElementById('erro-msg').style.display = 'block';
                });
        }

        document.getElementById('btn-login').addEventListener('click', fazerLogin);
        document.addEventListener('keydown', (e) => { if (e.key === 'Enter') fazerLogin(); });
    </script>
</body>
</html>";
            
            string caminhoArquivo = Path.Combine(PastaHtml, "index.html");
            File.WriteAllText(caminhoArquivo, html);
        }

        public static void GerarOrganogramaHtml(Setor setorRaiz)
        {
            GarantirPastaExiste();

            string iconGear = "<img src='https://cdn-icons-png.flaticon.com/512/5046/5046461.png' width='16' height='16' alt='' title='' class='img-small'>";
            string iconLogout = "<svg viewBox='0 0 24 24' width='16' height='16' stroke='currentColor' stroke-width='2' fill='none'><path d='M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4'></path><polyline points='16 17 21 12 16 7'></polyline><line x1='21' y1='12' x2='9' y2='12'></line></svg>";
            string iconGithub = "<img src='https://cdn-icons-png.flaticon.com/512/733/733609.png' width='16' height='16' alt='GitHub' title='GitHub' class='img-small'>";            string iconLinkedin = "<img src='https://cdn-icons-png.flaticon.com/512/1384/1384072.png' width='16' height='16' alt='LinkedIn' title='LinkedIn' class='img-small'>";            string iconResetButton = "<svg viewBox='0 0 24 24' width='16' height='16' stroke='currentColor' stroke-width='2' fill='none' stroke-linecap='round' stroke-linejoin='round'><path d='M3 12a9 9 0 1 0 9-9 9.75 9.75 0 0 0-6.74 2.74L3 8'></path><polyline points='3 3 3 8 8 8'></polyline></svg>";
            string iconEyeOpen = "<svg viewBox='0 0 24 24' width='16' height='16' stroke='currentColor' stroke-width='2' fill='none'><path d='M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z'></path><circle cx='12' cy='12' r='3'></circle></svg>";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html lang='pt-BR'><head><meta charset='UTF-8'><title>Organograma SEI</title>");
            sb.AppendLine("<link href='https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.css' rel='stylesheet'>");
            sb.AppendLine("<script src='https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.js'></script>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, sans-serif; background-color: #e9ecef; margin: 0; padding: 0; overflow: hidden; display: none; }");
            sb.AppendLine(".top-bar { position: fixed; top: 0; left: 0; width: 100%; background: #ffffff; box-shadow: 0 2px 10px rgba(0,0,0,0.1); padding: 10px 20px; z-index: 1000; display: flex; justify-content: space-between; align-items: center; box-sizing: border-box; }");
            sb.AppendLine(".top-left { display: flex; align-items: center; gap: 20px; }");
            sb.AppendLine(".top-left h1 { margin: 0; font-size: 20px; color: #005580; user-select: none; -webkit-user-select: none; }");
            sb.AppendLine(".user-profile-container { position: relative; user-select: none; -webkit-user-select: none; }");
            sb.AppendLine(".user-profile { display: flex; align-items: center; gap: 10px; cursor: pointer !important; padding: 5px 10px; border-radius: 8px; transition: 0.2s; border-left: 2px solid #eee; margin-left: 10px; }");
            sb.AppendLine(".user-profile *, .user-profile span, .user-profile strong { cursor: pointer !important; user-select: none !important; -webkit-user-select: none !important; }"); 
            sb.AppendLine(".user-profile:hover { background: #f0f2f5; }");
            sb.AppendLine(".avatar { width: 38px; height: 38px; background: #0088cc; color: white; border-radius: 50%; display: flex; align-items: center; justify-content: center; font-weight: bold; font-size: 16px; overflow: hidden; pointer-events: none; }");
            sb.AppendLine(".avatar img { width: 100%; height: 100%; object-fit: cover; }");
            sb.AppendLine(".dropdown-menu { position: absolute; top: 110%; right: 0; background: white; border: 1px solid #ccc; border-radius: 6px; box-shadow: 0 4px 12px rgba(0,0,0,0.15); width: 220px; display: none; flex-direction: column; overflow: hidden; z-index: 2000; user-select: none; -webkit-user-select: none; }");
            sb.AppendLine(".dropdown-menu.show { display: flex; }");
            sb.AppendLine(".dropdown-header { padding: 12px 15px; border-bottom: 1px solid #eee; font-size: 13px; color: #555; cursor: default !important; }");
            sb.AppendLine(".dropdown-header * { cursor: default !important; }");
            sb.AppendLine(".dropdown-item { padding: 10px 15px; font-size: 14px; color: #333; text-decoration: none; display: flex; align-items: center; gap: 8px; cursor: pointer; transition: background 0.2s; }");
            sb.AppendLine(".dropdown-item:hover { background: #0088cc; color: white; }");
            sb.AppendLine(".search-wrapper { position: relative; display: flex; align-items: center; }");
            sb.AppendLine(".search-box-input { padding: 9px 35px 9px 12px; border: 1px solid #ccc; border-radius: 20px; width: 350px; font-size: 14px; outline: none; transition: all 0.3s; background-color: #f8f9fa; user-select: text !important; cursor: text !important; }");
            sb.AppendLine(".search-box-input:focus { border-color: #0088cc; background-color: #fff; box-shadow: 0 0 5px rgba(0,136,204,0.1); }");
            sb.AppendLine(".search-icon { position: absolute; right: 12px; color: #999; width: 18px; height: 18px; cursor: pointer; transition: color 0.2s; pointer-events: auto; }");
            sb.AppendLine(".search-icon:hover { color: #0088cc; }");
            sb.AppendLine(".autocomplete-list { position: absolute; top: 100%; left: 0; width: 100%; background: white; border: 1px solid #ccc; border-radius: 8px; max-height: 300px; overflow-y: auto; box-shadow: 0 4px 10px rgba(0,0,0,0.15); z-index: 1002; display: none; margin-top: 5px; padding: 5px 0; }");
            sb.AppendLine(".autocomplete-item { padding: 8px 15px; font-size: 13px; color: #333; cursor: pointer; display: flex; align-items: center; gap: 8px; }");
            sb.AppendLine(".autocomplete-item.autocomplete-active, .autocomplete-item:hover { background: #e6f7ff; color: #005580; font-weight: bold; }");
            sb.AppendLine(".sidebar { position: fixed; right: -400px; top: 62px; width: 400px; height: calc(100vh - 62px); background: #fdfdfd; box-shadow: -4px 0 15px rgba(0,0,0,0.1); transition: right 0.4s ease; z-index: 1001; overflow-y: auto; box-sizing: border-box; border-left: 1px solid #ddd; }");
            sb.AppendLine(".sidebar.open { right: 0; }");
            sb.AppendLine(".sidebar-header { position: sticky; top: 0; background: #fdfdfd; display: flex; justify-content: space-between; align-items: center; border-bottom: 2px solid #0088cc; padding: 20px 20px 10px 20px; z-index: 10; margin-bottom: 15px; }");
            sb.AppendLine(".sidebar-header h2 { margin: 0; font-size: 18px; color: #333; }");
            sb.AppendLine(".btn-close-sidebar { background: none; border: none; font-size: 24px; cursor: pointer; color: #777; font-weight: bold; line-height: 1; padding: 0; }");
            sb.AppendLine(".btn-close-sidebar:hover { color: #ff4d4d; }");
            sb.AppendLine(".sidebar-results { padding: 0 20px 20px 20px; }");
            sb.AppendLine(".result-item { background: #fff; border: 1px solid #eee; padding: 12px; margin-bottom: 10px; border-radius: 6px; box-shadow: 0 2px 4px rgba(0,0,0,0.05); font-size: 13px; line-height: 1.5; }");
            sb.AppendLine(".result-item button { margin-top: 8px; width: 100%; padding: 6px; background: #0088cc; color: #fff; border: none; border-radius: 4px; cursor: pointer; font-weight: bold; transition: 0.2s; }");
            sb.AppendLine(".result-item button:hover { background: #005580; }");
            sb.AppendLine(".modal-overlay { position: fixed; top: 0; left: 0; width: 100vw; height: 100vh; background: rgba(0,0,0,0.6); z-index: 3000; display: none; justify-content: center; align-items: center; }");
            sb.AppendLine(".modal-content { background: white; padding: 25px; border-radius: 12px; width: 400px; max-height: 90vh; overflow-y: auto; box-shadow: 0 10px 30px rgba(0,0,0,0.3); position: relative; }");
            sb.AppendLine(".form-group { margin-bottom: 15px; }");
            sb.AppendLine(".form-group label { display: block; font-size: 12px; color: #555; margin-bottom: 5px; font-weight: bold; }");
            sb.AppendLine(".pass-wrapper { position: relative; display: flex; align-items: center; }");
            sb.AppendLine(".pass-wrapper input { width: 100%; padding: 8px; padding-right: 35px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box; font-size: 13px; }");
            sb.AppendLine(".eye-icon { position: absolute; right: 10px; cursor: pointer; color: #888; display: flex; }");
            sb.AppendLine(".eye-icon:hover { color: #0088cc; }");
            sb.AppendLine(".form-group input[type='text'], .form-group input[type='file'] { width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box; font-size: 13px; }");
            sb.AppendLine(".crop-container { width: 100%; max-height: 250px; margin-top: 10px; display: none; background: #eee; border-radius: 8px; overflow: hidden; }");
            sb.AppendLine(".btn-save-settings { width: 100%; padding: 10px; background: #0088cc; color: white; border: none; border-radius: 4px; font-weight: bold; cursor: pointer; margin-top: 10px; font-size: 14px; }");
            sb.AppendLine(".watermark { position: fixed; bottom: 15px; right: 20px; font-size: 12px; color: #666; z-index: 1000; background: rgba(255,255,255,0.9); padding: 8px 15px; border-radius: 20px; box-shadow: 0 2px 8px rgba(0,0,0,0.15); border: 1px solid #ddd; transition: all 0.3s ease; overflow: hidden; height: 18px; display: flex; flex-direction: column; pointer-events: auto; cursor: default; }");
            sb.AppendLine(".watermark:hover { height: 75px; border-color: #0088cc; }");
            sb.AppendLine(".watermark-links { margin-top: 12px; display: flex; flex-direction: column; gap: 8px; opacity: 0; transition: opacity 0.3s; }");
            sb.AppendLine(".watermark:hover .watermark-links { opacity: 1; }");
            sb.AppendLine(".watermark-links a { text-decoration: none; color: #333; font-weight: bold; display: flex; align-items: center; gap: 5px; }");
            sb.AppendLine(".zoom-controls { position: fixed; bottom: 30px; left: 30px; display: flex; flex-direction: column; gap: 10px; z-index: 1000; background: white; padding: 10px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.15); }");
            sb.AppendLine(".zoom-controls button { width: 40px; height: 40px; background: #f0f2f5; border: 1px solid #ccc; border-radius: 4px; cursor: pointer; font-size: 18px; font-weight: bold; color: #333; display: flex; align-items: center; justify-content: center; padding: 0; transition: 0.2s; }");
            sb.AppendLine(".zoom-controls button:hover { background: #e0e4e8; }");
            sb.AppendLine("#map-container { position: absolute; top: 60px; left: 0; width: 100vw; height: calc(100vh - 60px); overflow: auto; cursor: grab; background-color: #f4f7f6; box-sizing: border-box; scroll-behavior: smooth; }");
            sb.AppendLine("#map-container:active { cursor: grabbing; scroll-behavior: auto; }");
            sb.AppendLine(".tree-wrapper { transform-origin: top center; transition: transform 0.3s ease; padding: 40vh 50vw; display: inline-block; min-width: 100%; box-sizing: border-box; }"); 
            sb.AppendLine(".tree { display: inline-block; min-width: max-content; user-select: none; }"); 
            sb.AppendLine(".tree ul { padding-top: 20px; position: relative; transition: all 0.5s; display: flex; justify-content: center; padding-left: 0; }");
            sb.AppendLine(".tree li { float: left; text-align: center; list-style-type: none; position: relative; padding: 20px 5px 0; transition: all 0.5s; }");
            sb.AppendLine(".tree li::before, .tree li::after { content: ''; position: absolute; top: 0; right: 50%; border-top: 2px solid #aaa; width: 50%; height: 20px; }");
            sb.AppendLine(".tree li::after { right: auto; left: 50%; border-left: 2px solid #aaa; }");
            sb.AppendLine(".tree li:only-child::after, .tree li:only-child::before { display: none; }");
            sb.AppendLine(".tree li:only-child { padding-top: 0; }");
            sb.AppendLine(".tree li:first-child::before, .tree li:last-child::after { border: 0 none; }");
            sb.AppendLine(".tree li:last-child::before { border-right: 2px solid #aaa; border-radius: 0 5px 0 0; }");
            sb.AppendLine(".tree li:first-child::after { border-radius: 5px 0 0 0; }");
            sb.AppendLine(".tree ul ul::before { content: ''; position: absolute; top: 0; left: 50%; border-left: 2px solid #aaa; width: 0; height: 20px; }");
            sb.AppendLine(".collapsed > ul { display: none !important; }");
            sb.AppendLine(".box-wrapper { position: relative; display: inline-block; }");
            sb.AppendLine(".sector-box { width: 220px; min-height: 80px; border: 2px solid #0088cc; padding: 10px; color: #333; font-weight: bold; font-size: 13px; display: flex; flex-direction: column; align-items: center; justify-content: center; border-radius: 8px; background-color: white; transition: all 0.3s; box-shadow: 0 4px 6px rgba(0,0,0,0.1); word-wrap: break-word; user-select: none !important; -webkit-user-select: none !important; cursor: pointer !important; }");
            sb.AppendLine(".sector-box * { user-select: none !important; -webkit-user-select: none !important; cursor: pointer !important; }"); 
            sb.AppendLine(".has-children > .box-wrapper > .sector-box { border-bottom: 5px solid #0088cc; }"); 
            sb.AppendLine(".highlight { background-color: #fffac0 !important; border-color: #ff9800 !important; z-index: 10; }"); 
            sb.AppendLine(".btn-pessoas { margin-top: 8px; font-size: 11px; background: #eee; border-radius: 12px; padding: 4px 10px; color: #555; z-index: 20; border: 1px solid #ccc; pointer-events: auto; }");
            sb.AppendLine(".tooltip-content { display: none; width: 300px; background-color: #2c3e50; color: #fff; text-align: left; border-radius: 8px; padding: 15px; position: absolute; z-index: 9999; top: 110%; left: 50%; transform: translateX(-50%); box-shadow: 0px 10px 20px rgba(0,0,0,0.4); max-height: 260px; overflow-y: auto; font-weight: normal; font-size: 12px; cursor: default !important; }");
            sb.AppendLine(".tooltip-content * { cursor: default !important; user-select: text !important; -webkit-user-select: text !important; }"); 
            sb.AppendLine(".tooltip-content.show-pinned { display: block; border: 2px solid #ffeb3b; }"); 
            sb.AppendLine(".box-wrapper:hover .tooltip-content:not(.pinned-mode) { display: block; }"); 
            sb.AppendLine(".tree .tooltip-content ul.lista-servidores { display: block !important; width: 100% !important; padding: 0 !important; margin: 0 !important; position: static !important; }");
            sb.AppendLine(".tree .tooltip-content ul.lista-servidores li.item-servidor { display: block !important; width: 100% !important; float: none !important; clear: both !important; text-align: left !important; padding: 10px 0 !important; margin: 0 !important; border-bottom: 1px solid #4a6278 !important; position: static !important; box-sizing: border-box !important; line-height: 1.4 !important; }");
            sb.AppendLine(".tree .tooltip-content ul::before, .tree .tooltip-content ul::after, .tree .tooltip-content li::before, .tree .tooltip-content li::after { display: none !important; content: none !important; border: none !important; }");
            sb.AppendLine(".tree .tooltip-content ul.lista-servidores li.item-servidor:last-child { border-bottom: none !important; }");
            sb.AppendLine(".admin-badge { background-color: #ff4d4d; color: white; padding: 2px 4px; border-radius: 3px; font-size: 9px; margin-left: 5px; font-weight: bold; float: right; }");
            sb.AppendLine("</style></head><body>");
            
            sb.AppendLine("<div class='top-bar'>");
            sb.AppendLine("  <div class='top-left'>");
            sb.AppendLine("    <h1>Bem-vindo(a) ao Organograma SEE-PE</h1>");
            sb.AppendLine("    <div class='user-profile-container'>");
            sb.AppendLine("      <div class='user-profile' id='btn-toggle-dropdown'>");
            sb.AppendLine("        <div class='avatar' id='user-avatar'>?</div>");
            sb.AppendLine("        <div style='display:flex; flex-direction:column;'><span style='font-size:11px; color:#777;'>Olá,</span><strong id='user-name' style='font-size:13px;'>Usuário</strong></div>");
            sb.AppendLine("        <svg viewBox='0 0 24 24' width='16' height='16' stroke='#555' stroke-width='2' fill='none'><polyline points='6 9 12 15 18 9'></polyline></svg>");
            sb.AppendLine("      </div>");
            sb.AppendLine("      <div class='dropdown-menu' id='profile-dropdown'>");
            sb.AppendLine("        <div class='dropdown-header'>Logado como <strong id='drop-email'>...</strong></div>");
            sb.AppendLine($"        <a class='dropdown-item' id='btn-open-modal'>{iconGear} Configurações</a>");
            sb.AppendLine($"        <a class='dropdown-item' id='btn-sair' style='color:#ff4d4d;'>{iconLogout} Sair do Sistema</a>");
            sb.AppendLine("      </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class='search-wrapper'>");
            sb.AppendLine("    <input type='text' id='search-input' class='search-box-input' placeholder='Buscar Setor ou Nome...' autocomplete='off'>");
            sb.AppendLine("    <svg onclick='buscarESidebar()' class='search-icon' viewBox='0 0 24 24' stroke='currentColor' stroke-width='2' fill='none'><circle cx='11' cy='11' r='8'></circle><line x1='21' y1='21' x2='16.65' y2='16.65'></line></svg>");
            sb.AppendLine("    <div id='autocomplete-list' class='autocomplete-list'></div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div id='sidebar' class='sidebar'>");
            sb.AppendLine("  <div class='sidebar-header'><h2>Resultados da Busca</h2><button class='btn-close-sidebar' onclick='fecharSidebar()'>×</button></div>");
            sb.AppendLine("  <div id='sidebar-results' class='sidebar-results'></div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='modal-overlay' id='settings-modal'>");
            sb.AppendLine("  <div class='modal-content'>");
            sb.AppendLine("    <button class='modal-close' style='position:absolute; right:15px; top:15px; border:none; background:none; font-size:20px; cursor:pointer;' onclick='fecharModal()'>×</button>");
            sb.AppendLine("    <h2>Configurações do Perfil</h2>");
            
            // SESSÃO 1: PERFIL
            sb.AppendLine("    <div class='form-group'><label>Nome de Exibição:</label><input type='text' id='input-new-name'></div>");
            sb.AppendLine("    <div class='form-group'><label>Nova Foto (Arquivo):</label><input type='file' id='input-photo-file' accept='image/*'></div>");
            sb.AppendLine("    <div class='crop-container' id='crop-container'><img id='crop-image' src=''></div>");
            sb.AppendLine("    <button class='btn-save-settings' onclick='salvarPerfil()'>Salvar Perfil</button>");
            sb.AppendLine("    <div id='modal-msg-perfil' style='margin-top:10px; font-size:13px; text-align:center; display:none;'></div>");
            sb.AppendLine("    <hr style='border:none; border-top:1px solid #eee; margin: 20px 0;'>");
            
            // SESSÃO 2: SENHA
            sb.AppendLine("    <button class='btn-save-settings' style='background:#6c757d; margin-top:0;' onclick='toggleSenhaSection()'>Trocar Senha de Acesso</button>");
            sb.AppendLine("    <div id='senha-section' style='display:none; margin-top:15px; padding:15px; background:#f8f9fa; border-radius:8px; border:1px solid #ddd;'>");
            sb.AppendLine("      <h3 style='font-size:14px; color:#555; margin-top:0; margin-bottom:15px;'>Segurança</h3>");
            sb.AppendLine("      <div class='form-group'><label>Senha Atual:</label><div class='pass-wrapper'><input type='password' id='input-current-pass'><span class='eye-icon' onclick='toggleEye(\"input-current-pass\", this)'>" + iconEyeOpen + "</span></div></div>");
            sb.AppendLine("      <div class='form-group'><label>Nova Senha:</label><div class='pass-wrapper'><input type='password' id='input-new-pass' placeholder='Mínimo de 6 caracteres'><span class='eye-icon' onclick='toggleEye(\"input-new-pass\", this)'>" + iconEyeOpen + "</span></div></div>");
            sb.AppendLine("      <div class='form-group'><label>Confirmar Nova Senha:</label><div class='pass-wrapper'><input type='password' id='input-confirm-pass'><span class='eye-icon' onclick='toggleEye(\"input-confirm-pass\", this)'>" + iconEyeOpen + "</span></div></div>");
            sb.AppendLine("      <button class='btn-save-settings' style='background:#28a745;' onclick='salvarSenha()'>Atualizar Senha</button>");
            sb.AppendLine("      <div id='modal-msg-senha' style='margin-top:10px; font-size:13px; text-align:center; display:none;'></div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            // SESSÃO 3: NAVEGACAO
            sb.AppendLine("<div class='zoom-controls'>");
            sb.AppendLine("  <button onclick='mudarZoom(0.2)' title='Aproximar'>+</button>");
            sb.AppendLine("  <button onclick='mudarZoom(-0.2)' title='Afastar'>-</button>");
            sb.AppendLine($"  <button class='reset-btn' onclick='recolherTudo()' title='Recolher Organograma e Centralizar'>{iconResetButton}</button>");
            sb.AppendLine("</div>");

            // SESSÃO 4: ASSINATURA|MARCA D'AGUA
            sb.AppendLine("<div class='watermark'>");
            sb.AppendLine("  <div class='watermark-title'>Automatizado e Desenvolvido por <strong>Gabriel Arruda</strong></div>");
            sb.AppendLine("  <div class='watermark-links'>");
            sb.AppendLine($"    <a href='https://github.com/Gabriel-Olivr' target='_blank'>{iconGithub} Meu GitHub</a>");
            sb.AppendLine($"    <a href='https://www.linkedin.com/in/gabriel-oliveira-773743346/' target='_blank'>{iconLinkedin} Meu LinkedIn</a>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div id='map-container'><div class='tree-wrapper' id='tree-wrapper'><div class='tree'><ul>");
            RenderizarSetorHtml(setorRaiz, sb);
            sb.AppendLine("</ul></div></div></div>");

            // JS criptografado

            sb.AppendLine(@"<script type='module'>
        import { initializeApp } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-app.js';
        import { getAuth, onAuthStateChanged, signOut, updateProfile, updatePassword, EmailAuthProvider, reauthenticateWithCredential } from 'https://www.gstatic.com/firebasejs/10.8.1/firebase-auth.js';

        const firebaseConfig = { apiKey: atob('QUl6YVN5RDdqNFNaaHRGQmpQZUF1RkVNRUs2Zm56UDlyX0hUWVQ4'), authDomain: 'organograma-see-pe.firebaseapp.com', projectId: 'organograma-see-pe', storageBucket: 'organograma-see-pe.firebasestorage.app', messagingSenderId: '471766110154', appId: atob('MTo0NzE3NjYxMTAxNTQ6d2ViOmEyMTU0YWYxZmI5MDk2ZmQzODQ5MzM=') };        const app = initializeApp(firebaseConfig);
        const auth = getAuth(app);

        const svgUser = `<svg viewBox='0 0 24 24' width='14' height='14' stroke='currentColor' stroke-width='2' fill='none'><path d='M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2'></path><circle cx='12' cy='7' r='4'></circle></svg>`;
        const svgBuilding = `<svg viewBox='0 0 24 24' width='14' height='14' stroke='currentColor' stroke-width='2' fill='none'><rect x='4' y='2' width='16' height='20' rx='2' ry='2'></rect><path d='M9 22v-4h6v4'></path><path d='M8 6h.01'></path><path d='M16 6h.01'></path><path d='M12 6h.01'></path><path d='M12 10h.01'></path><path d='M12 14h.01'></path><path d='M16 10h.01'></path><path d='M16 14h.01'></path><path d='M8 10h.01'></path><path d='M8 14h.01'></path></svg>`;

        let inactivityTimeout;
        function resetInactivityTimer() {
            clearTimeout(inactivityTimeout);
            inactivityTimeout = setTimeout(() => {
                signOut(auth).then(() => { window.location.replace('index.html'); });
            }, 900000); 
        }
        
        window.onload = resetInactivityTimer; document.onmousemove = resetInactivityTimer; document.onkeydown = resetInactivityTimer; document.onclick = resetInactivityTimer; document.onscroll = resetInactivityTimer;

        onAuthStateChanged(auth, (user) => {
            if (user) { 
                document.body.style.display = 'block'; 
                let displayName = user.displayName || (user.email.split('@')[0].charAt(0).toUpperCase() + user.email.split('@')[0].slice(1));
                
                document.getElementById('user-name').innerText = displayName;
                document.getElementById('drop-email').innerText = user.email;
                document.getElementById('input-new-name').value = displayName;

                let savedLocalPic = localStorage.getItem('avatar_' + user.email);
                if(savedLocalPic) document.getElementById('user-avatar').innerHTML = `<img src='${savedLocalPic}' alt='Avatar'>`;
                else if(user.photoURL) document.getElementById('user-avatar').innerHTML = `<img src='${user.photoURL}' alt='Avatar'>`;
                else document.getElementById('user-avatar').innerText = displayName.charAt(0).toUpperCase();
                
                setTimeout(centerMap, 100); setTimeout(centerMap, 500); setTimeout(centerMap, 1500); 
            } else { window.location.replace('index.html'); }
        });

        document.getElementById('btn-toggle-dropdown').addEventListener('click', (e) => { e.stopPropagation(); document.getElementById('profile-dropdown').classList.toggle('show'); });
        
        document.addEventListener('click', (e) => { 
            document.getElementById('profile-dropdown').classList.remove('show'); 
            if(e.target !== document.getElementById('search-input')) document.getElementById('autocomplete-list').style.display = 'none';
            if (!e.target.closest('.btn-pessoas') && !e.target.closest('.tooltip-content')) {
                document.querySelectorAll('.show-pinned').forEach(el => el.classList.remove('show-pinned'));
                document.querySelectorAll('.pinned-mode').forEach(el => el.classList.remove('pinned-mode'));
            }
        });

        document.getElementById('btn-sair').addEventListener('click', () => signOut(auth).then(() => window.location.replace('index.html')));

        window.toggleEye = function(inputId, iconEl) {
            const input = document.getElementById(inputId);
            if(input.type === 'password') {
                input.type = 'text';
                iconEl.innerHTML = `<svg viewBox='0 0 24 24' width='16' height='16' stroke='currentColor' stroke-width='2' fill='none'><path d='M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24'></path><line x1='1' y1='1' x2='23' y2='23'></line></svg>`;
            } else {
                input.type = 'password';
                iconEl.innerHTML = `<svg viewBox='0 0 24 24' width='16' height='16' stroke='currentColor' stroke-width='2' fill='none'><path d='M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z'></path><circle cx='12' cy='12' r='3'></circle></svg>`;
            }
        };

        let cropper = null; let finalImageBase64 = null;
        document.getElementById('btn-open-modal').addEventListener('click', () => document.getElementById('settings-modal').style.display = 'flex');
       
        window.fecharModal = () => { 
            document.getElementById('settings-modal').style.display = 'none'; 
            document.getElementById('modal-msg-perfil').style.display = 'none'; 
            document.getElementById('modal-msg-senha').style.display = 'none'; 
            document.getElementById('senha-section').style.display = 'none';
            if(cropper) { cropper.destroy(); cropper = null; } 
            document.getElementById('crop-container').style.display = 'none'; 
            document.getElementById('input-photo-file').value = ''; 
            document.getElementById('input-current-pass').value = '';
            document.getElementById('input-new-pass').value = '';
            document.getElementById('input-confirm-pass').value = '';
        };

        window.toggleSenhaSection = function() {
            const section = document.getElementById('senha-section');
            section.style.display = section.style.display === 'none' ? 'block' : 'none';
        };

        // Função exclusiva para Salvar NOME e FOTO
        window.salvarPerfil = async function() {
            const user = auth.currentUser;
            const newName = document.getElementById('input-new-name').value.trim();
            const msgBox = document.getElementById('modal-msg-perfil');
            
            msgBox.style.display = 'block'; msgBox.style.color = '#0088cc'; msgBox.innerText = 'Salvando perfil...';

            if(cropper) finalImageBase64 = cropper.getCroppedCanvas({width: 80, height: 80}).toDataURL('image/jpeg', 0.4);

            try {
                let updates = {};
                if(newName !== user.displayName) updates.displayName = newName;
                if(finalImageBase64) {
                    localStorage.setItem('avatar_' + user.email, finalImageBase64);
                    updates.photoURL = finalImageBase64;
                }
                
                if(Object.keys(updates).length > 0) await updateProfile(user, updates);
                
                msgBox.style.color = 'green'; msgBox.innerText = 'Perfil atualizado com sucesso!';
                setTimeout(() => window.location.reload(), 1200);

            } catch(error) { 
                console.error(error);
                msgBox.style.color = 'red'; 
                msgBox.innerText = 'Erro ao atualizar o perfil.'; 
            }
        };

        // Função exclusiva para Salvar a SENHA!!!!
        window.salvarSenha = async function() {
            const user = auth.currentUser;
            const currentPass = document.getElementById('input-current-pass').value;
            const newPass = document.getElementById('input-new-pass').value;
            const confirmPass = document.getElementById('input-confirm-pass').value;
            const msgBox = document.getElementById('modal-msg-senha');
            
            msgBox.style.display = 'block'; msgBox.style.color = '#0088cc'; msgBox.innerText = 'Autenticando e trocando senha...';

            if(!currentPass) { msgBox.style.color = 'red'; msgBox.innerText = 'Digite sua Senha Atual.'; return; }
            if(!newPass || !confirmPass) { msgBox.style.color = 'red'; msgBox.innerText = 'Preencha a nova senha.'; return; }
            if(newPass !== confirmPass) { msgBox.style.color = 'red'; msgBox.innerText = 'As novas senhas não coincidem!'; return; }
            if(newPass.length < 6) { msgBox.style.color = 'red'; msgBox.innerText = 'A nova senha precisa ter no mínimo 6 caracteres.'; return; }

            try {
                const credential = EmailAuthProvider.credential(user.email, currentPass);
                await reauthenticateWithCredential(user, credential);
                await updatePassword(user, newPass);
                
                msgBox.style.color = 'green'; msgBox.innerText = 'Senha alterada com sucesso!';
                
                // Limpa os campos de senha por segurança
                document.getElementById('input-current-pass').value = '';
                document.getElementById('input-new-pass').value = '';
                document.getElementById('input-confirm-pass').value = '';
                
                // Esconde a aba de senha depois de 2 segundos
                setTimeout(() => { 
                    document.getElementById('senha-section').style.display = 'none'; 
                    msgBox.style.display = 'none'; 
                }, 2000);

            } catch(error) { 
                console.error(error);
                msgBox.style.color = 'red'; 
                msgBox.innerText = 'Erro: Verifique se sua Senha Atual está correta.'; 
            }
        };

        function removerAcentos(str) { return (!str) ? '' : str.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase(); }

        let baseDeDadosBusca = [];
        setTimeout(() => {
            let uniqueNomes = new Set();
            document.querySelectorAll('.sector-name').forEach(el => {
                let n = el.innerText.trim();
                if(!uniqueNomes.has('S_'+n)) { uniqueNomes.add('S_'+n); baseDeDadosBusca.push({ tipo: 'setor', nome: n }); }
            });
            document.querySelectorAll('.tooltip-content li').forEach(el => {
                let n = el.innerHTML.split('<br>')[0].replace(/<[^>]*>?/gm, '').trim();
                if(!uniqueNomes.has('U_'+n)) { uniqueNomes.add('U_'+n); baseDeDadosBusca.push({ tipo: 'pessoa', nome: n }); }
            });
        }, 1000);

        const searchInput = document.getElementById('search-input');
        const autoList = document.getElementById('autocomplete-list');
        
        searchInput.addEventListener('focus', function() { this.select(); });

        let currentFocus = -1;
        searchInput.addEventListener('input', function() {
            fecharSidebar();
            const valOriginal = this.value.trim();
            const val = removerAcentos(valOriginal);
            autoList.innerHTML = ''; currentFocus = -1;
            if(!val) { autoList.style.display = 'none'; return; }

            const resultados = baseDeDadosBusca.filter(item => removerAcentos(item.nome).includes(val)).slice(0, 8);
            if(resultados.length === 0) { autoList.style.display = 'none'; return; }

            autoList.style.display = 'block';
            resultados.forEach((res) => {
                let div = document.createElement('div');
                div.className = 'autocomplete-item';
                div.innerHTML = (res.tipo === 'setor' ? svgBuilding : svgUser) + ' ' + res.nome;
                div.onclick = () => { searchInput.value = res.nome; autoList.style.display = 'none'; buscarESidebar(res.nome); };
                autoList.appendChild(div);
            });
        });

        searchInput.addEventListener('keydown', function(e) {
            let x = document.getElementById('autocomplete-list');
            if (x) x = x.getElementsByTagName('div');
            if (e.keyCode == 40) { currentFocus++; addActive(x); } 
            else if (e.keyCode == 38) { currentFocus--; addActive(x); } 
            else if (e.keyCode == 13) {
                e.preventDefault();
                if (currentFocus > -1 && x) x[currentFocus].click();
                else if (searchInput.value) { autoList.style.display = 'none'; buscarESidebar(); }
            }
        });

        function addActive(x) { if (!x) return false; removeActive(x); if (currentFocus >= x.length) currentFocus = 0; if (currentFocus < 0) currentFocus = (x.length - 1); x[currentFocus].classList.add('autocomplete-active'); }
        function removeActive(x) { for (let i = 0; i < x.length; i++) x[i].classList.remove('autocomplete-active'); }
        
        window.fecharSidebar = () => document.getElementById('sidebar').classList.remove('open');

        window.buscarESidebar = function(termoOpcional = null) {
            autoList.style.display = 'none';
            const termoReal = termoOpcional || searchInput.value;
            const termoFormatado = removerAcentos(termoReal.trim());
            if (termoOpcional) searchInput.value = termoOpcional; 

            const resultsDiv = document.getElementById('sidebar-results');
            if(!termoFormatado) { fecharSidebar(); return; }
            
            resultsDiv.innerHTML = ''; let count = 0;
            document.querySelectorAll('.highlight').forEach(el => el.classList.remove('highlight'));
            
            let duplicatasFiltro = new Set(); 

            document.querySelectorAll('.sector-name').forEach(span => {
                if(removerAcentos(span.innerText).includes(termoFormatado)) {
                    let nomeSetor = span.innerText.trim();
                    if(!duplicatasFiltro.has('S_' + nomeSetor)) {
                        duplicatasFiltro.add('S_' + nomeSetor);
                        count++; const box = span.closest('.sector-box'); const id = 'sector-' + count; box.id = id; 
                        resultsDiv.innerHTML += `<div class='result-item'><strong style='display:flex; align-items:center; gap:5px;'>${svgBuilding} Setor:</strong> ${nomeSetor}<button onclick=""focarSetor('${id}')"">Ver no Mapa</button></div>`;
                    }
                }
            });

            document.querySelectorAll('.tooltip-content li').forEach(li => {
                if(removerAcentos(li.innerText).includes(termoFormatado)) {
                    let pessoaNome = li.innerHTML.split('<br>')[0].replace(/<[^>]*>?/gm, '').trim();
                    const box = li.closest('.sector-box'); const sectorName = box.querySelector('.sector-name').innerText.trim();
                    
                    if(!duplicatasFiltro.has('U_' + pessoaNome + '_' + sectorName)) {
                        duplicatasFiltro.add('U_' + pessoaNome + '_' + sectorName);
                        count++; const id = 'person-sector-' + count; box.id = id;
                        resultsDiv.innerHTML += `<div class='result-item'><strong style='display:flex; align-items:center; gap:5px;'>${svgUser} ${pessoaNome}</strong><br><small style='display:flex; align-items:center; gap:5px; margin-top:5px;'>${svgBuilding} Lotação: ${sectorName}</small><br><button onclick=""focarSetor('${id}', true)"">Localizar Servidor</button></div>`;
                    }
                }
            });

            if(count === 0) resultsDiv.innerHTML = '<p style=""color:#555;"">Nenhum resultado encontrado.</p>';
            document.getElementById('sidebar').classList.add('open');
        };

        window.focarSetor = function(boxId, isPerson = false) { const box = document.getElementById(boxId); if(box) executarFoco(box, isPerson); };

        window.focarNoMapaDireto = function(nomeBuscado, tipo) {
            let foundBox = null; let isPerson = (tipo === 'pessoa'); let termo = removerAcentos(nomeBuscado);
            if(isPerson) { document.querySelectorAll('.tooltip-content li').forEach(li => { if(removerAcentos(li.innerText).includes(termo)) foundBox = li.closest('.sector-box'); }); } 
            else { document.querySelectorAll('.sector-name').forEach(span => { if(removerAcentos(span.innerText) === termo) foundBox = span.closest('.sector-box'); }); }
            if(foundBox) executarFoco(foundBox, isPerson);
        };

        function executarFoco(box, isPerson) {
            document.querySelectorAll('.highlight').forEach(el => el.classList.remove('highlight'));
            document.querySelectorAll('.tree li').forEach(el => el.classList.add('collapsed'));
            
            box.classList.add('highlight');
            let parentLi = box.closest('li');
            while(parentLi) { parentLi.classList.remove('collapsed'); parentLi = parentLi.parentElement.closest('li'); }
            
            if(isPerson) {
                const wrapper = box.closest('.box-wrapper'); const tooltip = wrapper.querySelector('.tooltip-content');
                if(tooltip) { document.querySelectorAll('.show-pinned').forEach(el => el.classList.remove('show-pinned')); tooltip.classList.add('show-pinned'); wrapper.classList.add('pinned-mode'); }
            }
            
            currentZoom = 1; document.getElementById('tree-wrapper').style.transform = `scale(1)`; 
            setTimeout(() => { 
                const mapContainer = document.getElementById('map-container'); let el = box; let offsetLeft = 0; let offsetTop = 0;
                while(el && el !== mapContainer) { offsetLeft += el.offsetLeft; offsetTop += el.offsetTop; el = el.offsetParent; }
                const centerX = offsetLeft + (box.offsetWidth / 2); const centerY = offsetTop + (box.offsetHeight / 2) - 100;
                mapContainer.scrollTo({ left: centerX - (mapContainer.clientWidth / 2), top: centerY - (mapContainer.clientHeight / 2), behavior: 'smooth' });
            }, 300);
            if(window.innerWidth < 800) fecharSidebar();
        }

        let currentZoom = 1;
        window.mudarZoom = (amount) => { currentZoom += amount; if(currentZoom < 0.3) currentZoom = 0.3; if(currentZoom > 2.0) currentZoom = 2.0; document.getElementById('tree-wrapper').style.transform = `scale(${currentZoom})`; };
        
        // RESET: RECOLHE TUDO E CENTRALIZA A CAIXA RAIZ (SEE) FECHADA
        window.recolherTudo = () => { 
            try {
                fecharSidebar();
                document.getElementById('search-input').value = '';
                document.querySelectorAll('.highlight').forEach(el => el.classList.remove('highlight'));
                document.querySelectorAll('.show-pinned').forEach(el => el.classList.remove('show-pinned'));
                document.querySelectorAll('.pinned-mode').forEach(el => el.classList.remove('pinned-mode'));
                
                // Força todos os itens da árvore a fecharem
                document.querySelectorAll('.tree li').forEach(el => el.classList.add('collapsed')); 
                
                currentZoom = 1; 
                document.getElementById('tree-wrapper').style.transform = `scale(1)`; 
                
                // Centralização focada na raiz fechada
                centerMap();
            } catch(e) { console.error('Erro no Reset:', e); }
        };

        const slider = document.getElementById('map-container');
        let isDown = false; let startX, startY, scrollLeft, scrollTop;
        slider.addEventListener('mousedown', (e) => {
            if (e.target.closest('.tooltip-content') || e.target.closest('.zoom-controls') || e.target.closest('.modal-overlay') || e.target.closest('.watermark') || e.target.closest('.sidebar')) return;
            isDown = true; startX = e.pageX - slider.offsetLeft; startY = e.pageY - slider.offsetTop; scrollLeft = slider.scrollLeft; scrollTop = slider.scrollTop;
        });
        slider.addEventListener('mouseleave', () => isDown = false);
        slider.addEventListener('mouseup', () => isDown = false);
        slider.addEventListener('mousemove', (e) => {
            if(!isDown) return; e.preventDefault();
            const x = e.pageX - slider.offsetLeft; const y = e.pageY - slider.offsetTop;
            slider.scrollLeft = scrollLeft - (x - startX) * 1.5; slider.scrollTop = scrollTop - (y - startY) * 1.5;
        });

        function centerMap() { 
            const mapContainer = document.getElementById('map-container');
            const rootBox = document.querySelector('.tree > ul > li > .box-wrapper'); 
            if(rootBox && mapContainer) { 
                let el = rootBox; let offsetLeft = 0; let offsetTop = 0;
                while(el && el !== mapContainer) { offsetLeft += el.offsetLeft; offsetTop += el.offsetTop; el = el.offsetParent; }
                const centerX = offsetLeft + (rootBox.offsetWidth / 2); const centerY = offsetTop + (rootBox.offsetHeight / 2) - 100; 
                mapContainer.scrollLeft = centerX - (mapContainer.clientWidth / 2); mapContainer.scrollTop = centerY - (mapContainer.clientHeight / 2);
            } 
        }
        
        window.addEventListener('resize', () => { if(document.querySelectorAll('.tree li:not(.collapsed)').length === 1) centerMap(); });
        window.toggleNode = function(element, event) { if(event && event.target.closest('.btn-pessoas')) return; const li = element.closest('li'); if(li.classList.contains('has-children')) li.classList.toggle('collapsed'); };
        
        window.toggleTooltip = function(btnElement) { 
            const wrapper = btnElement.closest('.box-wrapper'); const tooltip = wrapper.querySelector('.tooltip-content'); 
            document.querySelectorAll('.show-pinned').forEach(el => { if(el !== tooltip) { el.classList.remove('show-pinned'); el.closest('.box-wrapper').classList.remove('pinned-mode'); } }); 
            tooltip.classList.toggle('show-pinned'); wrapper.classList.toggle('pinned-mode');  
        };
    </script>");
            sb.AppendLine("</body></html>");

            string caminhoArquivo = Path.Combine(PastaHtml, "organograma.html");
            File.WriteAllText(caminhoArquivo, sb.ToString());
        }
#endregion
        private static void RenderizarSetorHtml(Setor setor, StringBuilder html)
        {
            string clsRecolhido = "collapsed"; 
            string clsFilhos = setor.SubSetores.Any() ? "has-children" : "";

            html.AppendLine($"<li class='{clsFilhos} {clsRecolhido}'>");
            html.AppendLine("  <div class='box-wrapper'>");
            html.AppendLine($"    <div class='sector-box' onclick='toggleNode(this, event)'>");
            html.AppendLine($"      <span class='sector-name'>{setor.Nome}</span>");
            
            if (setor.Usuarios.Any())
            {
                html.AppendLine($"      <div class='btn-pessoas' onclick='toggleTooltip(this)'>👥 {setor.Usuarios.Count} Lotado(s)</div>");
                html.AppendLine("      <div class='tooltip-content'>");
                html.AppendLine($"        <div style='margin-bottom:8px; font-weight:bold; border-bottom:1px solid #aaa; padding-bottom:5px; color:#ffeb3b;'>Equipe ({setor.Usuarios.Count})</div>");
                
                html.AppendLine("        <ul class='lista-servidores'>");
                foreach (var user in setor.Usuarios.OrderByDescending(u => u.IsAdmin).ThenBy(u => u.NomeFormatado))
                {
                    string badge = user.IsAdmin ? "<span class='admin-badge'>ADM</span>" : "";
                    html.AppendLine($"          <li class='item-servidor'>{user.NomeFormatado} <br><small style='color:#bdc3c7;'>({user.Login})</small> {badge}</li>");
                }
                html.AppendLine("        </ul>");
                
                html.AppendLine("      </div>"); 
            }
            html.AppendLine("    </div>"); 
            html.AppendLine("  </div>");   

            if (setor.SubSetores.Any())
            {
                html.AppendLine("<ul>");
                foreach (var subSetor in setor.SubSetores) RenderizarSetorHtml(subSetor, html);
                html.AppendLine("</ul>");
            }
            html.AppendLine("</li>");
        }

        private static void GarantirPastaExiste()
        {
            if (!Directory.Exists(PastaHtml))
            {
                Directory.CreateDirectory(PastaHtml);
            }
        }
    }
}