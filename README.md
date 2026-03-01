# 🏛️ Organograma SEE-PE

![Status](https://img.shields.io/badge/Status-Versão%201.0-green)
![Tech](https://img.shields.io/badge/C%23-.NET%209.0-blue)
![Frontend](https://img.shields.io/badge/Web-HTML5%20%2F%20CSS3%20%2F%20JS-orange)
![Security](https://img.shields.io/badge/Security-Firebase%20Auth-yellow)

Plataforma institucional de visualização dinâmica da estrutura organizacional da **Secretaria de Educação de Pernambuco (SEE)**, inspirada na [Estrutura Organizacional SEI SEE-PE](https://estruturasei.pe.gov.br/), assim ganhando uma repaginação e com novas funcionalidades [veja aqui](https://gabriel-olivr.github.io/organograma-sei-pe/).

---

## Funcionalidades principais

* **Mapa Interativo**: Organograma hierárquico com expansão e recolhimento de setores.
* **Busca Inteligente**: Localização instantânea de setores ou servidores no mapa com destaque visual em amarelo.
* **Segurança Robusta**:
    * Autenticação via **Firebase Auth**.
    * Fluxo de troca de senha com verificação tripla (Atual, Nova e Confirmação).
    * **Timeout de Inatividade**: Encerramento automático da sessão após **15 minutos** sem uso.
* **Gestão de Perfil**: Alteração de foto (com ferramenta de recorte) e nome de exibição.
* **Design**: Tela de login inspirada em site governamentais com carrossel dinâmico e logos institucionais bem como arte conceitual da sigla 'UMCT'.

---

## Tecnologias Utilizadas

### Backend (Processador de Dados)
* **C# / .NET 9.0**: Utilizado para **parsear arquivos HTML** locais e gerar a estrutura de dados.
* **HtmlAgilityPack**: Biblioteca para manipulação e extração de nós do HTML do SEI.

### Frontend (Interface do Usuário)
* **JavaScript (ES6+)**: Lógica do mapa, zoom e gerenciamento de inatividade.
* **Firebase SDK**: Gestão de usuários e autenticação em tempo real.
* **CSS3 Moderno**: Flexbox/Grid para o layout Split Screen e animações de carrossel.
* **Cropper.js**: Biblioteca para edição de imagem de perfil.

---

## ⚙️ Estrutura do Projeto

O sistema é gerado dinamicamente pela classe `HtmlGenerator.cs`, que produz dois arquivos principais:

1.  `index.html`: Portal de acesso com carrossel de imagens e autenticação.
2.  `organograma.html`: Interface principal com o mapa hierarquico, ferramentas de busca e configurações do perfil.


---

## Autor: [Gabriel Estevam]

[![LinkedIn](https://img.shields.io/badge/-LinkedIn-000?style=for-the-badge&logo=linkedin&logoColor=30A3DC)](https://www.linkedin.com/in/gabriel-oliveira-773743346/) 
[![DIO](https://img.shields.io/badge/-DIO-000?style=for-the-badge&logoColor=30A3DC)](https://web.dio.me/users/gabrielolivr_16?tab=achievements)

