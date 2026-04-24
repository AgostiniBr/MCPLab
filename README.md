![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![C#](https://img.shields.io/badge/C%23-Language-green)
![MCP](https://img.shields.io/badge/MCP-Model%20Context%20Protocol-orange)
![Ollama](https://img.shields.io/badge/Ollama-Local%20AI-black)
![License](https://img.shields.io/badge/License-MIT-yellow)

🌐 MCPLab — Exemplo Básico de Servidor MCP com .NET + Ollama
<br/>Este projeto demonstra uma estrutura BÁSICA para entender como utilizar um servidor MCP (Model Context Protocol) integrado a uma API e a modelos de IA locais, como o Ollama.
<br/>O objetivo é ensinar, de forma simples e acessível, como funciona o fluxo completo:
<br/>Frontend → API → MCP → Modelo de IA → API externa (clima)
<br/>Ideal para iniciantes que querem aprender arquitetura limpa, camadas desacopladas e integração com IA.

🌦️ O que este exemplo faz
<br/>. Consulta dados de clima usando uma API gratuita
<br/>. Envia perguntas ao Ollama para demonstrar o fluxo MCP
<br/>. Mostra como estruturar um projeto com camadas separadas
<br/>. Ensina o caminho correto para integrar modelos de IA em aplicações reais

🧭 Visão Geral do Fluxo
<br/>[Frontend HTML]
<br/>&emsp;&emsp;&emsp;&emsp;↓
<br/>[API ASP.NET Core]
<br/>&emsp;&emsp;&emsp;&emsp;↓
<br/>[MCP Server (.NET)]
<br/>&emsp;&emsp;&emsp;&emsp;↓
<br/>[API de Clima / Ollama / Outro Modelo]

✔ A camada WEB não precisa conhecer MCP  
✔ A API funciona como ponto central de segurança e controle  
✔ O MCP pode evoluir sem quebrar o frontend
✔ Você pode trocar o modelo de IA sem alterar o restante do sistema

🌦️ O que este exemplo faz
<br/>. Consulta dados de clima usando uma API gratuita
<br/>. Envia perguntas ao Ollama para demonstrar o fluxo MCP
<br/>. Mostra como estruturar um projeto com camadas separadas
<br/>. Ensina o caminho correto para integrar modelos de IA em aplicações reais

🤖 Modelos gratuitos que você pode usar
<br/>. OpenAI gpt‑4o‑mini (faixa gratuita)
<br/>. HuggingFace Inference API (alguns modelos grátis)
<br/>. Ollama (modelos locais)
<br/>. LM Studio (modelos locais)

🧱 Estrutura da Solution
<br/>MCPLab
<br/>│
<br/>├── Apresentacao
<br/>│     ├── MCPLab.Web (HTML puro)
<br/>│     └── MCPLab.WinForms (opcional futuramente)
<br/>│
<br/>├── API
<br/>│     └── MCPLab.Api (ASP.NET Core)
<br/>│
<br/>└── MCP
<br/>&emsp;&emsp;&emsp;&emsp;└── MCPLab.McpServer (.NET MCP Server)

🧩 Por que separar Frontend, API e MCP?
<br/>. Facilita o desacoplamento entre interface e lógica
<br/>. Permite trocar o frontend sem alterar o backend
<br/>. Mantém o MCP isolado e organizado
<br/>. Ajuda iniciantes a entenderem camadas de software
<br/>. Permite evoluir cada parte de forma independente

🚀 Como Rodar o Projeto
1. Instale o .NET 8+
https://dotnet.microsoft.com/download
2. Instale o Ollama
https://ollama.com/download
3. Baixe um modelo local
Exemplo: ollama pull phi3
4. Rode o MCP Server
No diretório MCPLab.McpServer: dotnet run
5. Rode a API
No diretório MCPLab.Api: dotnet run
6. Abra o Frontend
Abra o arquivo: MCPLab.Web/index.html

🧪 Exemplo de Perguntas
<br/>. Pergunta sobre clima: Como está o clima em São Paulo?
<br/>. Pergunta técnica: Explique o que é uma API REST.
<br/>. Pergunta geral: O que é o MCP?
<br/>A API decide automaticamente qual ferramenta MCP usar.

🛠️ Tecnologias Utilizadas
<br/>. .NET 8
<br/>. ASP.NET Core
<br/>. Ollama
<br/>. HTML/CSS/JS puro
<br/>. MCP (Model Context Protocol)
<br/>. C# Clean Architecture

📌 Próximos Passos (Roadmap)
<br/>. [ ] Adicionar interface WinForms
<br/>. [ ] Criar mais ferramentas MCP (ex: busca em banco de dados)
<br/>. [ ] Adicionar autenticação na API
<br/>. [ ] Criar um dashboard de logs do MCP
<br/>. [ ] Suporte a Azure OpenAI e OpenAI API
<br/>. [ ] Criar testes automatizados

🤝 Agradecimentos
<br/>Espero que este programa ajude a entender o BÁSICO do MCP.
<br/>Este projeto foi desenvolvido em cooperação com o assistente de IA Copilot (Microsoft), que contribuiu ativamente na arquitetura, organização do MCP-like Server, estruturação das ferramentas, revisão de código e aprimoramento contínuo do fluxo entre API → MCP → Ollama.
<br/>A colaboração permitiu acelerar decisões técnicas, validar abordagens, identificar melhorias e manter o projeto alinhado a boas práticas de engenharia de software.
<br/><b>Dica: usar IA com “Vibe Studying” pode ser mais produtivo do que usar com “Vibe Coding”.</b>
